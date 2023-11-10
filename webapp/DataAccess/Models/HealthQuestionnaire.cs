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
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.HealthQuestionnaires, PluralName = Strings.Names.HealthQuestionnaires, Name = Strings.Names.HealthQuestionnaire)]
    public partial class HealthQuestionnaire : ObjectBase
    {
        [Required]
        public Guid ExternalId { get; set; }

        public bool IsComplete()
        {
            return IsGeneralHealthComplete() &&
                   IsPersonalInformationComplete() &&
                   IsFamilyHistoryComplete() &&
                   IsDentitionComplete() &&
                   IsAcetylationStatusComplete() &&
                   IsBiometricsComplete() &&
                   IsBloodAnalysisComplete() &&
                   IsFamilyHistoryComplete() &&
                   IsDermatoglyphicsComplete() &&
                   IsTasterStatusComplete();
        }

        public bool IsStarted()
        {
            return IsGeneralHealthComplete() ||
                   IsPersonalInformationComplete() ||
                   IsFamilyHistoryComplete() ||
                   IsAcetylationStatusComplete() ||
                   IsDentitionComplete() ||
                   IsBiometricsComplete() ||
                   IsBloodAnalysisComplete() ||
                   IsFamilyHistoryComplete() ||
                   IsDermatoglyphicsComplete() ||
                   IsTasterStatusComplete();
        }

        #region Css

        public bool IsPersonalDetailsActive() => !IsPersonalInformationComplete();

        public bool IsBloodAnalysisActive() => IsPersonalInformationComplete() &&
                                               !IsBloodAnalysisComplete();

        public bool IsAcetylationActive() => IsPersonalInformationComplete() &&
                                           IsBloodAnalysisComplete() &&
                                           !IsAcetylationStatusComplete();

        public bool IsBiometricsActive() => IsPersonalInformationComplete() &&
                                          IsBloodAnalysisComplete() &&
                                          IsAcetylationStatusComplete() &&
                                          !IsBiometricsComplete();

        public bool IsDermatoglyphicsActive() => IsPersonalInformationComplete() &&
                                               IsBloodAnalysisComplete() &&
                                               IsAcetylationStatusComplete() &&
                                               IsBiometricsComplete() &&
                                               !IsDermatoglyphicsComplete();

        public bool IsDentitionActive() => IsPersonalInformationComplete() &&
                                         IsBloodAnalysisComplete() &&
                                         IsAcetylationStatusComplete() &&
                                         IsBiometricsComplete() &&
                                         IsDermatoglyphicsComplete() &&
                                         !IsDentitionComplete();

        public bool IsTasterStatusActive() => IsPersonalInformationComplete() &&
                                            IsBloodAnalysisComplete() &&
                                            IsAcetylationStatusComplete() &&
                                            IsBiometricsComplete() &&
                                            IsDermatoglyphicsComplete() &&
                                            IsDentitionComplete() &&
                                            !IsTasterStatusComplete();

        public bool IsFamilyHistoryActive() => IsPersonalInformationComplete() &&
                                             IsBloodAnalysisComplete() &&
                                             IsAcetylationStatusComplete() &&
                                             IsBiometricsComplete() &&
                                             IsDermatoglyphicsComplete() &&
                                             IsDentitionComplete() &&
                                             IsTasterStatusComplete() &&
                                             !IsFamilyHistoryComplete();

        public bool IsGeneralHealthActive() => IsPersonalInformationComplete() &&
                                             IsBloodAnalysisComplete() &&
                                             IsAcetylationStatusComplete() &&
                                             IsBiometricsComplete() &&
                                             IsDermatoglyphicsComplete() &&
                                             IsDentitionComplete() &&
                                             IsTasterStatusComplete() &&
                                             IsFamilyHistoryComplete() &&
                                             !IsGeneralHealthComplete();

        public bool IsCbsAndMethylationActive() => IsPersonalInformationComplete() &&
                                             IsBloodAnalysisComplete() &&
                                             IsAcetylationStatusComplete() &&
                                             IsBiometricsComplete() &&
                                             IsDermatoglyphicsComplete() &&
                                             IsDentitionComplete() &&
                                             IsTasterStatusComplete() &&
                                             IsFamilyHistoryComplete() &&
                                             IsGeneralHealthComplete() &&
                                             !IsCbsAndMethylationComplete();

        public bool IsDoshasActive() => IsPersonalInformationComplete() &&
                                        IsBloodAnalysisComplete() &&
                                        IsAcetylationStatusComplete() &&
                                        IsBiometricsComplete() &&
                                        IsDermatoglyphicsComplete() &&
                                        IsDentitionComplete() &&
                                        IsTasterStatusComplete() &&
                                        IsFamilyHistoryComplete() &&
                                        IsGeneralHealthComplete() &&
                                        IsCbsAndMethylationComplete() &&
                                        !IsDoshasComplete();

        public string GetPersonalDetailsActivePanelClass() =>
            IsPersonalDetailsActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetPersonalDetailsActiveTabClass() =>
            IsPersonalDetailsActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetBloodAnalysisActivePanelClass() =>
            IsBloodAnalysisActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetBloodAnalysisActiveTabClass() =>
            IsBloodAnalysisActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetAcetylationActivePanelClass() =>
            IsAcetylationActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetAcetylationActiveTabClass() =>
            IsAcetylationActive() ? Strings.CssClasses.ActiveTabClass : "";
       
        public string GetBiometricsActivePanelClass() =>
        IsBloodAnalysisActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetBiometricsActiveTabClass() =>
            IsBloodAnalysisActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetDermatoglyphicsActivePanelClass() =>
            IsDermatoglyphicsActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetDermatoglyphicsActiveTabClass() =>
            IsDermatoglyphicsActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetDentitionActivePanelClass() =>
            IsDentitionActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetDentitionActiveTabClass() =>
            IsDentitionActive() ? Strings.CssClasses.ActiveTabClass: "";

        public string GetTasterStatusActivePanelClass() =>
            IsTasterStatusActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetTasterStatusActiveTabClass() =>
            IsTasterStatusActive() ? Strings.CssClasses.ActiveTabClass: "";

        public string GetFamilyHistoryActivePanelClass() =>
            IsFamilyHistoryActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetFamilyHistoryActiveTabClass() =>
            IsFamilyHistoryActive() ? Strings.CssClasses.ActiveTabClass: "";

        public string GetGeneralHealthActivePanelClass() =>
            IsGeneralHealthActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetGeneralHealthActiveTabClass() =>
            IsGeneralHealthActive() ? Strings.CssClasses.ActiveTabClass: "";

        public string GetCbsAndMethylationActivePanelClass() =>
            IsCbsAndMethylationActive() ? Strings.CssClasses.ActivePanelClass: "";

        public string GetCbsAndMethylationActiveTabClass() =>
            IsCbsAndMethylationActive() ? Strings.CssClasses.ActiveTabClass: "";

        public string GetDoshasActivePanelClass() =>
            IsDoshasActive() ? Strings.CssClasses.ActivePanelClass: "";

        public string GetDoshasActiveTabClass() =>
            IsDoshasActive() ? Strings.CssClasses.ActiveTabClass: "";

        #endregion

        #region Personal Details

        [UIHint("Client")]
        [ForeignKey("Client")]
        [Required]
        [Index(IsUnique = true)]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [UIHint("Gender")]
        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GenderLabel)]
        public EGender Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DateOfBirthLabel)]
        public DateTime? DateOfBirth { get; set; }

        [Display(ResourceType = typeof(Base.Globalisation.Dictionary), Name = Base.Globalisation.Strings.Labels.LanguageLabel)]
        public string GenderName => Gender.GetLocalisedLanguageName();

        public string GenderPossessivePronoun => Gender == EGender.Male ? "his" : "her";

        public string GenderPronoun => Gender == EGender.Male ? "he" : "she";

        public int? YearsOld => GetYearsOld();

        public bool IsAdult() => YearsOld >= 18;

        public bool IsSixteenOrOver() => YearsOld >= 16;

        public NineStarKiModel GetNineStarKiModel()
        {
            var client = new System.Net.WebClient();
            if (DateOfBirth.HasValue)
            {
                var requestString =
                    $"https://9starki.org/api/calculate?dateOfBirth={DateOfBirth.Value.ToString(FormatConstants.ApiDateTimeFormat)}&gender={Gender}";
                var nineStarKiJson = client.DownloadString(requestString);
                var serializer = new JavaScriptSerializer();

                return serializer.Deserialize<NineStarKiModel>(nineStarKiJson);
            }

            return null;
        }

        public ZodiacModel GetZodiacModel()
        {
            return DateOfBirth.HasValue 
                ? new ZodiacModel(DateOfBirth.Value) 
                : null;
        }

        private int? GetYearsOld()
        {
            return DateOfBirth.HasValue ? (DateTime.Now.Year - DateOfBirth.Value.Year) - (DateTime.Now.DayOfYear < DateOfBirth.Value.DayOfYear ? 1 : 0) : (int?)null;
        }

        public bool IsPersonalInformationComplete() => DateOfBirth.HasValue;

        #endregion

        #region General Health 

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthIssuesLabel)]
        public string CurrentHealthIssues { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HealthGoalsLabel)]
        public string HealthGoals { get; set; }

        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthLevelLabel)]
        [Min(1)]
        [Max(10)]
        public int? CurrentHealthLevel { get; set; }

        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NutritionExpertiseLevelLabel)]
        [Min(1)]
        [Max(10)]
        public int? NutritionExpertiseLevel { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EnjoysCookingLabel)]
        public EYesNo EnjoysCooking { get; set; }

        [UIHint("Frequency")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EnjoysCookingLabel)]
        public EFrequency CookingFrequency { get; set; }

        public bool IsGeneralHealthComplete()
        {
            return !string.IsNullOrEmpty(CurrentHealthIssues) &&
                   !string.IsNullOrEmpty(HealthGoals) &&
                   EnjoysCooking != EYesNo.Unanswered &&
                   CookingFrequency != EFrequency.Unanswered;
        }

        #endregion
    }
}
