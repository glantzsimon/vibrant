using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.WebApplication.ViewModels
{
    public static partial class OrdersExtensions
    {
        public static List<Order> OrderedByDate(this IEnumerable<Order> items)
        {
            return items.OrderByDescending(e => e.CreatedOn).ToList();
        }
    }

    public class OrdersReviewViewModel
    {
        public readonly List<Order> AllOrders;
        public readonly List<Order> AllDirectOrders;
        public readonly List<Order> AllActiveOrders;

        public OrdersReviewViewModel(IEnumerable<Order> allOrders)
        {
            AllOrders = allOrders.Where(e => e.OrderType != EOrderType.ShoppingCart).ToList();
            AllDirectOrders = allOrders.Where(e => e.OrderType != EOrderType.ShopProvision).ToList();
            AllActiveOrders = allOrders.Where(e => !e.IsOnHold && e.OrderType != EOrderType.ShopProvision).ToList();
        }

        public Order SelectedOrder { get; set; }

        [UIHint("Order")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderLabel)]
        public int SelectedOrderId => SelectedOrder?.Id ?? 0;

        public List<Order> GetShopProvisionOrders() => AllOrders.Where(e => !e.IsOnHold && e.OrderType == EOrderType.ShopProvision).OrderedByDate();
        
        public List<Order> GetRestockOrders() => AllOrders.Where(e => !e.IsOnHold && e.OrderType == EOrderType.Restock).OrderedByDate();

        public List<Order> GetRedeemableOrders() => AllOrders.Where(e => !e.IsOnHold && e.RepId.HasValue).OrderedByDate();

        public List<Order> GetOrdersOnHold() => AllDirectOrders.Where(e => e.IsOnHold).OrderedByDate();

        public List<Order> GetIncompleteOrders() => AllActiveOrders.Where(e => !e.IsComplete).OrderedByDate();

        public List<Order> GetPickslipOrders() => AllOrders.Where(e => !e.IsComplete && !e.IsOnHold && e.OrderType != EOrderType.Restock && e.OrderType != EOrderType.Invoice && !e.IsDelivered).OrderedByDate();
        
        public List<Order> GetCompleteOrders() => AllActiveOrders.Where(e => e.IsComplete).OrderedByDate();
        
        public List<Order> GetOrdersToMake() => AllActiveOrders.Where(e => !e.IsMade).OrderedByDate();

        public List<Order> GetOrdersToSend() => AllActiveOrders.Where(e => e.IsMade && !e.IsComplete && e.OrderType != EOrderType.Restock).OrderedByDate();

        public List<OrderProduct> GetCombinedOrderProducts() => GetCombinedGroupedProducts();

        public List<OrderProduct> GetOrderProductsForProduct(int productId) => AllOrderProducts.Where(e => e.ProductId == productId).ToList();

        public List<OrderProductPack> GetOrderProductPacksForProductPack(int productPackId) => AllOrderProductPacks.Where(e => e.ProductPackId == productPackId).ToList();

        private List<OrderProduct> AllOrderProducts => GetOrdersToMake().SelectMany(e => e.Products.Where(p => p.Product.ProductType != EProductType.Other)).ToList();
        
        private List<Product> GetAllProducts() => AllOrderProducts?.Select(e => e.Product).ToList() ?? new List<Product>();
        
        private List<Product> GetAllProductPackProducts() => AllProductPacks.Where(e => e.Products != null && e.Products.Any()).SelectMany(e => e.Products.Select(p => p.Product)).ToList();    
        
        private List<Product> GetCombinedProducts() => GetAllProducts().Concat(GetAllProductPackProducts()).ToList();  

        private List<OrderProductPack> AllOrderProductPacks => GetOrdersToMake().SelectMany(e => e.ProductPacks).ToList();
        
        private List<ProductPack> AllProductPacks => AllOrderProductPacks?.Select(e => e.ProductPack).ToList() ?? new List<ProductPack>();

        private List<OrderProduct> GetCombinedGroupedProducts()
        {
            var combinedProductsGrouped =
                GetCombinedProducts().GroupBy(e => e.Id)
                .Select(group =>
                    {
                        var groupOrderProducts = AllOrderProducts.Where(e => e.ProductId == group.Key).ToList();
                        var groupOrderProductPacks = AllOrderProductPacks.Select(o => new
                        {
                            OrderProductPack = o,
                            Products = o.ProductPack.Products.Where(e => e.ProductId == group.Key)
                        }).ToList();

                        var groupItem = new
                        {
                            Product = GetCombinedProducts().FirstOrDefault(e => e.Id == group.Key),
                            
                            Count = groupOrderProducts.Sum(e => e.Amount) + 
                                    groupOrderProductPacks.Sum(e => e.OrderProductPack.Amount * e.Products.Sum(p => p.Amount)),

                            CompleteCount = groupOrderProducts.Sum(e => e.AmountCompleted) + 
                                            groupOrderProductPacks.Sum(e => e.OrderProductPack.AmountCompleted * e.Products.Sum(p => p.Amount)) + 
                                            groupOrderProductPacks.SelectMany(e => e.OrderProductPack.ProductPackProducts.Where(p => p.ProductId == group.Key)).Sum(o => o.AmountCompleted)};

                        return groupItem;
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

            return results.OrderBy(e => e.Product.Name).ToList();
        }
    }
}