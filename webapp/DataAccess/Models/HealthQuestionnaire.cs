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
using K9.SharedLibrary.Authentication;

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

        public bool IsBloodAnalysisEnabled() => IsPersonalInformationComplete();

        public bool IsBloodAnalysisActive() => IsBloodAnalysisEnabled() &&
                                               !IsBloodAnalysisComplete();

        public bool IsAcetylationEnabled() => IsPersonalInformationComplete() &&
                                             IsBloodAnalysisComplete();

        public bool IsAcetylationActive() => IsAcetylationEnabled() &&
                                           !IsAcetylationStatusComplete();

        public bool IsBiometricsEnabled() => IsPersonalInformationComplete() &&
                                            IsBloodAnalysisComplete() &&
                                            IsAcetylationStatusComplete();

        public bool IsBiometricsActive() => IsBiometricsEnabled() &&
                                          !IsBiometricsComplete();

        public bool IsDermatoglyphicsEnabled() => IsPersonalInformationComplete() &&
                                                 IsBloodAnalysisComplete() &&
                                                 IsAcetylationStatusComplete() &&
                                                 IsBiometricsComplete();

        public bool IsDermatoglyphicsActive() => IsDermatoglyphicsEnabled() &&
                                               !IsDermatoglyphicsComplete();

        public bool IsDentitionEnabled() => IsPersonalInformationComplete() &&
                                           IsBloodAnalysisComplete() &&
                                           IsAcetylationStatusComplete() &&
                                           IsBiometricsComplete() &&
                                           IsDermatoglyphicsComplete();

        public bool IsDentitionActive() => IsDentitionEnabled() &&
                                         !IsDentitionComplete();

        public bool IsTasterStatusEnabled() => IsPersonalInformationComplete() &&
                                              IsBloodAnalysisComplete() &&
                                              IsAcetylationStatusComplete() &&
                                              IsBiometricsComplete() &&
                                              IsDermatoglyphicsComplete() &&
                                              IsDentitionComplete();

        public bool IsTasterStatusActive() => IsTasterStatusEnabled() &&
                                            !IsTasterStatusComplete();

        public bool IsFamilyHistoryEnabled() => IsPersonalInformationComplete() &&
                                               IsBloodAnalysisComplete() &&
                                               IsAcetylationStatusComplete() &&
                                               IsBiometricsComplete() &&
                                               IsDermatoglyphicsComplete() &&
                                               IsDentitionComplete() &&
                                               IsTasterStatusComplete();

        public bool IsFamilyHistoryActive() => IsFamilyHistoryEnabled() &&
                                             !IsFamilyHistoryComplete();


        public bool IsCbsAndMethylationEnabled() => IsPersonalInformationComplete() &&
                                                   IsBloodAnalysisComplete() &&
                                                   IsAcetylationStatusComplete() &&
                                                   IsBiometricsComplete() &&
                                                   IsDermatoglyphicsComplete() &&
                                                   IsDentitionComplete() &&
                                                   IsTasterStatusComplete() &&
                                                   IsFamilyHistoryComplete();

        public bool IsCbsAndMethylationActive() => IsCbsAndMethylationEnabled() &&
                                                   !IsCbsAndMethylationComplete();

        public bool IsDoshasEnabled() => IsPersonalInformationComplete() &&
                                        IsBloodAnalysisComplete() &&
                                        IsAcetylationStatusComplete() &&
                                        IsBiometricsComplete() &&
                                        IsDermatoglyphicsComplete() &&
                                        IsDentitionComplete() &&
                                        IsTasterStatusComplete() &&
                                        IsFamilyHistoryComplete() &&
                                        IsCbsAndMethylationComplete();

        public bool IsDoshasActive() => IsDoshasEnabled() &&
                                        !IsDoshasComplete();

        public bool IsGeneralHealthEnabled() => IsPersonalInformationComplete() &&
                                                IsBloodAnalysisComplete() &&
                                                IsAcetylationStatusComplete() &&
                                                IsBiometricsComplete() &&
                                                IsDermatoglyphicsComplete() &&
                                                IsDentitionComplete() &&
                                                IsTasterStatusComplete() &&
                                                IsCbsAndMethylationComplete() &&
                                                IsDoshasComplete() &&
                                                IsFamilyHistoryComplete();

        public bool IsGeneralHealthActive() => IsGeneralHealthEnabled() &&
                                               !IsGeneralHealthComplete();


        #region Personal Details

        public string GetPersonalDetailsActivePanelClass() =>
            IsPersonalDetailsActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetPersonalDetailsActiveTabClass() =>
            IsPersonalDetailsActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetPersonalDetailsCompleteHtml() =>
            IsPersonalInformationComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Blood Analysis

        public string GetBloodAnalysisActivePanelClass() =>
            IsBloodAnalysisActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetBloodAnalysisActiveTabClass() =>
            IsBloodAnalysisActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetBloodAnalysisEnabledClass() =>
            IsBloodAnalysisEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetBloodAnalysisToggle() =>
            IsBloodAnalysisEnabled() ? Strings.CssClasses.Pill : "";

        public string GetBloodAnalysisCompleteHtml() =>
            IsBloodAnalysisComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Acetylation

        public string GetAcetylationActivePanelClass() =>
            IsAcetylationActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetAcetylationActiveTabClass() =>
            IsAcetylationActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetAcetylationEnabledClass() =>
            IsAcetylationEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetAcetylationToggle() =>
            IsAcetylationEnabled() ? Strings.CssClasses.Pill : "";

        public string GetAcetylationCompleteHtml() =>
            IsAcetylationStatusComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Biometrics

        public string GetBiometricsActivePanelClass() =>
        IsBiometricsActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetBiometricsActiveTabClass() =>
            IsBiometricsActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetBiometricsEnabledClass() =>
            IsBiometricsEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetBiometricsToggle() =>
            IsBiometricsEnabled() ? Strings.CssClasses.Pill : "";

        public string GetBiometricsCompleteHtml() =>
            IsBiometricsComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Dermatoglyphics

        public string GetDermatoglyphicsActivePanelClass() =>
            IsDermatoglyphicsActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetDermatoglyphicsActiveTabClass() =>
            IsDermatoglyphicsActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetDermatoglyphicsEnabledClass() =>
            IsDermatoglyphicsEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetDermatoglyphicsToggle() =>
            IsDermatoglyphicsEnabled() ? Strings.CssClasses.Pill : "";

        public string GetDermatoglyphicsCompleteHtml() =>
            IsDermatoglyphicsComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Dentition

        public string GetDentitionActivePanelClass() =>
            IsDentitionActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetDentitionActiveTabClass() =>
            IsDentitionActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetDentitionEnabledClass() =>
            IsDentitionEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetDentitionToggle() =>
            IsDentitionEnabled() ? Strings.CssClasses.Pill : "";

        public string GetDentitionCompleteHtml() =>
            IsDentitionComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Taster Status

        public string GetTasterStatusActivePanelClass() =>
            IsTasterStatusActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetTasterStatusActiveTabClass() =>
            IsTasterStatusActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetTasterStatusEnabledClass() =>
            IsTasterStatusEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetTasterStatusToggle() =>
            IsTasterStatusEnabled() ? Strings.CssClasses.Pill : "";

        public string GetTasterStatusCompleteHtml() =>
            IsTasterStatusComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Family History

        public string GetFamilyHistoryActivePanelClass() =>
            IsFamilyHistoryActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetFamilyHistoryActiveTabClass() =>
            IsFamilyHistoryActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetFamilyHistoryEnabledClass() =>
            IsFamilyHistoryEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetFamilyHistoryToggle() =>
            IsFamilyHistoryEnabled() ? Strings.CssClasses.Pill : "";

        public string GetFamilyHistoryCompleteHtml() =>
            IsFamilyHistoryComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region General Health

        public string GetGeneralHealthActivePanelClass() =>
            IsGeneralHealthActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetGeneralHealthActiveTabClass() =>
            IsGeneralHealthActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetGeneralHealthEnabledClass() =>
            IsGeneralHealthEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetGeneralHealthToggle() =>
            IsGeneralHealthEnabled() ? Strings.CssClasses.Pill : "";

        public string GetGeneralHealthCompleteHtml() =>
            IsGeneralHealthComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Cbs And Methylation

        public string GetCbsAndMethylationActivePanelClass() =>
            IsCbsAndMethylationActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetCbsAndMethylationActiveTabClass() =>
            IsCbsAndMethylationActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetCbsAndMethylationEnabledClass() =>
            IsCbsAndMethylationEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetCbsAndMethylationToggle() =>
            IsCbsAndMethylationEnabled() ? Strings.CssClasses.Pill : "";

        public string GetCbsAndMethylationCompleteHtml() =>
            IsCbsAndMethylationComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #region Doshas

        public string GetDoshasActivePanelClass() =>
            IsDoshasActive() ? Strings.CssClasses.ActivePanelClass : "";

        public string GetDoshasActiveTabClass() =>
            IsDoshasActive() ? Strings.CssClasses.ActiveTabClass : "";

        public string GetDoshasEnabledClass() =>
            IsDoshasEnabled() ? "" : Strings.CssClasses.DisabledClass;

        public string GetDoshasToggle() =>
            IsDoshasEnabled() ? Strings.CssClasses.Pill : "";

        public string GetDoshasCompleteHtml() =>
            IsDoshasComplete() ? "<i class=\"fa fa-check-circle green\"></i>" : "";

        #endregion

        #endregion

        #region Personal Details

        [UIHint("Client")]
        [ForeignKey("Client")]
        [Required]
        [Index(IsUnique = true)]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [UIHint("Gender")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GenderLabel)]
        public EGender? Gender { get; set; }

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

        public bool IsPersonalInformationComplete() => DateOfBirth.HasValue && Gender.HasValue;

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
        public EYesNo? EnjoysCooking { get; set; }

        [UIHint("Frequency")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CookingFrequencyLabel)]
        public EFrequency? CookingFrequency { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BloatingLabel)]
        public EYesNo? Bloating { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GasLabel)]
        public EYesNo? Gas { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LooseStoolLabel)]
        public EYesNo? LooseStool { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ConstipationLabel)]
        public EYesNo? Constipation { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AbdominalPainOrCrampingLabel)]
        public EYesNo? AbdominalPainOrCramping { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinIssuesLabel)]
        public EYesNo? SkinIssues { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinIssuesDetailsLabel)]
        public string SkinIssuesDetails { get; set; }

        public double GetDigestionIssuesScore()
        {
            var totalScore = 6;
            var score = 0;

            if (Bloating == EYesNo.Yes)
            {
                score++;
            }

            if (Gas == EYesNo.Yes)
            {
                score++;
            }

            if (LooseStool == EYesNo.Yes)
            {
                score++;
            }

            if (Constipation == EYesNo.Yes)
            {
                score++;
            }

            if (AbdominalPainOrCramping == EYesNo.Yes)
            {
                score++;
            }

            if (SkinIssues == EYesNo.Yes)
            {
                score++;
            }

            return score / totalScore;
        }

        [UIHint("Severity")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.InfectionSeverityLabel)]
        public ESeverity? InfectionSeverity { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinIssuesLabel)]
        public EYesNo? AllergiesAndSensitivities { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.UTILabel)]
        public EYesNo? UTI { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AllergiesAndSensitivitiesLabel)]
        public string AllergiesAndSensitivitiesDetails { get; set; }

        public double GetImmunityIssuesScore()
        {
            var totalScore = 5;
            var score = 0;

            if (InfectionSeverity == ESeverity.MoreSevere)
            {
                score++;
            }

            if (CandidaAndFungus == EYesNo.Yes)
            {
                score++;
            }

            if (AllergiesAndSensitivities == EYesNo.Yes)
            {
                score++;
            }

            if (UTI == EYesNo.Yes)
            {
                score++;
            }

            if (GetDigestionIssuesScore() > 0)
            {
                score++;
            }

            return score / totalScore;
        }

        #region Yin Disease

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ColdExtremitiesLabel)]
        public EYesNo? ColdExtremities { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ColdIntolerantLabel)]
        public EYesNo? ColdIntolerant { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CandidaFungusLabel)]
        public EYesNo? CandidaAndFungus { get; set; }

        public double GetYinDiseaseScore()
        {
            var totalScore = 4;
            var score = 0;

            if (ColdExtremities == EYesNo.Yes)
            {
                score++;
            }

            if (ColdIntolerant == EYesNo.Yes)
            {
                score++;
            }

            if (CandidaAndFungus == EYesNo.Yes)
            {
                score++;
            }

            if (UTI == EYesNo.Yes)
            {
                score++;
            }

            return score / totalScore;
        }

        #endregion

        #region Inflammation

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.JointInflammationLabel)]
        public EYesNo? JointInflammation { get; set; }

        public double GetInflammationScore()
        {
            var totalScore = 3;
            var score = 0;

            if (JointInflammation == EYesNo.Yes)
            {
                score++;
            }

            if (AllergiesAndSensitivities == EYesNo.Yes)
            {
                score++;
            }

            if (SkinIssues == EYesNo.Yes)
            {
                score++;
            }

            return score / totalScore;
        }

        #endregion

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ExerciseLabel)]
        public string Exercise { get; set; }

        public bool IsGeneralHealthComplete()
        {
            return !string.IsNullOrEmpty(CurrentHealthIssues) &&
                   !string.IsNullOrEmpty(HealthGoals) &&
                   EnjoysCooking.HasValue &&
                   CookingFrequency.HasValue &&
                   Bloating.HasValue &&
                   Gas.HasValue &&
                   LooseStool.HasValue &&
                   Constipation.HasValue &
                   AbdominalPainOrCramping.HasValue &&
                   SkinIssues == EYesNo.Yes
                ? !string.IsNullOrEmpty(SkinIssuesDetails)
                : SkinIssues.HasValue &&
                  InfectionSeverity.HasValue;
            ;
        }

        #endregion
    }
}
