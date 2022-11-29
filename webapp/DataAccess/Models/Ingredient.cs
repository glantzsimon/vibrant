using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Ingredients, PluralName = Globalisation.Strings.Names.Ingredients, Name = Globalisation.Strings.Names.Ingredient)]
    public class Ingredient : ObjectBase
	{
	    [UIHint("IngredientType")]
	    [Required]
	    [Display(ResourceType = typeof(Globalisation.Dictionary),
	        Name = Globalisation.Strings.Labels.IngredientTypeLabel)]
	    public EIngredientType IngredientType { get; set; }

	    public string MeasuredIn => GetMeasuredInText();
	    
	    public string MeasuredInForLargeQuantity => GetMeasuredInForLargeQuantityText();
		
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShortDescriptionLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string ShortDescription { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BodyLabel)]
		[Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
		[StringLength(int.MaxValue)]
		[DataType(DataType.Html)]
		[AllowHtml]
		public string Body { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string Benefits { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubjectiveEffectsLabel)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string SubjectiveEffects { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SideEffectsLabel)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string SideEffects { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DeficiencySymptomsLabel)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string DeficiencySymptoms { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ResearchLabel)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string Research { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [DataType(DataType.Currency)]
	    public double Cost { get; set; }

        [UIHint("Quantity")]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantityLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    public int Quantity { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary),
	        Name = Globalisation.Strings.Labels.QuantityInStockLabel)]
	    public string FormattedQuantity => $"{Quantity} {MeasuredIn}";

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostPer100GramsLabel)]
	    [DataType(DataType.Currency)]
	    public double CostPer100Grams => (100f / Quantity) * Cost;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostPerMilligramLabel)]
	    [DataType(DataType.Currency)]
	    public double CostPerMilligram => CostPer100Grams / 100000;

	    [FileSourceInfo("upload/products", Filter = EFilesSourceFilter.Images)]
		[Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadImages)]
		public FileSource ImageFileSource { get; set; }

	    [FileSourceInfo("upload/products", Filter = EFilesSourceFilter.Videos)]
	    [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadVideo)]
	    public FileSource VideoFileSource { get; set; }
        
	    [StringLength(512)]
	    [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ImageUrlLabel)]
	    public string ImageUrl { get; set; }

	    [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AdditionalCssClassesLabel)]
	    public string AdditionalCssClasses { get; set; }

	    [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SeoFriendlyIdLabel)]
	    public string SeoFriendlyId { get; set; }

	    [StringLength(512)]
	    [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.VideoUrlLabel)]
	    public string VideoUrl { get; set; }

	    [StringLength(512)]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PurchaseUrlsLabel)]
	    public string PurchaseUrls { get; set; }

	    private string GetMeasuredInText()
	    {
	        switch (IngredientType)
	        {
	            case EIngredientType.Liquid:
	                return Globalisation.Strings.Constants.Measures.Millilitres;

	            case EIngredientType.Powder:
	                return Globalisation.Strings.Constants.Measures.Milligrams;
                    
	            default:
	                return string.Empty;

	        }
	    }

	    private string GetMeasuredInForLargeQuantityText()
	    {
	        switch (IngredientType)
	        {
	            case EIngredientType.Liquid:
	                return Globalisation.Strings.Constants.Measures.Litres;

	            case EIngredientType.Powder:
	                return Globalisation.Strings.Constants.Measures.Grams;
                    
	            default:
	                return string.Empty;

	        }
	    }
	}
}
