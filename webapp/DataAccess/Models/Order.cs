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

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public string ContactName { get; set; }

        [UIHint("Contact")]
        [ForeignKey("Rep")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RepLabel)]
        public int? RepId { get; set; }

        public virtual Contact Rep { get; set; }

        [UIHint("OrderType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderTypeLabel)]
        public EOrderType OrderType { get; set; }

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
        public bool IsMade => MadeOn != null && MadeOn >= DateTime.Today;

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
        public EOrderStatus OrderStatus => GetOrderStatus();

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderStatusLabel)]
        public string OrderStatusText => OrderStatus.GetAttribute<EnumDescriptionAttribute>().GetDescription();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShippingLabel)]
        [DataType(DataType.Currency)]
        public double ShippingCost { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShippingLabel)]
        public string FormattedShippingCost => double.Parse(ShippingCost.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalPrice => TotalProductsPrice + TotalProductPacksPrice + ShippingCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string FormattedTotalPrice => double.Parse(TotalPrice.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalProductsPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalProductsPrice => Products?.Sum(e => e.TotalPrice) ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalProductPacksPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalProductPacksPrice => ProductPacks?.Sum(e => e.TotalPrice) ?? 0;
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        [UIHint("Percentage")]
        public double SuggestedDiscount => Methods.RoundToInteger(TotalPrice > 0 ? TotalPrice.GetSuggestedBulkDiscount() : 0, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public string FormattedSuggestedDiscountAsPercent => (SuggestedDiscount / 100).ToString("P0", CultureInfo.InvariantCulture);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedBulkDiscountLabel)]
        public double SuggestedDiscountAmount => TotalPrice * (SuggestedDiscount / 100);

        [UIHint("Percentage")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public double? Discount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string FormattedDiscountAsPercent => (Discount / 100)?.ToString("P0", CultureInfo.InvariantCulture);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        [DataType(DataType.Currency)]
        public double DiscountAmount => TotalPrice * (Discount / 100 ?? 0);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string FormattedDiscountAmount => double.Parse(DiscountAmount.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        [DataType(DataType.Currency)]
        public double GrandTotal => TotalPrice - DiscountAmount;

        public int TotalProducts => Products?.Sum(e => e.Amount) ?? 0;

        public int TotalProductPacks => ProductPacks?.Sum(e => e.Amount) ?? 0;

        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }

        #region Invoice

        private int TotalItems => Products?.Count ?? 0 + ProductPacks?.Count ?? 0;

        public string InvoiceNumbersText => GetInvoiceNumbersText();

        private string GetInvoiceNumbersText()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= TotalItems; i++)
            {
                sb.AppendLine(i.ToString().Trim());
            }
            return sb.ToString().Trim();
        }

        private List<OrderProduct> GetOrderedProducts()
        {
            return Products?.OrderBy(e => e.Product.Name).ToList() ?? new List<OrderProduct>();
        }

        private string GetMaxProductNameLength(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var maxLength = value.Length > MaxInvoiceProductNameLength ? MaxInvoiceProductNameLength : value.Length;
            return value.Substring(0, maxLength);
        }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductsLabel)]
        public string ProductsList => GetOrderedProducts().Select(e => GetMaxProductNameLength(e.Product.Name)).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantitiesLabel)]
        public string QuantitiesList => GetOrderedProducts().Select(e => e.Amount.ToString()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PricesListLabel)]
        public string PricesList => GetOrderedProducts().Select(e => e.Price.ToCurrency()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalsLabel)]
        public string TotalsList => GetOrderedProducts().Select(e => e.TotalPrice.ToCurrency()).ToDisplayList();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTotalLabel)]
        public string FormattedSubTotal => TotalPrice.ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string FormattedDiscount => DiscountAmount.ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        public string FormattedGrandTotal => GrandTotal.ToCurrency();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ContactLabel)]
        public string BulkDiscountText => $"{FormattedDiscountAsPercent} {Globalisation.Dictionary.BulkDiscountLabel}";

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

        private EOrderStatus GetOrderStatus()
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
