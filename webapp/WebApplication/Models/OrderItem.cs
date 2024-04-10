using System;
using System.ComponentModel.DataAnnotations;

namespace K9.WebApplication.Models
{
    public class OrderItem
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderNumberLabel)]
        public string OrderNumber { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InvoiceLabel)]
        public string InvoiceNumber => $"{Globalisation.Dictionary.InvoiceLabel.ToUpper()} {OrderNumber}";

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ClientLabel)]
        public string ClientName { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InvoiceDateLabel)]
        public string InvoiceDate { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InvoiceNumbersLabel)]
        public string InvoiceNumbersText { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BulkDiscountLabel)]
        public string BulkDiscountText { get; set; }

        public string CustomDiscountText => Globalisation.Dictionary.CustomDiscount;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductsLabel)]
        public string ProductsList { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantitiesLabel)]
        public string QuantitiesList { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PricesListLabel)]
        public string PricesList { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalsLabel)]
        public string TotalsList { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTotalLabel)]
        public string FormattedSubTotal { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DiscountLabel)]
        public string FormattedDiscount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CustomDiscount)]
        public string FormattedCustomDiscount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        public string FormattedGrandTotal { get; set; }
    }
}