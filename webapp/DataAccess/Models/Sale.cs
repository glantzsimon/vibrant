using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Sales, PluralName = Globalisation.Strings.Names.Sales, Name = Globalisation.Strings.Names.Sale)]
    public class Sale : ObjectBase
	{
	    [UIHint("IngredientType")]
	    [Required]
	    [Display(ResourceType = typeof(Globalisation.Dictionary),
	        Name = Globalisation.Strings.Labels.IngredientTypeLabel)]
	    public ESaleType SaleType { get; set; }


	    [UIHint("Contact")]
	    [ForeignKey("Contact")]
	    public int ContactId { get; set; }

	    public virtual Contact Contact { get; set; }
	    
	    [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [DataType(DataType.Currency)]
	    public double TotalPrice { get; set; }
        
	}
}
