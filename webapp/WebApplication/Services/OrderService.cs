using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IRepository<Client> _clientsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<RepCommission> _repCommissionsRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductsRepository;
        private readonly IRepository<Country> _countriesRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrderService(
            ILogger logger, 
            IRepository<Order> ordersRepository, 
            IRepository<OrderProduct> orderProductsRepository, 
            IRepository<OrderProductPack> orderProductPacksRepository, 
            IRepository<Product> productsRepository, 
            IRepository<ProductPack> productPackRepository, 
            IOptions<DefaultValuesConfiguration> defaultValues, 
            IRepository<Client> clientsRepository, 
            IRepository<User> usersRepository, 
            IRepository<RepCommission> repCommissionsRepository, 
            IRepository<Ingredient> ingredientsRepository, 
            IRepository<Protocol> protocolsRepository, 
            IRepository<IngredientSubstitute> ingredientSubstitutesRepository, 
            IRepository<ProductIngredient> productIngredientsRepository, 
            IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository, 
            IRepository<Activity> activitiesRepository, 
            IRepository<DietaryRecommendation> dietaryRecommendationsRepository, 
            IRepository<ProductPackProduct> productPackProductsRepository, 
            IRepository<Country> countriesRepository, 
            IRepository<FoodItem> foodItemsRepository) : base(productsRepository, productPackRepository, ingredientsRepository, protocolsRepository, ingredientSubstitutesRepository, productIngredientsRepository, productIngredientSubstitutesRepository, activitiesRepository, dietaryRecommendationsRepository, productPackProductsRepository, foodItemsRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderProductPacksRepository = orderProductPacksRepository;
            _productsRepository = productsRepository;
            _productPackRepository = productPackRepository;
            _clientsRepository = clientsRepository;
            _usersRepository = usersRepository;
            _repCommissionsRepository = repCommissionsRepository;
            _productPackProductsRepository = productPackProductsRepository;
            _countriesRepository = countriesRepository;
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
            order.Client = _clientsRepository.Find(order.ClientId ?? 0);
            order.ClientName = order.Client?.FullName;
            order.Client.Country = _countriesRepository.Find(order.Client.CountryId ?? 0);
            order.User = _usersRepository.Find(order.UserId);
            order.UserName = order.User.Name;

            order.Products = _orderProductsRepository.Find(e => e.OrderId == order.Id).ToList();
            foreach (var orderProduct in order.Products)
            {
                orderProduct.Product = GetProducts().FirstOrDefault(e => e.Id == orderProduct.ProductId);
                orderProduct.PriceTier = order.Client.PriceTier;
            }

            order.ProductPacks = _orderProductPacksRepository.Find(e => e.OrderId == order.Id).ToList();
            foreach (var orderProductPack in order.ProductPacks)
            {
                orderProductPack.ProductPack = GetProductPacks().FirstOrDefault(e => e.Id == orderProductPack.ProductPackId);
                orderProductPack.PriceTier = order.Client.PriceTier;

                orderProductPack.ProductPack.Products = GetProductPackProducts()
                    .Where(e => e.ProductPackId == orderProductPack.ProductPackId).ToList();

                foreach (var productPackProduct in orderProductPack.ProductPack.Products)
                {
                    productPackProduct.Product = GetProducts().FirstOrDefault(e => e.ProductId == productPackProduct.ProductId);
                }
            }

            order.TotalPrice = order.GetTotalPrice();
            order.TotalProductsPrice = order.GetTotalProductsPrice();
            order.TotalInternationalProductsPrice = order.GetTotalInternationalProductsPrice();
            order.TotalProductPacksPrice = order.GetTotalProductPacksPrice();
            order.TotalInternationalProductPacksPrice = order.GetTotalInternationalProductPacksPrice();
            order.FormattedSuggestedDiscountAsPercent = order.GetFormattedSuggestedDiscountAsPercent();
            order.FormattedSuggestedDiscountAmount = order.GetFormattedSuggestedDiscountAmount();
            order.DiscountAmount = order.GetDiscountAmount();
            order.GrandTotal = order.GetGrandTotal();
            order.InternationalGrandTotal = order.GetInternationalGrandTotal();

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

        public Order UpdatePricesForClient(Order order)
        {
            if (!order.Products.Any(e => e.Amount > 0))
            {
                // New order - update price tier
                foreach (var product in order.Products.Where(e => e.Amount == 0))
                {
                    product.PriceTier = order.Client.PriceTier;
                }
            }

            if (!order.ProductPacks.Any(e => e.Amount > 0))
            {
                // New order - update price tier
                foreach (var pack in order.ProductPacks.Where(e => e.Amount == 0))
                {
                    pack.PriceTier = order.Client.PriceTier;
                }
            }

            return order;
        }

        public List<Order> ListForClient(int clientId)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(clientId), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));

                var orders = _ordersRepository.Find(e => !e.IsDeleted && e.ClientId == clientId).OrderBy(e => e.Name).ToList();

                var fullOrders = new List<Order>();
                foreach (var order in orders)
                {
                    fullOrders.Add(GetFullOrder(order));
                }

                return fullOrders;
            });
        }

        public List<Order> List(bool retrieveFullOrder = false)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(retrieveFullOrder), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));

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
                ClientId = order.ClientId,
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
            var repOrders = List(true).Where(e => (e.RepId == repId) && !e.IsOnHold && e.PaidOn.HasValue).ToList();
            var redeemedOrders = List(true).Where(e => (e.RepId == repId || e.ClientId == repId) && !e.IsOnHold && e.OrderType == EOrderType.RedeemCommission).ToList();

            var totalRedeemed = redeemedCommissions.Sum(e => e.AmountRedeemed) +
                                redeemedOrders.Where(e => e.OrderType == EOrderType.RedeemCommission).Sum(e => e.TotalPrice);

            var totalPrice = repOrders.SelectMany(e => e.Products).Sum(e => e.TotalPrice) +
                             repOrders.SelectMany(e => e.ProductPacks).Sum(e => e.TotalPrice);

            var totalRedeemable = (totalPrice / 10) - totalRedeemed;

            return new RepCommissionViewModel
            {
                AmountRedeemable = totalRedeemable,
                AmountRedeemed = totalRedeemed,
                RepId = repId,
                Rep = _clientsRepository.Find(repId),
                RepCommissions = redeemedCommissions,
                RedeemedOrders = redeemedOrders
            };
        }

        /// <summary>
        /// Update product pack amount and set to 1, if 0. This is the default behaviour when selecting product packs for the first time.
        /// </summary>
        public void UpdateProductPacksSetDefaultAmountIfZero(int orderId)
        {
            foreach (var orderProductPack in _orderProductPacksRepository.Find(e => e.OrderId == orderId).ToList())
            {
                if (orderProductPack.Amount == 0)
                {
                    orderProductPack.Amount = 1;
                    _orderProductPacksRepository.Update(orderProductPack);
                }
            }
        }

        public void UpdateOrderNumberIfEmpty(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                var lastOrder = _ordersRepository.CustomQuery<Order>($"SELECT TOP 1 * FROM [{nameof(Order)}] ORDER BY [Id] DESC").FirstOrDefault();
                var orderNumberCount = lastOrder?.Id + 3 + Order.OrderNumberRoot;
                var newOrderNumber = $"PA-{orderNumberCount}";
                var maxTries = 11;
                var tryIndex = 0;
                
                while(_ordersRepository.Exists(e => e.OrderNumber == newOrderNumber) && tryIndex <= maxTries)
                {
                    orderNumberCount++;
                    newOrderNumber = $"PA-{orderNumberCount}";
                    tryIndex++;
                }

                order.OrderNumber = newOrderNumber;
            }
        }
    }
}