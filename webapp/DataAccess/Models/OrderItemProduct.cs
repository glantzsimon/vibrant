using System;
using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderItemProducts, PluralName = Globalisation.Strings.Names.OrderItemProducts, Name = Globalisation.Strings.Names.OrderItemProduct)]
    public class OrderItemProduct : ObjectBase
    {
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
        public string ProductName { get; set; }

        [UIHint("OrderItem")]
        [ForeignKey("OrderItem")]
        public int OrderItemId { get; set; }

        public virtual OrderItem OrderItem { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.OrderItemLabel)]
        [LinkedColumn(LinkedTableName = "OrderItem", LinkedColumnName = "Name")]
        public string OrderItemName { get; set; }

        [UIHint("Quantity")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public float Amount { get; set; }

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
