using System;
using System.ComponentModel.DataAnnotations;

namespace K9.WebApplication.Models
{
    public class OrderItem
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ContactLabel)]
        public string ContactName { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InvoiceDateLabel)]
        public DateTime CreatedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InvoiceNumbersLabel)]
        public string InvoiceNumbersText { get; set; }

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

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.GrandTotalLabel)]
        public string FormattedGrandTotal { get; set; }
    }
}