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
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Products, PluralName = Globalisation.Strings.Names.Products, Name = Globalisation.Strings.Names.Product)]
    public class Ingredient : ObjectBase
	{
	    [UIHint("IngredientType")]
	    [Required]
	    [Display(ResourceType = typeof(Globalisation.Dictionary),
	        Name = Globalisation.Strings.Labels.IngredientTypeLabel)]
	    public EIngredientType IngredientType { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TitleLabel)]
		[Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
		[StringLength(256)]
		public string Title { get; set; }
		
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

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    [StringLength(int.MaxValue)]
	    [DataType(DataType.Html)]
	    [AllowHtml]
	    public string Dosage { get; set; }

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
        
	}
}
