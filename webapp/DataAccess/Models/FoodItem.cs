using K9.Base.DataAccessLayer.Attributes;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.FoodItems, PluralName = Globalisation.Strings.Names.FoodItems, Name = Globalisation.Strings.Names.FoodItem)]
    public class FoodItem : GenoTypeBase
    {
        public Guid ExternalId { get; set; }

        [UIHint("FoodCategory")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ActivityLabel)]
        public EFoodCategory Category { get; set; }

        public bool IsRedMeat => Category == EFoodCategory.RedMeat;
        public bool IsPoultry => Category == EFoodCategory.Poultry;
        public bool IsFishAndSeafood => Category == EFoodCategory.FishAndSeafood;
        public bool IsEggsAndRoes => Category == EFoodCategory.EggsAndRoes;
        public bool IsDairy => Category == EFoodCategory.Dairy;
        public bool IsVegetables => Category == EFoodCategory.Vegetables;
        public bool IsVegetableProtein => Category == EFoodCategory.VegetableProtein;
        public bool IsFungi => Category == EFoodCategory.Fungi;
        public bool IsFruit => Category == EFoodCategory.Fruits;
        public bool IsGrains => Category == EFoodCategory.Carbohydrates;

        [NotMapped]
        [UIHint("FoodItem")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ActivityLabel)]
        public int FoodItemId => Id;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SummaryLabel)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Benefits { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.RecommendationsLabel)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Recommendations { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsWhiteLabel)]
        public bool IsWhite { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsBeigeLabel)]
        public bool IsBeige { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsBlueLabel)]
        public bool IsBlue { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsGreenLabel)]
        public bool IsGreen { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsYellowLabel)]
        public bool IsYellow { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsOrangeLabel)]
        public bool IsOrange { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsRedLabel)]
        public bool IsRed { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsPurpleLabel)]
        public bool IsPurple { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsBrownLabel)]
        public bool IsBrown { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsBlackLabel)]
        public bool IsBlack { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CanEatRawLabel)]
        public bool CanBeEatenRaw { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsCitrusFruitLabel)]
        public bool IsCitrusFruit { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsLowCarbLabel)]
        public bool IsLowCarb { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsNightshadeLabel)]
        public bool IsNightshade { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PlantPartLabel)]
        public EPlantPart PlantPart { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsBitterLabel)]
        public bool IsBitter { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsSweetLabel)]
        public bool IsSweet { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsPungentLabel)]
        public bool IsPungent { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsSaltyLabel)]
        public bool IsSalty { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsSourLabel)]
        public bool IsSour { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsAstringentLabel)]
        public bool IsAstringent { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsSpringLabel)]
        public bool IsSpring { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsSummerLabel)]
        public bool IsSummer { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsLateSummerLabel)]
        public bool IsLateSummer { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsAutumnLabel)]
        public bool IsAutumn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsWinterLabel)]
        public bool IsWinter { get; set; }
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsWaterElementLabel)]
        public bool IsWaterElement { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsTreeElementLabel)]
        public bool IsTreeElement { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsFireElementLabel)]
        public bool IsFireElement { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsEarthElementLabel)]
        public bool IsEarthElement { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsMetalElementLabel)]
        public bool IsMetalElement { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighOxalateLabel)]
        public bool IsHighOxalate { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighLectinLabel)]
        public bool IsHighLectin { get; set; }

        public string HighLectinDescription{ get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighPhytateLabel)]
        public bool IsHighPhytate { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighHistamineLabel)]
        public bool IsHighHistamine { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighMycotoxinLabel)]
        public bool IsHighMycotoxin { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IsHighOmega6Label)]
        public bool IsHighOmega6 { get; set; }

        public string IsHighOmega6Description{ get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BulletProofDietLabel)]
        public bool IsBulletProof { get; set; }

        [FileSourceInfo("upload/fooditems", Filter = EFilesSourceFilter.Images)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadImages)]
        public FileSource ImageFileSource { get; set; }
    }
}
