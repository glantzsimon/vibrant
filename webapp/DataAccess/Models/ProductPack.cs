using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Helpers;
using K9.SharedLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProductPacks, PluralName = Globalisation.Strings.Names.ProductPacks, Name = Globalisation.Strings.Names.ProductPack)]
    public class ProductPack : ObjectBase
    {
        public Guid ExternalId { get; set; }

        [NotMapped]
        [UIHint("ProductPack")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        public int ProductPackId => Id;

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountCompletedLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int AmountCompleted { get; set; }

        public virtual IEnumerable<ProductPackProduct> ProductPackProducts { get; set; }

        [NotMapped]
        public List<ProductPackProduct> Products { get; set; }

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

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DosageLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Dosage { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string FormattedPrice => double.Parse(Price.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedRetailPriceLabel)]
        [DataType(DataType.Currency)]
        public double SuggestedRetailPrice => Methods.RoundToInteger(TotalProductsPrice > 0 ? (TotalProductsPrice * 0.93) : 0, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalSavingsLabel)]
        [DataType(DataType.Currency)]
        public double ProductsDiscount => TotalProductsPrice - Price;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount1Label)]
        [DataType(DataType.Currency)] public double PriceDiscount1 => Methods.RoundToInteger(Price * 0.80, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount1Label)]
        public string FormattedPriceDiscount1 => double.Parse(PriceDiscount1.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount2Label)]
        [DataType(DataType.Currency)] public double PriceDiscount2 => Methods.RoundToInteger(Price * 0.66, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount2Label)]
        public string FormattedPriceDiscount2 => double.Parse(PriceDiscount2.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SeoFriendlyIdLabel)]
        public string SeoFriendlyId { get; set; }

        [FileSourceInfo("upload/productpacks", Filter = EFilesSourceFilter.Images)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadImages)]
        public FileSource ImageFileSource { get; set; }

        [FileSourceInfo("upload/productpacks", Filter = EFilesSourceFilter.Videos)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadVideo)]
        public FileSource VideoFileSource { get; set; }

        private double TotalProductsPrice => Products?.Sum(e => e.Product.Price * e.Amount) ?? 0;
    }
}
