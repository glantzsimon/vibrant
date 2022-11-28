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
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Sales, PluralName = Globalisation.Strings.Names.Sales, Name = Globalisation.Strings.Names.Sale)]
    public class SaleItem : ObjectBase
    {
        [Required]
        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        public string UserName { get; set; }
        
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

    }
}
