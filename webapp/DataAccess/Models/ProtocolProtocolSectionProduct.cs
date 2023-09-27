using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProtocolProtocolSectionProducts, PluralName = Globalisation.Strings.Names.ProtocolProtocolSectionProducts, Name = Globalisation.Strings.Names.ProtocolProtocolSectionProduct)]
    public class ProtocolProtocolSectionProduct : ObjectBase
    {
        [ForeignKey("ProtocolProtocolSection")]
        public int ProtocolProtocolSectionId { get; set; }

        public virtual ProtocolProtocolSection ProtocolProtocolSection { get; set; }
        
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
        public string ProductName { get; set; }
    }
}
