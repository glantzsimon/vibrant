﻿using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProductIngredients, PluralName = Globalisation.Strings.Names.ProductIngredients, Name = Globalisation.Strings.Names.ProductIngredient)]
    public class ProductIngredient : ObjectBase
	{
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

	    [UIHint("Ingredient")]
	    [ForeignKey("Ingredient")]
	    public int IngredientId { get; set; }

	    public virtual Ingredient Ingredient { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    public float Amount { get; set; }

	    public string FormattedAmount =>
	        $"{Amount} {Globalisation.Strings.Constants.Measures.Milligrams}";

	    public float AmountPer100Capsules => Amount * 100;

	    public string FormattedAmountPer100Capsules =>
	        $"{AmountPer100Capsules} {Ingredient?.MeasuredInForLargeQuantity}";

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
	    [DataType(DataType.Currency)]
	    public double Cost => (Amount / 100f) * Ingredient?.CostPer100Grams ?? 0;
	}
}