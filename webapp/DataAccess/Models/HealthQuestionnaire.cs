using DataAnnotationsExtensions;
using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Enums;
using K9.Base.DataAccessLayer.Extensions;
using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Constants;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.HealthQuestionnaires, PluralName = Strings.Names.HealthQuestionnaires, Name = Strings.Names.HealthQuestionnaire)]
    public partial class HealthQuestionnaire : ObjectBase
    {
        public HealthQuestionnaire()
        {
            CurrentSeason = GetCurrentSeasonInNorthernHemisphere();
        }

        [Required]
        public Guid ExternalId { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DietaryPreference)]
        public ESeason CurrentSeason { get; set; }

        public ESeason GetCurrentSeasonInNorthernHemisphere()
        {
            var today = DateTime.Today;
            if (today.Month >= 1 && today.Month <= 2)
            {
                return ESeason.Winter;
            }
            else if (today.Month == 3)
            {
                if (today.Day < 21)
                {
                    return ESeason.Winter;
                }
                else
                {
                    return ESeason.Spring;
                }
            }
            else if (today.Month >= 4 && today.Month <= 5)
            {
                return ESeason.Spring;
            }
            else if (today.Month == 6)
            {
                if (today.Day < 21)
                {
                    return ESeason.Spring;
                }
                else
                {
                    return ESeason.Summer;
                }
            }
            else if (today.Month >= 7 && today.Month <= 8)
            {
                return ESeason.Summer;
            }
            else if (today.Month == 9)
            {
                if (today.Day < 21)
                {
                    return ESeason.LateSummer;
                }
                else
                {
                    return ESeason.Autumn;
                }
            }
            else if (today.Month >= 10 && today.Month <= 11)
            {
                return ESeason.Autumn;
            }
            else if (today.Month == 12)
            {
                return ESeason.Winter;
            }

            return ESeason.Spring;
        }

        public bool IsComplete()
        {
            return IsGeneralHealthComplete() &&
                   IsPersonalInformationComplete() &&
                   IsFamilyHistoryComplete() &&
                   IsDentitionComplete() &&
                   IsAcetylationStatusComplete() &&
                   IsBiometricsComplete() &&
                   IsBloodAnalysisComplete() &&
                   IsDermatoglyphicsComplete() &&
                   IsCbsAndMethylationComplete() &&
                   IsDoshasComplete() &&
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

        #region Scores

        public int GetScore(Func<ScoreAttribute, bool> condition, Func<bool> condition2 = null, int condition2ScoreFactor = 1)
        {
            var answers = GetPropertiesWithScoreAttribute()
                .Select(e => new
                {
                    PropertyInfo = e,
                    ScoreAttribute = e.GetAttribute<ScoreAttribute>()
                })
                .Where(e => condition(e.ScoreAttribute))
                .Select(e => new
                {
                    value = (EYesNo?)this.GetProperty(e.PropertyInfo),
                    scoreFactor = e.ScoreAttribute.ScoreFactor
                }).ToList();

            var yesses = answers.Where(e => e.value.HasValue && e.value.Value == EYesNo.Yes).ToList();
            var totalAnswers = answers.Sum(e => e.scoreFactor);
            var totalYesses = yesses.Sum(e => e.scoreFactor);

            if (condition2 != null)
            {
                totalAnswers += condition2ScoreFactor;

                if (condition2())
                {
                    totalYesses += condition2ScoreFactor;
                }
            }

            var score = yesses.Count > 0 ? (((double)totalYesses / (double)totalAnswers)) : 0;

            return (int)Math.Ceiling((double)score * 100);
        }
        
        public int GetDentalHealthScore()
        {
            return GetScore(e => e.DentalHealth,
                () => RootCanals == EYesNo.Yes || (!string.IsNullOrEmpty(DentalIssues)), 3);
        }

        public int GetNeurologicalScore()
        {
            return GetScore(e => e.NeurologicalHealth);
        }

        public int GetCardiovascularScore()
        {
            return GetScore(e => e.CardioVascularHealth);
        }

        public int GetInflammationScore()
        {
            return GetScore(e => e.IsInflammation, () => (GetCbsScore() > 50 || GetNeurologicalScore() > 50 || GetDigestionIssuesScore() > 50), 3);
        }

        #endregion

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

        [QuestionCategory(Category = EQuestionCategory.PersonalDetails)]
        [UIHint("Gender")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GenderLabel)]
        public EGender? Gender { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.PersonalDetails)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLGBTQPlusLabel)]
        public EYesNo? IsIsLGBTQPlus { get; set; }

        [QuestionCategory(Category = EQuestionCategory.PersonalDetails)]
        [UIHint("DietaryPreference")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DietaryPreference)]
        public EDietaryPreference? DietaryPreference { get; set; }

        [QuestionCategory(Category = EQuestionCategory.PersonalDetails)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DateOfBirthLabel)]
        public DateTime? DateOfBirth { get; set; }

        [Display(ResourceType = typeof(Base.Globalisation.Dictionary), Name = Base.Globalisation.Strings.Labels.LanguageLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string GenderName => Gender?.GetLocalisedLanguageName();

        public string GenderPossessivePronoun => Gender == EGender.Male ? Dictionary.His : Gender == EGender.Female ? Dictionary.Hers : Dictionary.Theirs;

        public string GenderPronoun => Gender == EGender.Male ? Dictionary.He : Gender == EGender.Female ? Dictionary.She : Dictionary.They;

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

        public bool IsPersonalInformationComplete() => IsCategoryComplete(e => e.Category == EQuestionCategory.PersonalDetails);

        #endregion

        #region Cardiovascular Health

        [Score(CardioVascularHealth = true, PittaDosha = true, IsYang = true, IsInflammation = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HighBloodPressureLabel)]
        public EYesNo? HighBloodPressure { get; set; }

        [Score(CardioVascularHealth = true, ScoreFactor = 2)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ChestPainLabel)]
        public EYesNo? ChestPain { get; set; }

        [Score(CardioVascularHealth = true, ScoreFactor = 2)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.PalpitationsLabel)]
        public EYesNo? Palpitations { get; set; }

        [Score(CardioVascularHealth = true, ScoreFactor = 2)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EasilyOutOfBreathLabel)]
        public EYesNo? EasilyOutOfBreath { get; set; }

        #endregion

        #region General Health 

        public List<PropertyInfo> GeneralHealthProperties() => this
            .GetProperties().Where(e => e.GetAttribute<QuestionCategoryAttribute>()?.Category == EQuestionCategory.GeneralHealth).ToList();

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthIssuesLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string CurrentHealthIssues { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HealthGoalsLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string HealthGoals { get; set; }

        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CurrentHealthLevelLabel)]
        [Min(1)]
        [Max(10)]
        public int? CurrentHealthLevel { get; set; }

        public int GetCurrentHealthScore() => CurrentHealthLevel.HasValue ? CurrentHealthLevel.Value * 10 : 0;

        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NutritionExpertiseLevelLabel)]
        [Min(1)]
        [Max(10)]
        public int? NutritionExpertiseLevel { get; set; }

        public int GetNutritionExpertiseScore() => NutritionExpertiseLevel.HasValue ? NutritionExpertiseLevel.Value * 10 : 0;

        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.EnjoysCookingLabel)]
        public EYesNo? EnjoysCooking { get; set; }

        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [UIHint("Frequency")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CookingFrequencyLabel)]
        public EFrequency? CookingFrequency { get; set; }

        #region Digestion

        [Score(DigestiveHealth = true, VataDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BloatingLabel)]
        public EYesNo? Bloating { get; set; }

        [Score(DigestiveHealth = true, PittaDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IrritableBowerlLabel)]
        public EYesNo? IBS { get; set; }

        [Score(DigestiveHealth = true, VataDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GasLabel)]
        public EYesNo? Gas { get; set; }

        [Score(DigestiveHealth = true, PittaDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LooseStoolLabel)]
        public EYesNo? LooseStool { get; set; }

        [Score(DigestiveHealth = true, VataDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ConstipationLabel)]
        public EYesNo? Constipation { get; set; }

        [Score(DigestiveHealth = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AbdominalPainOrCrampingLabel)]
        public EYesNo? AbdominalPainOrCramping { get; set; }

        [Score(DigestiveHealth = true, IsYang = true, PittaDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinIssuesLabel)]
        public EYesNo? SkinIssues { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinIssuesDetailsLabel)]
        [StringLength(1111)]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth, AllowNull = true)]
        [DataType(DataType.MultilineText)]
        public string SkinIssuesDetails { get; set; }

        [Score(DigestiveHealth = true, Immunity = true, Detoxification = true, DentalHealth = true, KaphaDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CoatedTongueLabel)]
        public EYesNo? CoatedTongue { get; set; }

        public int GetDigestionIssuesScore()
        {
            return GetScore(e => e.DigestiveHealth);
        }

        #endregion

        #region Immune System

        [Score(Immunity = true)]
        [UIHint("Severity")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.InfectionSeverityLabel)]
        public ESeverity? InfectionSeverity { get; set; }

        [Score(Immunity = true, Detoxification = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AllergiesAndSensitivitiesLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string AllergiesAndSensitivitiesDetails { get; set; }

        [Score(Immunity = true, UrologicalHealth = true, Detoxification = true, IsYin = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.UTILabel)]
        public EYesNo? UTI { get; set; }

        public int GetImmunityIssuesScore()
        {
            return GetScore(e => e.Immunity,
                () => GetDigestionIssuesScore() > 0);
        }

        #endregion

        #region Yin Disease

        [Score(Restorative = true, IsYin = true, VataDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ColdExtremitiesLabel)]
        public EYesNo? ColdExtremities { get; set; }

        [Score(Restorative = true, IsYin = true, VataDosha = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ColdIntolerantLabel)]
        public EYesNo? ColdIntolerant { get; set; }

        [Score(Restorative = true, Immunity = true, IsYin = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CandidaFungusLabel)]
        public EYesNo? CandidaAndFungus { get; set; }

        public int GetYinImbalanceScore()
        {
            return GetScore(e => e.IsYin);
        }

        #endregion

        #region Inflammation

        [Score(AntiInflammatory = true, Immunity = true, Detoxification = true, IsYang = true, PittaDosha = true, IsInflammation = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.JointInflammationLabel)]
        public EYesNo? JointInflammation { get; set; }

        [Score(AntiInflammatory = true, Detoxification = true, Immunity = true, IsYang = true, PittaDosha = true, IsInflammation = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AutoImmunityLabel)]
        public EYesNo? Autoimmunity { get; set; }

        public int GetYangImbalanceScore()
        {
            return GetScore(e => e.IsYang);
        }

        #endregion

        #region Other

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SurgeryDetailsLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string SurgeryDetails { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.PrescriptionMedicationLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string PrescriptionMedication { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SupplementsLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string Supplements { get; set; }

        [Score(Detoxification = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SmokeLabel)]
        public EYesNo? Smoke { get; set; }

        [Score(NeurologicalHealth = true, Mood = true, Cognition = true, StressRelief = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DrinksAlcoholLabel)]
        public EYesNo? DrinksAlcohol { get; set; }

        [FileSourceInfo("upload/health", Filter = EFilesSourceFilter.Images)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.PhotoOfEyesLabel)]
        public FileSource EyesFileSource { get; set; }

        #endregion

        #region Trauma

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.PhysicalTraumaLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string PhysicalTraumaDetails { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DrinksAlcoholLabel)]
        public EYesNo? EmotionalTrauma { get; set; }

        #endregion

        #region Teeth

        [Score(Detoxification = true, DentalHealth = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AmalgamFillingsLabel)]
        public EYesNo? AmalgamFillings { get; set; }

        [Score(Detoxification = true, DentalHealth = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AmalgamFillingsHistoryLabel)]
        public EYesNo? AmalgamFillingsHistory { get; set; }

        [Score(Immunity = true, DentalHealth = true, Restorative = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ToothPainLabel)]
        public EYesNo? ToothPain { get; set; }

        [Score(Immunity = true, DentalHealth = true, Restorative = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TMJLabel)]
        public EYesNo? TMJ { get; set; }

        [Score(Immunity = true, DentalHealth = true, Restorative = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CrackedTeeth)]
        public EYesNo? CrackedTeeth { get; set; }

        [Score(Immunity = true, DentalHealth = true, Restorative = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.Cavities)]
        public EYesNo? Cavities { get; set; }

        [Score(Immunity = true, DentalHealth = true, Restorative = true)]
        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.GeneralHealth)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RootCanalsLabel)]
        public EYesNo? RootCanals { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DentalIssuesLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string DentalIssues { get; set; }

        #endregion

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ExerciseLabel)]
        [StringLength(1111)]
        [DataType(DataType.MultilineText)]
        public string Exercise { get; set; }

        public bool IsGeneralHealthComplete() => IsCategoryComplete(e => e.Category == EQuestionCategory.GeneralHealth);

        #endregion

        public static List<PropertyInfo> GetPropertiesWithScoreAttribute()
        {
            return typeof(HealthQuestionnaire).GetProperties().Where(e => e.GetAttribute<ScoreAttribute>() != null && e.PropertyType == typeof(EYesNo?) || e.PropertyType == typeof(EYesNo)).ToList();
        }

        public static List<PropertyInfo> GetPropertiesWithQuestionCategoryAttribute()
        {
            return typeof(HealthQuestionnaire).GetProperties().Where(e => e.GetAttribute<QuestionCategoryAttribute>() != null).ToList();
        }

        private bool IsCategoryComplete(Func<QuestionCategoryAttribute, bool> condition)
        {
            var categoryQuestions = GetPropertiesWithQuestionCategoryAttribute()
                .Select(e => new
                {
                    PropertyInfo = e,
                    QuestionCategoryAttribute = e.GetAttribute<QuestionCategoryAttribute>()
                })
                .Where(e => condition(e.QuestionCategoryAttribute)).ToList();

            return categoryQuestions
                .All(e =>
                       {
                           var value = this.GetProperty(e.PropertyInfo);
                           var valueIsNull = value == null;
                           var isComplete = !valueIsNull 
                                    && (e.QuestionCategoryAttribute.MustBeGreaterThanZero 
                                        ? double.Parse(value.ToString()) > 0 
                                        : true )
                                    || e.QuestionCategoryAttribute.AllowNull;

                           return isComplete;
                       });
        }
    }
}
