using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using K9.DataAccessLayer.Helpers;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Orders, PluralName = Globalisation.Strings.Names.Orders, Name = Globalisation.Strings.Names.Order)]
    public class Order : ObjectBase
    {
        public const int MaxInvoiceProductNameLength = 24;
        public const int OrderNumberRoot = 11111 + 9 + 7 + 3;

        [UIHint("Order")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderLabel)]
        public int OrderId => Id;

        public Guid ExternalId { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderLabel)]
        public string OrderNumber { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsOnHoldLabel)]
        public bool IsOnHold { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShortDescriptionLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.FullNameLabel)]
        public string FullName { get; set; }

        public string GetFullName() => $"{Name} - {RequestedOn.ToShortDateString()} - {OrderStatusText}";

        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        [Display(ResourceType = typeof(K9.Globalisation.Dictionary), Name = K9.Globalisation.Strings.Labels.ConsultantLabel)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        [Display(ResourceType = typeof(K9.Globalisation.Dictionary), Name = K9.Globalisation.Strings.Labels.ConsultantLabel)]
        public string UserName { get; set; }

        [UIHint("Client")]
        [ForeignKey("Client")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        [LinkedColumn(LinkedTableName = "Client", LinkedColumnName = "FullName")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public string ClientName { get; set; }

        [UIHint("Client")]
        [ForeignKey("Rep")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RepLabel)]
        public int? RepId { get; set; }

        public virtual Client Rep { get; set; }

        [UIHint("OrderType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderTypeLabel)]
        public EOrderType OrderType { get; set; } = EOrderType.Sale;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RequestedOnLabel)]
        [Required]
        public DateTime RequestedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.StartedOnLabel)]
        public DateTime? StartedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.DueByLabel)]
        public DateTime? DueBy { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsCompleteLabel)]
        public bool IsOverdue => DueBy != null && DateTime.Today > DueBy;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.MadeOnLabel)]
        public DateTime? MadeOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsMadeLabel)]
        public bool IsMade => MadeOn != null && MadeOn <= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CompletedOnLabel)]
        public DateTime? CompletedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsCompleteLabel)]
        public bool IsComplete => CompletedOn != null && CompletedOn <= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.PaidOnLabel)]
        public DateTime? PaidOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsPaidLabel)]
        public bool IsPaid => PaidOn != null && PaidOn <= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderStatusLabel)]
        public EOrderStatus GetOrderStatus() => CalculateOrderStatus();

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderStatusLabel)]
        public string OrderStatusText => GetOrderStatus().GetAttribute<EnumDescriptionAttribute>().GetDescription();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShippingLabel)]
        [DataType(DataType.Currency)]
        public double ShippingCost { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShippingLabel)]
        public string GetFormattedShippingCost() =>
            double.Parse(ShippingCost.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double GetTotalPrice() => GetTotalProductsPrice() + GetTotalProductPacksPrice() + ShippingCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.FullPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetFullTotalPrice() => GetFullTotalProductsPrice() + GetFullTotalProductPacksPrice() + ShippingCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string GetFormattedTotalPrice() =>
            double.Parse(GetTotalPrice().ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalProductsPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalProductsPrice { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalProductsPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetTotalProductsPrice() => Products?.Sum(e => e.TotalPrice) ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalFullProductsPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetFullTotalProductsPrice() => Products?.Sum(e => e.FullTotalPrice) ?? 0;

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.TotalProductPacksPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalProductPacksPrice { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.TotalProductPacksPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetTotalProductPacksPrice() => ProductPacks?.Sum(e => e.TotalPrice) ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.TotalFullProductPacksPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetFullTotalProductPacksPrice() => ProductPacks?.Sum(e => e.FullTotalPrice) ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public double GetSuggestedDiscount() =>
            Methods.RoundToInteger(GetTotalPrice() > 0 ? GetTotalPrice().GetSuggestedBulkDiscount() : 0, 100);

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public string FormattedSuggestedDiscountAsPercent { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public string GetFormattedSuggestedDiscountAsPercent() =>
            (GetSuggestedDiscount() / 100).ToString("P0", CultureInfo.InvariantCulture);

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public double GetSuggestedDiscountAmount() => GetTotalPrice() * (GetSuggestedDiscount() / 100);

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public string FormattedSuggestedDiscountAmount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public string GetFormattedSuggestedDiscountAmount() => double.Parse(GetSuggestedDiscount().ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [UIHint("Percentage")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public double? Discount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string GetFormattedDiscountAsPercent() => (Discount / 100)?.ToString("P0", CultureInfo.InvariantCulture);

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        [DataType(DataType.Currency)]
        public double DiscountAmount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        [DataType(DataType.Currency)]
        public double GetDiscountAmount() => GetTotalPrice() * (Discount / 100 ?? 0);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        [DataType(DataType.Currency)]
        public double GetTotalTierDiscount() => GetFullTotalPrice() - GetTotalPrice();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string GetFormattedDiscountAmount() =>
            double.Parse(GetDiscountAmount().ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        [DataType(DataType.Currency)]
        public double GrandTotal { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        [DataType(DataType.Currency)]
        public double GetGrandTotal() => GetTotalPrice() - GetDiscountAmount();

        public int GetTotalProducts() => Products?.Sum(e => e.Amount) ?? 0;

        public int GetTotalProductPacks() => ProductPacks?.Sum(e => e.Amount) ?? 0;

        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }

        #region Invoice

        public string GetInvoiceNumbersText() => GetAllInvoiceNumbersText();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductsLabel)]
        public string GetProductsList() =>
            GetAllOrderedProductsIncludingShipping().Select(e => GetMaxProductNameLength(e.Product.Name)).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantitiesLabel)]
        public string GetQuantitiesList() => GetAllOrderedProductsIncludingShipping().Select(e => e.Amount.ToString()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PricesListLabel)]
        public string GetPricesList() => GetAllOrderedProductsIncludingShipping().Select(e => e.GetPrice().ToCurrency()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalsLabel)]
        public string GetTotalsList() => GetAllOrderedProductsIncludingShipping().Select(e => e.TotalPrice.ToCurrency()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTotalLabel)]
        public string GetFormattedSubTotal() => GetFullTotalPrice().ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string GetFormattedDiscount() => GetDiscountAmount().ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string GetFormattedTotalDiscount() => (GetDiscountAmount() + GetTotalTierDiscount()).ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        public string GetFormattedGrandTotal() => GetGrandTotal().ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ClientLabel)]
        public string GetBulkDiscountText() =>
            $"{GetFormattedDiscountAsPercent()} {Globalisation.Dictionary.BulkDiscountLabel}";

        private int GetTotalItems() => Products?.Count ?? 0 + ProductPacks?.Count ?? 0;

        private string GetAllInvoiceNumbersText()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= GetTotalItems(); i++)
            {
                sb.AppendLine(i.ToString().Trim());
            }
            return sb.ToString().Trim();
        }

        private List<OrderProduct> GetAllOrderedProductsIncludingShipping()
        {
            var items = GetCombinedGroupedProducts().OrderBy(e => e.Name).ToList() ?? new List<OrderProduct>();
            if (ShippingCost > 0)
            {
                var shippingItem = new OrderProduct
                {
                    Product = new Product
                    {
                        Name = Globalisation.Dictionary.ShippingLabel,
                        Price = ShippingCost
                    },
                    Amount = 1
                };
                items.Add(shippingItem);
            }
            return items;
        }

        private string GetMaxProductNameLength(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var maxLength = value.Length > MaxInvoiceProductNameLength ? MaxInvoiceProductNameLength : value.Length;
            return value.Substring(0, maxLength);
        }

        #endregion

        [NotMapped]
        public List<OrderProduct> Products { get; set; }

        public virtual IEnumerable<OrderProductPack> OrderProductPacks { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShowCompletedLabel)]
        [NotMapped]
        public bool ShowCompleted { get; set; }

        [NotMapped]
        public List<OrderProductPack> ProductPacks { get; set; }

        public bool AreProductsReady() => !Products?.Any(e => e.AmountRemaining > 0) ?? true;

        public bool AreProductPacksReady() => !ProductPacks?.Any(e => e.AmountRemaining > 0) ?? true;

        public bool AreAllItemsReady() => AreProductsReady() && AreProductPacksReady();

        public List<OrderProduct> GetCombinedGroupedProducts()
        {
            var combinedProductsGrouped =
                GetCombinedProducts().GroupBy(e => e.Id)
                    .Select(group =>
                    {
                        var groupOrderProducts = Products.Where(e => e.ProductId == group.Key).ToList();
                        var groupOrderProductPacks = ProductPacks.Select(o => new
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

            return results;
        }

        private List<Product> GetCombinedProducts() => GetAllProducts().Concat(GetAllProductPackProducts()).ToList();

        private List<Product> GetAllProducts() => Products?.Select(e => e.Product).ToList() ?? new List<Product>();

        private List<Product> GetAllProductPackProducts() => ProductPacks.Where(e => e.ProductPack.Products != null && e.ProductPack.Products.Any()).SelectMany(e => e.ProductPack.Products.Select(p => p.Product)).ToList();

        private EOrderStatus CalculateOrderStatus()
        {
            if (!StartedOn.HasValue)
            {
                return EOrderStatus.InPreparation;
            }

            if (!MadeOn.HasValue)
            {
                return EOrderStatus.InProgress;
            }

            if (!CompletedOn.HasValue)
            {
                return EOrderStatus.ReadyForDelivery;
            }

            if (!IsPaid)
            {
                return EOrderStatus.AwaitingPayment;
            }

            return EOrderStatus.Complete;
        }
    }
}
