using DataAnnotationsExtensions;
using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Enums;
using K9.Base.DataAccessLayer.Extensions;
using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Constants;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.HealthQuestionnaires, PluralName = Strings.Names.HealthQuestionnaires, Name = Strings.Names.HealthQuestionnaire)]
    public partial class HealthQuestionnaire : ObjectBase
    {

        #region Personal Details

        [UIHint("Gender")]
        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GenderLabel)]
        public EGender Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DateOfBirthLabel)]
        public DateTime DateOfBirth { get; set; }

        [Display(ResourceType = typeof(Base.Globalisation.Dictionary), Name = Base.Globalisation.Strings.Labels.LanguageLabel)]
        public string GenderName => Gender.GetLocalisedLanguageName();

        public string GenderPossessivePronoun => Gender == EGender.Male ? "his" : "her";

        public string GenderPronoun => Gender == EGender.Male ? "he" : "she";

        public int YearsOld => GetYearsOld();

        public bool IsAdult() => YearsOld >= 18;

        public bool IsSixteenOrOver() => YearsOld >= 16;

        public NineStarKiModel GetNineStarKiModel()
        {
            var client = new System.Net.WebClient();
            var nineStarKiJson = client.DownloadString($"https://9starki.org/api/calculate?dateOfBirth={DateOfBirth.ToString(FormatConstants.ApiDateTimeFormat)}&gender={Gender}");
            var serializer = new JavaScriptSerializer();

            return serializer.Deserialize<NineStarKiModel>(nineStarKiJson);
        }

        public ZodiacModel GetZodiacModel()
        {
            return new ZodiacModel(DateOfBirth);
        }

        private int GetYearsOld()
        {
            return (DateTime.Now.Year - DateOfBirth.Year) - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        }

        #endregion

        #region General Health 

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthIssuesLabel)]
        [Required]
        public string CurrentHealthIssues { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HealthGoalsLabel)]
        [Required]
        public string HealthGoals { get; set; }

        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthLevelLabel)]
        [Required]
        [Min(1)]
        [Max(10)]
        public int CurrentHealthLevel { get; set; }

        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NutritionExpertiseLevelLabel)]
        [Required]
        [Min(1)]
        [Max(10)]
        public int NutritionExpertiseLevel { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EnjoysCookingLabel)]
        [Required]
        public EYesNo EnjoysCooking { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EnjoysCookingLabel)]
        [Required]
        [Min(1)]
        [Max(10)]
        public EFrequency CookingFrequency { get; set; }

        #endregion
    }
}
