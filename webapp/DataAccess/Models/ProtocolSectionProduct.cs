using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProtocolProtocolSectionProducts, PluralName = Globalisation.Strings.Names.ProtocolProtocolSectionProducts, Name = Globalisation.Strings.Names.ProtocolProtocolSectionProduct)]
    public class ProtocolSectionProduct : ObjectBase
    {
        [ForeignKey("ProtocolSection")]
        public int ProtocolSectionId { get; set; }

        public virtual ProtocolSection ProtocolSection { get; set; }
        
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
        public string ProductName { get; set; }

        [UIHint("Quantity")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int Amount { get; set; }
    }
}
