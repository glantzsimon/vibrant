﻿using K9.DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.WebApplication.ViewModels
{
    public class OrdersReviewViewModel
    {
        private readonly List<Order> _allOrders;

        public OrdersReviewViewModel(List<Order> allOrders)
        {
            _allOrders = allOrders;
        }

        public Order SelectedOrder { get; set; }

        [UIHint("Order")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderLabel)]
        public int SelectedOrderId => SelectedOrder?.Id ?? 0;

        public List<Order> OrdersToMake => _allOrders.Where(e => !e.IsMade).ToList();

        public List<Order> OrdersToSend => _allOrders.Where(e => e.IsMade && !e.IsComplete).ToList();

        public List<OrderProduct> CombinedOrderProducts => GetAllOrderProducts();

        public List<OrderProduct> GetOrderProductsForProduct(int productId) => AllOrderProducts.Where(e => e.ProductId == productId).ToList();

        public List<OrderProductPack> GetOrderProductPacksForProductPack(int productPackId) => AllOrderProductPacks.Where(e => e.ProductPackId == productPackId).ToList();

        private List<OrderProduct> AllOrderProducts => OrdersToMake.SelectMany(e => e.Products).ToList();
        private List<Product> AllProducts => AllOrderProducts.Select(e => e.Product).ToList();

        private List<OrderProductPack> AllOrderProductPacks => OrdersToMake.SelectMany(e => e.ProductPacks).ToList();
        private List<ProductPack> AllProductPacks => AllOrderProductPacks.Select(e => e.ProductPack).ToList();

        private List<OrderProduct> GetAllOrderProducts()
        {
            var combinedProducts = AllProducts.Concat(AllProductPacks.SelectMany(e => e.Products.Select(p => p.Product)));

            var combinedProductsGrouped =
                combinedProducts.GroupBy(e => e.Id)
                .Select(group => new
                {
                    Product = AllProducts.FirstOrDefault(e => e.Id == group.Key),
                    Count = group.Count(),
                    CompleteCount = AllOrderProducts.Where(e => e.ProductId == group.Key).Sum(e => e.AmountCompleted) +
                               AllOrderProductPacks.Where(e => e.ProductPack.Products.Select(j => j.Id).Contains(group.Key)).Sum(i => i.AmountCompleted)
                })
                .ToList();

            var results = new List<OrderProduct>();
            foreach (var group in combinedProductsGrouped)
            {
                var newOrderProduct = new OrderProduct
                {
                    Product = group.Product,
                    ProductId = group.Product.Id,
                    Amount = group.Count,
                    AmountCompleted = group.CompleteCount
                };
                results.Add(newOrderProduct);
            }

            return results;
        }
    }
}