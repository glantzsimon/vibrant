using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using K9.SharedLibrary.Attributes;

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

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
	    [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
	    public string ProductName { get; set; }

	    [UIHint("Ingredient")]
	    [ForeignKey("Ingredient")]
	    public int IngredientId { get; set; }

	    public virtual Ingredient Ingredient { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IngredientLabel)]
	    [LinkedColumn(LinkedTableName = "Ingredient", LinkedColumnName = "Name")]
	    public string IngredientName { get; set; }
        
	    [UIHint("Quantity")]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    public float Amount { get; set; }

	    [NotMapped]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BatchSizeLabel)]
	    public int BatchSize { get; set; } = 1;

	    [UIHint("Quantity")]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary),
	        ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    public float AmountPerBatch => Amount * BatchSize;

	    [UIHint("Quantity")]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    public float AmountPerConcentration => Amount * (1 / (Ingredient?.Concentration ?? 1));

	    [UIHint("Quantity")]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    public float AmountPerConcentrationPerBatch => AmountPerBatch * (1 / (Ingredient?.Concentration ?? 1));

	    public string FormattedAmount =>
	        $"{Amount} {Globalisation.Strings.Constants.Measures.Milligrams}";
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerBatchLabel)]
	    public float AmountPer100Capsules => AmountPerConcentrationPerBatch * 100;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerBatchLabel)]
	    public string FormattedAmountPer100Capsules =>
	        $"{AmountPer100Capsules} {Ingredient?.MeasuredIn}";

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerBatchLabel)]
	    public string FormattedLargeAmountPer100Capsules =>
	        $"{AmountPerConcentrationPerBatch / 10} {Ingredient?.MeasuredInForLargeQuantity}";

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
	    [DataType(DataType.Currency)]
	    public double Cost => Amount * Ingredient?.CostPerMilligram ?? 0;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
	    [DataType(DataType.Currency)]
	    public double CostPer100Capsules => Cost * 100;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
	    public float? PercentageOfDailyAllowance => AmountPerConcentration / Ingredient?.RecommendedDailyAllownace;

	    public string FormattedPercentageOfDailyAllowance => PercentageOfDailyAllowance?.ToString("P0") ?? "*";
	}
}
