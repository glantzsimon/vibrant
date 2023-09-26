using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double Price => GetPrice();

        private double GetPrice()
        {
            switch (PriceTier)
            {
                case EPriceTier.Discount1:
                    return Product?.PriceDiscount1 ?? 0;

                case EPriceTier.Discount2:
                    return Product?.PriceDiscount2 ?? 0;

                case EPriceTier.SmallPack:
                    return Product?.PriceSmallPack ?? 0;

                case EPriceTier.SmallPackDiscount1:
                    return Product?.PriceSmallPackDiscount1 ?? 0;

                case EPriceTier.SmallPackDiscount2:
                    return Product?.PriceSmallPackDiscount2 ?? 0;

                default:
                    return Product?.Price ?? 0;
            }
        }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalPrice => Amount * Product?.Price ?? 0;

        public string FormattedAmount =>
            $"{Amount} {PackageType}";

        private string PackageType => GetPackageType();

        private string GetPackageType()
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
