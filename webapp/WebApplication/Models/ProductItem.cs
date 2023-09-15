using System.ComponentModel.DataAnnotations;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Attributes;

namespace K9.WebApplication.Models
{
    public class ProductItem
    {
        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
        public string ProductName { get; set; }

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.SubTitleLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary),
            ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public string ProductSubTitle { get; set; }

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
        public string CapsulesDosageLabelText => $"{MinDosage} - {MaxDosage}";

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CapsulesDosageLabel)]
        public string CapsulesLabellext => MaxDosage > 1 ? Globalisation.Dictionary.Capsules : Globalisation.Dictionary.Capsule;

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.BenefitsLabel)]
        public string BenefitsLabelText { get; set; }

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IngredientLabel)]
        public string IngredientsList { get; set; }

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantitiesLabel)]
        public string QuantitiesList { get; set; }

        [ProductLabel]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DailyValuesLabel)]
        public string DailyValues { get; set; }

    }
}