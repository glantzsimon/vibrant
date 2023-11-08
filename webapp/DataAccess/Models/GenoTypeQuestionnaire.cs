using DataAnnotationsExtensions;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    public partial class HealthQuestionnaire
    {
        public List<EGenoType> GetTotalGenotypeScore()
        {
            return GetGenoTypeFromSpaceBetweenThighs()
                    .Concat(GetGenoTypeFromTendonsAndSinewsAndMuscles())
                    .Concat(GetGenoTypeFromFingerprints()).ToList();
        }

        #region Biometrics

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthLabel)]
        [Required]
        public int TorsoLength { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftLegLengthLabel)]
        [Required]
        public int LegLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightLegLengthLabel)]
        [Required]
        public int LegLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerLengthLabel)]
        [Required]
        public int IndexFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerLengthLabel)]
        [Required]
        public int IndexFingerLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerLengthLabel)]
        [Required]
        public int RingFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
        [Required]
        public int RingFingerLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SpaceBetweenThighsLabel)]
        [Required]
        public int SpaceBetweenThighs { get; set; }

        public List<EGenoType> GetGenoTypeFromSpaceBetweenThighs()
        {
            var results = new List<EGenoType>();
            if (SpaceBetweenThighs <= 0.3)
            {
                results.Add(EGenoType.Gatherer);
            }
            else if (NumberOfMatchingFingerprints >= 1)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Warrior);
            }
            else if (NumberOfMatchingFingerprints >= 0.5)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TendonsAndSinewsVisibleLabel)]
        [Required]
        public EYesNo TendonsAndSinewsVisible { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WristsAndAnklesPaddedLabel)]
        [Required]
        public EYesNo WristsAndAnklesLookPadded { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GainsMuscleEasilyLabel)]
        [Required]
        public EYesNo GainsMuscleEasily { get; set; }

        public List<EGenoType> GetGenoTypeFromTendonsAndSinewsAndMuscles()
        {
            var results = new List<EGenoType>();
            if (TendonsAndSinewsVisible == EYesNo.Yes)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Explorer);
            }

            if (WristsAndAnklesLookPadded == EYesNo.Yes)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
            }

            if (GainsMuscleEasily == EYesNo.Yes)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
            }

            return results;
        }

        #endregion

        #region Blood Analysis

        [UIHint("BloodGroup")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BloodGroupLabel)]
        [Required]
        public EBloodGroup BloodGroup { get; set; }

        [UIHint("RhesusFactor")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RhesusFactorLabel)]
        [Required]
        public ERhesusFactor RhesusFactor { get; set; }

        #endregion

        #region Acetylation Status

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.MedicationSensitivityLabel)]
        [Required]
        public EYesNo SensitivityToMedications { get; set; }

        [UIHint("Range")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CaffeineSensitivityLabel)]
        [Required]
        [Min(1)]
        [Max(10)]
        public int SensitivityToCaffeine { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CaffeineAffectsSleepLabel)]
        [Required]
        public EYesNo CaffeineAffectsSleep { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SensitiveToMoldLabel)]
        [Required]
        public EYesNo SensitiveToMold { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SensitiveToEnvironmentalChemicalsLabel)]
        [Required]
        public EYesNo SensitiveToEnvironmentalChemicals { get; set; }

        #endregion

        #region Family History

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfNeurologicalDiseaseLabel)]
        [Required]
        public EYesNo FamilyHistoryOfNeurologicalDisease { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfHeartDiseaseStrokeOrDiabetesLabel)]
        [Required]
        public EYesNo FamilyHistoryOfHeartDiseaseStrokeOrDiabetes { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfCancerLabel)]
        [Required]
        public EYesNo FamilyHistoryOfCancer { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfAutoimmuneDiseaseLabel)]
        [Required]
        public EYesNo FamilyHistoryOfAutoimmuneDisease { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfSubstanceDependencyLabel)]
        [Required]
        public EYesNo FamilyHistoryOfSubstanceDependency { get; set; }

        #endregion

        #region Dermatoglyphics

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NumberOfMatchingFingerprintsLabel)]
        [Required]
        [Min(0)]
        [Max(5)]
        public int NumberOfMatchingFingerprints { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NumberOfMatchingFingerprintsLabel)]
        [Required]

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IndexFingersMatchLabel)]
        [Required]
        public EYesNo IndexFingerprintsMatch { get; set; }

        public List<EGenoType> GetGenoTypeFromFingerprints()
        {
            var results = new List<EGenoType>();
            if (NumberOfMatchingFingerprints <= 2)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Gatherer);
            }
            else if (NumberOfMatchingFingerprints >= 4)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Nomad);
            }
            else
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Teacher);
            }

            if (IndexFingerprintsMatch == EYesNo.Yes)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            return results;
        }

        #endregion

        #region Dentition

        #endregion
    }
}