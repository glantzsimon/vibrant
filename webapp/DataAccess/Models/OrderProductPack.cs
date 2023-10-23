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
        public int Amount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountCompletedLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int AmountCompleted { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountCompletedLabel)]
        public int GetAmountRemaining() => Amount - AmountCompleted;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double GetPrice() => GetPriceWithDiscount();

        private double GetPriceWithDiscount()
        {
            switch (PriceTier)
            {
                case EPriceTier.Discount1:
                    return ProductPack.PriceDiscount1;

                case EPriceTier.Discount2:
                    return ProductPack.PriceDiscount2;

                default:
                    return ProductPack?.Price ?? 0;
            }
        }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double GetTotalPrice() => Amount * ProductPack?.Price ?? 0;

        public string GetFormattedAmount() => $"{Amount} {GetPackageType()}";

        private string GetPackageType() => GetPackageTypeText();

        private string GetPackageTypeText()
        {
            return Amount == 1 ? Globalisation.Dictionary.Pack : Globalisation.Dictionary.Packs;
        }
    }
}
