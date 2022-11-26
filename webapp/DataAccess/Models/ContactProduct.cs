using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ContactProducts, PluralName = Globalisation.Strings.Names.ContactProducts, Name = Globalisation.Strings.Names.ContactProduct)]
    public class ContactProduct : ObjectBase
	{
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

	    [UIHint("Contact")]
	    [ForeignKey("Contact")]
	    public int ContactId { get; set; }

	    public virtual Contact Contact { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [DataType(DataType.Currency)]
	    public double Price { get; set; }
	}
}
