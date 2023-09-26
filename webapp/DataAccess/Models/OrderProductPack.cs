using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using K9.DataAccessLayer.Enums;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderProductPack, PluralName = Globalisation.Strings.Names.OrderProductPacks, Name = Globalisation.Strings.Names.OrderProductPack)]
    public class OrderProductPack : ObjectBase
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceTierLabel)]
        public EPriceTier PriceTier { get; set; }

        [UIHint("ProductPack")]
        [ForeignKey("ProductPack")]
        public int ProductPackId { get; set; }

        public virtual ProductPack ProductPack { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductPackLabel)]
        [LinkedColumn(LinkedTableName = "ProductPack", LinkedColumnName = "Name")]
        public string ProducPacktName { get; set; }

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
                    return ProductPack?.PriceDiscount1 ?? 0;

                case EPriceTier.Discount2:
                    return ProductPack?.PriceDiscount2 ?? 0;

                default:
                    return ProductPack?.Price ?? 0;
            }
        }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double TotalPrice => Amount * ProductPack?.Price ?? 0;

        public string FormattedAmount =>
            $"{Amount} {PackageType}";

        private string PackageType => GetPackageType();

        private string GetPackageType()
        {
            return Amount == 1 ? Globalisation.Dictionary.Pack : Globalisation.Dictionary.Packs;
        }
    }
}
