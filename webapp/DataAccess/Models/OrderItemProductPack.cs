using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderItemProductPack, PluralName = Globalisation.Strings.Names.OrderItemProductPacks, Name = Globalisation.Strings.Names.OrderItemProductPack)]
    public class OrderItemProductPack : ObjectBase
    {
        [UIHint("ProductPack")]
        [ForeignKey("ProductPack")]
        public int ProductPackId { get; set; }

        public virtual ProductPack ProductPack { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductPackLabel)]
        [LinkedColumn(LinkedTableName = "ProductPack", LinkedColumnName = "Name")]
        public string ProducPacktName { get; set; }

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
            return Amount == 1 ? Globalisation.Dictionary.Pack : Globalisation.Dictionary.Packs;
        }
    }
}
