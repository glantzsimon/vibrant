using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Helpers;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Products, PluralName = Globalisation.Strings.Names.Products, Name = Globalisation.Strings.Names.Product)]
    public class Product : ObjectBase
    {
        public const int ProductLabelBenefitsCount = 9;

        public Guid ExternalId { get; set; }

        [NotMapped]
        [UIHint("Product")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        public int ProductId => Id;

        [UIHint("ProductType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.ProductTypeLabel)]
        public EProductType ProductType { get; set; }

        public string MeasuredIn => GetMeasuredInText();

        public string ServingMeasuredIn => GetServingMeasuredInText();

        public string MeasuredInForLargeQuantity => GetMeasuredInForLargeQuantityText();

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        [UIHint("ProductIngredients")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IngredientsLabel)]
        public int ProductIngredientsId => Id;

        public virtual IEnumerable<ProductIngredient> ProductIngredients { get; set; }

        public List<ProductIngredient> Ingredients { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsMainLabel)]
        public bool IsMain { get; set; }

        [UIHint("Quantity")]
        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        public string FormattedAmount => $"{Amount} {MeasuredIn}";

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        public string FormattedSmallPackAmount => $"{Amount / 2} {MeasuredIn}";

        [UIHint("Quantity")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerServingLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public float AmountPerServing { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerServingLabel)]
        public string FormattedAmountPerServing => $"{AmountPerServing} {ServingMeasuredIn}";

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

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceSmallPackLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double PriceSmallPack => Methods.RoundToInteger(Price * 0.60, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string FormattedPriceSmallPack => double.Parse(PriceSmallPack.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostOfMaterialsLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [DataType(DataType.Currency)]
        public double CostOfMaterials { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CostOfIngredientsLabel)]
        [DataType(DataType.Currency)]
        public double CostOfIngredients => ((ProductIngredients?.Sum(e => e.Cost) ?? 0) * Amount);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalCostLabel)]
        [DataType(DataType.Currency)]
        public double TotalCost => CostOfIngredients + CostOfMaterials;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedRetailPriceLabel)]
        [DataType(DataType.Currency)]
        public double SuggestedRetailPrice => Methods.RoundToInteger(TotalCost * 2 + 900, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SuggestedRetailPriceLabel)]
        public string FormattedSuggestedRetailPrice => double.Parse(SuggestedRetailPrice.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginLabel)]
        [DataType(DataType.Currency)]
        public double ProfitMargin => Price - TotalCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginSmallPackLabel)]
        [DataType(DataType.Currency)]
        public double ProfitMarginSmallPack => PriceSmallPack - (TotalCost / 2);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginDiscount1Label)]
        [DataType(DataType.Currency)]
        public double ProfitMarginDiscount1 => PriceDiscount1 - TotalCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginSmallPackDiscount1Label)]
        [DataType(DataType.Currency)]
        public double ProfitMarginSmallPackDiscount1 => PriceSmallPackDiscount1 - (TotalCost / 2);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginDiscount2Label)]
        [DataType(DataType.Currency)]
        public double ProfitMarginDiscount2 => PriceDiscount2 - TotalCost;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginSmallPackDiscount2Label)]
        [DataType(DataType.Currency)]
        public double ProfitMarginSmallPackDiscount2 => PriceSmallPackDiscount2 - (TotalCost / 2);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProfitMarginLabel)]
        public string FormattedProfitMargin => double.Parse(ProfitMargin.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount1Label)]
        [DataType(DataType.Currency)] public double PriceDiscount1 => Methods.RoundToInteger(Price * 0.80, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceSmallPackDiscount1Label)]
        [DataType(DataType.Currency)] public double PriceSmallPackDiscount1 => Methods.RoundToInteger(PriceSmallPack * 0.80, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount1Label)]
        public string FormattedPriceDiscount1 => double.Parse(PriceDiscount1.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount2Label)]
        [DataType(DataType.Currency)] public double PriceDiscount2 => Methods.RoundToInteger(Price * 0.66, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceSmallPackDiscount2Label)]
        [DataType(DataType.Currency)] public double PriceSmallPackDiscount2 => Methods.RoundToInteger(PriceSmallPack * 0.66, 100);

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceDiscount2Label)]
        public string FormattedPriceDiscount2 => double.Parse(PriceDiscount2.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InStockLabel)]
        public bool IsHydroscopic() => Ingredients?.Any(e => e.Ingredient.IsHydroscopic) ?? false;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantityInStockLabel)]
        public int QuantityInStock { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InStockLabel)]
        public bool IsInStock => QuantityInStock > 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.StockLowWarningLevelLabel)]
        public int StockLowWarningLevel { get; set; } = 10;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.StockLowWarningLabel)]
        public bool IsStockLowWarning => QuantityInStock < StockLowWarningLevel;

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

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BatchSizeLabel)]
        public int BatchSize { get; set; } = 1;

        #region Product Label

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        public string ProductName => Name.ToUpper();

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTitleLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary),
            ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public string SubTitleLabelText { get; set; }

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTitleLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary),
            ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public string ProductSubTitle => SubTitleLabelText?.ToUpper();

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.MaxDosageLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int MaxDosage { get; set; } = 1;

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.MinDosageLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary),
            ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int MinDosage { get; set; } = 1;

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CapsulesDosageLabel)]
        public string CapsulesDosageLabelText => MaxDosage > 1 ? $"{MinDosage} - {MaxDosage}" : MaxDosage.ToString();

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CapsulesLabel)]
        public string CapsulesLabellext => MaxDosage > 1 ? Globalisation.Dictionary.Capsules : Globalisation.Dictionary.Capsule;

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CapsulesDailyText)]
        public string CapsulesDailyLabellext => $"{CapsulesLabellext} Daily";

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.FullDosageText)]
        public string FullDosageLabellext => $"{CapsulesDosageLabelText} {CapsulesDailyLabellext}";

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
        public string BenefitsLabelText => Benefits.HtmlToText().SelectLines(ProductLabelBenefitsCount);

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IngredientLabel)]
        public string IngredientsList => GetList(Ingredients?.Select(e => e.Ingredient.Name).ToArray());

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantitiesLabel)]
        public string QuantitiesList => GetList(Ingredients?.Select(e => e.FormattedLabelAmount).ToArray());

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DailyValuesLabel)]
        public string DailyValues => GetList(Ingredients?.Select(e => e.FormattedPercentageOfDailyAllowance).ToArray());

        public string GetList(string[] items)
        {
            return items == null ? string.Empty : string.Join(Environment.NewLine, items);
        }

        #endregion

        public static List<PropertyInfo> GetProductLabelProperties() => typeof(Product).GetProperties()
            .Where(e => e.GetCustomAttributes<ProductLabelAttribute>().Any()).ToList();

        public float GetTotalIngredientsAmount()
        {
            return Ingredients?.Sum(e => e.AmountPerConcentration) ?? 0;
        }

        public string GetFormattedTotalIngredientsAmount()
        {
            return $"{GetTotalIngredientsAmount()} {ServingMeasuredIn}";
        }

        public bool IngredientAmountsAreCorrect()
        {
            return GetTotalIngredientsAmount() == (AmountPerServing);
        }

        public string GetIngredientAmountIncorrectError()
        {
            return $"Total ingredients per serving must equal {AmountPerServing} {ServingMeasuredIn}. The current value is {GetFormattedTotalIngredientsAmount()}";
        }

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

        private string GetServingMeasuredInText()
        {
            switch (ProductType)
            {
                case EProductType.Capsules:
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
