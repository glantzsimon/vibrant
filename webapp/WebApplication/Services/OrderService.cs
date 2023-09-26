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
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPacksRepository;

        public OrderService(ILogger logger, IRepository<Order> ordersRepository, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPacksRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderProductPacksRepository = orderProductPacksRepository;
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
            order.ProductPacks = _orderProductPacksRepository.Find(e => e.OrderId == order.Id).ToList();
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