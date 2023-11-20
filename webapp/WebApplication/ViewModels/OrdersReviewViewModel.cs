using K9.DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.WebApplication.ViewModels
{
    public class OrdersReviewViewModel
    {
        public readonly List<Order> AllOrders;
        public readonly List<Order> AllOrdersNotOnHold;

        public OrdersReviewViewModel(List<Order> allOrders)
        {
            AllOrders = allOrders;
            AllOrdersNotOnHold = allOrders.Where(e => !e.IsOnHold).ToList();
        }

        public Order SelectedOrder { get; set; }

        [UIHint("Order")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderLabel)]
        public int SelectedOrderId => SelectedOrder?.Id ?? 0;

        public List<Order> GetOrdersOnHold() => AllOrders.Where(e => e.IsOnHold).ToList();

        public List<Order> GetIncompleteOrders() => AllOrdersNotOnHold.Where(e => !e.IsComplete).ToList();
        
        public List<Order> GetCompleteOrders() => AllOrdersNotOnHold.Where(e => e.IsComplete).ToList();
        
        public List<Order> GetOrdersToMake() => AllOrdersNotOnHold.Where(e => !e.IsMade).ToList();

        public List<Order> GetOrdersToSend() => AllOrdersNotOnHold.Where(e => e.IsMade && !e.IsComplete).ToList();

        public List<OrderProduct> GetCombinedOrderProducts() => GetCombinedGroupedProducts();

        public List<OrderProduct> GetOrderProductsForProduct(int productId) => AllOrderProducts.Where(e => e.ProductId == productId).ToList();

        public List<OrderProductPack> GetOrderProductPacksForProductPack(int productPackId) => AllOrderProductPacks.Where(e => e.ProductPackId == productPackId).ToList();

        private List<OrderProduct> AllOrderProducts => GetOrdersToMake().SelectMany(e => e.Products).ToList();
        
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
                                            groupOrderProductPacks.Sum(e => e.OrderProductPack.AmountCompleted * e.Products.Sum(p => p.Amount)),
                        };

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