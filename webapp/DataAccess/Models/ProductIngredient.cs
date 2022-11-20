using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Products, PluralName = Globalisation.Strings.Names.Products, Name = Globalisation.Strings.Names.Product)]
    public class ProductIngredient : ObjectBase
	{
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

	    [UIHint("Ingredient")]
	    [ForeignKey("Ingredient")]
	    public int IngredientId { get; set; }

	    public virtual Product Product { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string Dosage { get; set; }
        
	}
}
