using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderItem> _orderItemsRepository;
        private readonly IRepository<OrderItemProduct> _orderItemProductsRepository;
        private readonly IRepository<OrderItemProductPack> _orderItemProductPacksRepository;

        public OrderService(ILogger logger, IRepository<Order> ordersRepository, IRepository<OrderItem> orderItemsRepository, IRepository<OrderItemProduct> orderItemProductsRepository, IRepository<OrderItemProductPack> orderItemProductPacksRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
            _orderItemProductsRepository = orderItemProductsRepository;
            _orderItemProductPacksRepository = orderItemProductPacksRepository;
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

        public List<OrderItem> GetFullOrderItems(int orderId)
        {
            var orderItems = _orderItemsRepository.Find(e => e.OrderId == orderId).ToList();
            foreach (var orderItem in orderItems)
            {
                orderItem.Products = _orderItemProductsRepository.Find(e => e.OrderItemId == orderItem.Id).ToList();
                orderItem.ProductPacks = _orderItemProductPacksRepository.Find(e => e.OrderItemId == orderItem.Id).ToList();
            }

            return orderItems;
        }

        public Order GetFullOrder(Order order)
        {
            order.OrderItems = GetFullOrderItems(order.Id);
            order.Products = order.OrderItems.SelectMany(e => e.Products).ToList();
            order.ProductPacks = order.OrderItems.SelectMany(e => e.ProductPacks).ToList();
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
    }
}