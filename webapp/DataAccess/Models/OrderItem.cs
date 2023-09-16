using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderItems, PluralName = Globalisation.Strings.Names.OrderItems, Name = Globalisation.Strings.Names.OrderItem)]
    public class OrderItem : ObjectBase
    {
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        public string UserName { get; set; }
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [UIHint("OrderItemProducts")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.ProductsLabel)]
        public int ProductPackProductsId => Id;

        public virtual IEnumerable<OrderItemProduct> OrderItemProducts { get; set; }

        [NotMapped]
        public List<OrderItemProduct> Products { get; set; }

        public virtual IEnumerable<OrderItemProductPack> OrderItemProductPacks { get; set; }

        [NotMapped]
        public List<OrderItemProductPack> ProductPacks { get; set; }

    }
}
