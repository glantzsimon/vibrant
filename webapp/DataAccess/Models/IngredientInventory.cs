using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.IngredientInventory, PluralName = Globalisation.Strings.Names.IngredientInventory, Name = Globalisation.Strings.Names.IngredientInventoryItem)]
    public class IngredientInventory : ObjectBase
	{
	    [UIHint("Ingredient")]
	    [ForeignKey("Ingredient")]
	    public int IngredientId { get; set; }

	    public virtual Ingredient Ingredient { get; set; }

	    [LinkedColumn(LinkedTableName = "Ingredient", LinkedColumnName = "Name")]
	    public string IngredientName { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantityInStockLabel)]
	    public int QuantityInStock { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InStockLabel)]
	    public bool IsInStock => QuantityInStock > 0;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerIngredientLabel)]
		public int AmountPerIngredient { get; set; }
        
		[Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NotesLabel)]
		[Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
		[StringLength(int.MaxValue)]
		[DataType(DataType.Html)]
		[AllowHtml]
		public string Notes { get; set; }
        
	}
}
