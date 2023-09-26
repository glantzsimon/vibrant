using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using K9.WebApplication.Config;

namespace K9.WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPacksRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPackRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrderService(ILogger logger, IRepository<Order> ordersRepository, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPacksRepository, IRepository<Product> productsRepository, IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderProductPacksRepository = orderProductPacksRepository;
            _productsRepository = productsRepository;
            _productPackRepository = productPackRepository;
            _defaultValues = defaultValues.Value;
        }

        public Order Find(int id)
        {
            var order = _ordersRepository.Find(id);
            if (order != null)
            {
                order = GetFullOrder(order);
            }

            return order;
        }

        public Order FindNext(int id)
        {
            var order = _ordersRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _ordersRepository.GetQuery("SELECT TOP 1 * FROM [Order] ORDER BY [Id]").FirstOrDefault();
            if (order != null)
            {
                order = GetFullOrder(order);
            }

            return order;
        }

        public Order FindPrevious(int id)
        {
            var order = _ordersRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _ordersRepository.GetQuery("SELECT TOP 1 * FROM [Order] ORDER BY [Id] DESC").FirstOrDefault();
            if (order != null)
            {
                order = GetFullOrder(order);
            }

            return order;
        }

        public Order Find(Guid id)
        {
            var order = _ordersRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            if (order != null)
            {
                order = GetFullOrder(order);
            }

            return order;
        }

        public Order GetFullOrder(Order order)
        {
            order.Products = _orderProductsRepository.Find(e => e.OrderId == order.Id).ToList();
            foreach (var orderProduct in order.Products)
            {
                orderProduct.Product = _productsRepository.Find(orderProduct.ProductId);
            }

            order.ProductPacks = _orderProductPacksRepository.Find(e => e.OrderId == order.Id).ToList();
            foreach (var orderProductPack in order.ProductPacks)
            {
                orderProductPack.ProductPack = _productPackRepository.Find(orderProductPack.ProductPackId);
            }

            return order;
        }

        public Order FillZeroQuantities(Order order)
        {
            foreach (var product in order.Products.Where(e => e.Amount == 0))
            {
                product.Amount = 1;
            }
            foreach (var pack in order.ProductPacks.Where(e => e.Amount == 0))
            {
                pack.Amount = 1;
            }

            return order;
        }

        public List<Order> List(bool retrieveFullOrder = false, bool includeCustomOrders = false)
        {
            var orders = _ordersRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

            if (!includeCustomOrders)
            {
                orders = orders.Where(e => e.ContactId <= 0 || !e.ContactId.HasValue).ToList();
            }

            if (retrieveFullOrder)
            {
                var fullOrders = new List<Order>();
                foreach (var order in orders)
                {
                    fullOrders.Add(GetFullOrder(order));
                }

                return fullOrders;
            }

            return orders;
        }

        public Order DuplicateOrder(int id)
        {
            var order = _ordersRepository.Find(id);
            if (order != null)
            {
                order = GetFullOrder(order);
            }
            var newOrderExternalId = Guid.NewGuid();
            int.TryParse(_defaultValues.DefaultUserId, out var userId);

            var newOrder = new Order
            {
                RequestedOn = DateTime.Today,
                DueBy = DateTime.Today.AddDays(11),
                UserId = userId > 0 ? userId : 3,
                Name = $"{order.Name} (Copy)",
                ShortDescription = order.ShortDescription,
                OrderType = order.OrderType,
                StartedOn = order.StartedOn,
                MadeOn = order.MadeOn,
                CompletedOn = order.CompletedOn,
                PaidOn = order.PaidOn,
                ContactId = order.ContactId,
                Discount = order.Discount,
                ExternalId = newOrderExternalId
            };
            
            _ordersRepository.Create(newOrder);

            // Get Id
            newOrder = Find(newOrderExternalId);

            // Copy products
            foreach (var orderProduct in order.Products)
            {
                var newProduct = new OrderProduct
                {
                    OrderId = newOrder.Id,
                    ProductId = orderProduct.Id,
                    PriceTier = orderProduct.PriceTier,
                    Amount = orderProduct.Amount
                };

                _orderProductsRepository.Create(newProduct);
            }

            // Copy product packs
            foreach (var orderProductPack in order.ProductPacks)
            {
                var newProductPack = new OrderProductPack
                {
                    OrderId = newOrder.Id,
                    ProductPackId = orderProductPack.Id,
                    PriceTier = orderProductPack.PriceTier,
                    Amount = orderProductPack.Amount
                };

                _orderProductPacksRepository.Create(newProductPack);
            }

            return newOrder;
        }
    }
}