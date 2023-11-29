using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using K9.DataAccessLayer.Helpers;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderProducts, PluralName = Globalisation.Strings.Names.OrderProducts, Name = Globalisation.Strings.Names.OrderProduct)]
    public class OrderProduct : ObjectBase
    {
        [UIHint("PriceTier")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceTierLabel)]
        public EPriceTier PriceTier { get; set; }

        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
        public string ProductName { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int Amount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountCompletedLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int AmountCompleted { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRemainingLabel)] 
        public int AmountRemaining => Amount - AmountCompleted;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double GetPrice() => GetPriceWithDiscount();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string GetFormattedPrice() =>
            double.Parse(GetPrice().ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        private double GetPriceWithDiscount()
        {
            if (Product != null)
            {
                switch (PriceTier)
                {
                    case EPriceTier.Discount1:
                        return Product.PriceDiscount1;

                    case EPriceTier.Discount2:
                        return Product.PriceDiscount2;

                    case EPriceTier.SmallPack:
                        return Product.PriceSmallPack;

                    case EPriceTier.SmallPackDiscount1:
                        return Product.PriceSmallPackDiscount1;

                    case EPriceTier.SmallPackDiscount2:
                        return Product.PriceSmallPackDiscount2;

                    default:
                        return Product?.Price ?? 0;
                }
            }

            return 0;
        }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalPrice => Amount * GetPrice();

        [UIHint("InternationalCurrency")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalInternationalPrice => TotalPrice.ToInternationalPrice();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double FullTotalPrice => Amount * Product?.Price ?? 0;
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        public string GetFormattedTotalPrice() =>
            double.Parse(TotalPrice.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        public string GetFormattedAmount() => $"{Amount} {GetPackageType()}";

        private string GetPackageType() => GetPackageTypeText();

        private string GetPackageTypeText()
        {
            switch (Product.ProductType)
            {
                case EProductType.Liquid:
                    return Amount == 1 ? Globalisation.Dictionary.Bottle : Globalisation.Dictionary.Bottles;

                default:
                    return Amount == 1 ? Globalisation.Dictionary.Pack : Globalisation.Dictionary.Packs;
            }
        }
    }
}
