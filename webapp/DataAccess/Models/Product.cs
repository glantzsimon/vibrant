using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Products, PluralName = Globalisation.Strings.Names.Products, Name = Globalisation.Strings.Names.Product)]
    public class Product : ObjectBase
    {
        [UIHint("ProductType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.ProductTypeLabel)]
        public EProductType ProductType { get; set; }

        public string MeasuredIn => GetMeasuredInText();
        
        public string MeasuredInForLargeQuantity => GetMeasuredInForLargeQuantityText();

        [ForeignKey("Contact")]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        public virtual IEnumerable<ProductIngredient> ProductIngredients { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsLiveOnLabel)]
        public DateTime IsLiveOn { get; set; }

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

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostLabel)]
        [DataType(DataType.Currency)]
        public double Cost => ProductIngredients?.Sum(e => e.Cost) ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double SuggestedRetailPrice => Cost * 10;

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

        private string GetMeasuredInText()
        {
            switch (ProductType)
            {
                case EProductType.Capsules:
                    return Globalisation.Strings.Constants.Measures.Capsules;

                case EProductType.Powder:
                    return Globalisation.Strings.Constants.Measures.Milligrams;

                case EProductType.Liquid:
                    return Globalisation.Strings.Constants.Measures.Millilitres;

                default:
                    return string.Empty;
            }
        }

        private string GetMeasuredInForLargeQuantityText()
        {
            switch (ProductType)
            {
                case EProductType.Capsules:
                    return Globalisation.Strings.Constants.Measures.Capsules;

                case EProductType.Powder:
                    return Globalisation.Strings.Constants.Measures.Grams;

                case EProductType.Liquid:
                    return Globalisation.Strings.Constants.Measures.Litres;

                default:
                    return string.Empty;
            }
        }
    }
}
