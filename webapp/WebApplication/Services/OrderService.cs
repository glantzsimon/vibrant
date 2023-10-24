using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using K9.WebApplication.ViewModels;

namespace K9.WebApplication.Services
{
    public class OrderService : CacheableServiceBase<Order>, IOrderService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPacksRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPackRepository;
        private readonly IRepository<Contact> _contactsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<RepCommission> _repCommissionsRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrderService(ILogger logger, IRepository<Order> ordersRepository, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPacksRepository, IRepository<Product> productsRepository, IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<Contact> contactsRepository, IRepository<User> usersRepository, IRepository<RepCommission> repCommissionsRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderProductPacksRepository = orderProductPacksRepository;
            _productsRepository = productsRepository;
            _productPackRepository = productPackRepository;
            _contactsRepository = contactsRepository;
            _usersRepository = usersRepository;
            _repCommissionsRepository = repCommissionsRepository;
            _defaultValues = defaultValues.Value;
        }

        public Order Find(int id)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                var order = _ordersRepository.Find(id);
                if (order != null)
                {
                    order = GetFullOrder(order);
                }

                return order;
            });
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

        public Order Find(string orderNumber)
        {
            var order = _ordersRepository.Find(e => e.OrderNumber == orderNumber).FirstOrDefault();
            if (order != null)
            {
                order = GetFullOrder(order);
            }

            return order;
        }

        public Order GetFullOrder(Order order)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(order.Id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                order.Products = _orderProductsRepository.Find(e => e.OrderId == order.Id).ToList();
                foreach (var orderProduct in order.Products)
                {
                    orderProduct.Product = _productsRepository.Find(orderProduct.ProductId);
                    orderProduct.TotalPrice = orderProduct.GetTotalPrice();
                }

                order.ProductPacks = _orderProductPacksRepository.Find(e => e.OrderId == order.Id).ToList();
                foreach (var orderProductPack in order.ProductPacks)
                {
                    orderProductPack.ProductPack = _productPackRepository.Find(orderProductPack.ProductPackId);
                }

                order.Contact = _contactsRepository.Find(order.ContactId ?? 0);
                order.ContactName = order.Contact?.FullName;

                order.User = _usersRepository.Find(order.UserId);
                order.UserName = order.User.Name;
                order.TotalPrice = order.GetTotalPrice();
                order.TotalProductsPrice = order.GetTotalProductsPrice();
                order.TotalProductPacksPrice = order.GetTotalProductPacksPrice();
                order.FormattedSuggestedDiscountAsPercent = order.GetFormattedSuggestedDiscountAsPercent();
                order.FormattedSuggestedDiscountAmount = order.GetFormattedSuggestedDiscountAmount();
                order.DiscountAmount = order.GetDiscountAmount();
                order.GrandTotal = order.GetGrandTotal();

                return order;
            });
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

        public Order UpdatePricesForContact(Order order)
        {
            if (!order.Products.Any(e => e.Amount > 0))
            {
                // New order - update price tier
                foreach (var product in order.Products.Where(e => e.Amount == 0))
                {
                    product.PriceTier = order.Contact.PriceTier;
                }
            }

            if (!order.ProductPacks.Any(e => e.Amount > 0))
            {
                // New order - update price tier
                foreach (var pack in order.ProductPacks.Where(e => e.Amount == 0))
                {
                    pack.PriceTier = order.Contact.PriceTier;
                }
            }

            return order;
        }

        public List<Order> List(bool retrieveFullOrder = false)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.TenMinutes));

                var orders = _ordersRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

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
            });
        }

        public Order Duplicate(int id)
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

        public void DeleteChildRecords(int id)
        {
            var order = Find(id);

            foreach (var orderProduct in order.Products)
            {
                _orderProductsRepository.Delete(orderProduct.Id);
            }

            foreach (var orderProductPack in order.ProductPacks)
            {
                _orderProductPacksRepository.Delete(orderProductPack.Id);
            }
        }

        public RepCommissionViewModel CalculateRepCommission(int repId)
        {
            var redeemedCommissions = _repCommissionsRepository.Find(e => e.RepId == repId).ToList();
            var repOrders = List(true).Where(e => e.RepId == repId || e.ContactId == repId).ToList();

            var totalRedeemed = redeemedCommissions.Sum(e => e.AmountRedeemed);
            var totalPrice = repOrders.SelectMany(e => e.Products).Sum(e => e.GetTotalPrice()) +
                             repOrders.SelectMany(e => e.ProductPacks).Sum(e => e.TotalPrice);
            
            var totalRedeemable = (totalPrice / 10) - totalRedeemed;

            return new RepCommissionViewModel
            {
                AmountRedeemable = totalRedeemable,
                AmountRedeemed = totalRedeemed,
                RepId = repId,
                Rep = _contactsRepository.Find(repId),
                RepCommissions = redeemedCommissions
            };
        }
    }
}