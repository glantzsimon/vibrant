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
                    .Concat(GetGenoTypeFromGonialAngle())
                    .Concat(GetGenoTypeFromHeadShape())
                    .Concat(GetGenoTypeFromFingerprints()).ToList();
        }

        #region Biometrics

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StandingHeightLabel)]
        [Required]
        public double StandingHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StandingHeightLabel)]
        [Required]
        public double SittingHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ChairHeightLabel)]
        [Required]
        public double ChairHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthLabel)]
        [Required]
        public double TorsoLength => SittingHeight - ChairHeight;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalLegLengthLabel)]
        [Required]
        public double TotalLegLength => StandingHeight - TorsoLength;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowerLegLengthLabel)]
        [Required]
        public int LowerLegLength { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowerLegLengthLabel)]
        [Required]
        public int UpperLegLength { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthGreatherThanLegLengthLabel)]
        public bool IsTorsoLengthGreaterThanLegLength() => TorsoLength >= TotalLegLength;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLowerLegLengthGreaterThanUpperLegLengthLabel)]
        public bool IsLowerLegLengthGreaterThanUpperLegLength() => LowerLegLength >= UpperLegLength;

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

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLeftIndexFingerLongerThanLeftRingFingerLabel)]
        public bool IsIndexFingerLongerThanRingFingerLeft() => IndexFingerLengthLeft >= RingFingerLengthLeft;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsRightIndexFingerLongerThanRightRingFingerLabel)]
        public bool IsIndexFingerLongerThanRingFingerRight() => IndexFingerLengthRight >= RingFingerLengthRight;
        
        [UIHint("GapSize")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SpaceBetweenThighsLabel)]
        [Required]
        public EGapSize SpaceBetweenThighs { get; set; }

        public List<EGenoType> GetGenoTypeFromSpaceBetweenThighs()
        {
            var results = new List<EGenoType>();
            if (SpaceBetweenThighs == EGapSize.Touching)
            {
                results.Add(EGenoType.Gatherer);
            }
            else if (SpaceBetweenThighs == EGapSize.Small)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Warrior);
            }
            else if (SpaceBetweenThighs == EGapSize.Large)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WaistSizeLabel)]
        [Required]
        public int WaistSize { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HipSizeLabel)]
        [Required]
        public int HipSize { get; set; }

        public ERatio GetWaistToHipRatio()
        {
            var ratio = WaistSize / HipSize;

            if (ratio <= 0.7)
            {
                return ERatio.VeryLow;
            }
            if (ratio <= 0.8)
            {
                return ERatio.Low;
            }
            if (ratio <= 0.9)
            {
                return ERatio.Medium;
            }
            if (ratio <= 1)
            {
                return ERatio.High;
            }

            return ERatio.VeryHigh;
        }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GonialAngleLabel)]
        [Required]
        public EGonialAngle GonialAngle { get; set; }

        public List<EGenoType> GetGenoTypeFromGonialAngle()
        {
            var results = new List<EGenoType>();
            if (GonialAngle == EGonialAngle.WideAngle)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
            }

            if (GonialAngle == EGonialAngle.NarrowAngle)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }
           
            return results;
        }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HeadShapeLabel)]
        [Required]
        public EHeadShape HeadShape { get; set; }

        public List<EGenoType> GetGenoTypeFromHeadShape()
        {
            var results = new List<EGenoType>();
            if (HeadShape == EHeadShape.Elongated)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if (HeadShape == EHeadShape.Square)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Nomad);
            }
           
            return results;
        }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CruciferousVegetablesVeryBitterLabel)]
        [Required]
        public EYesNo CruciferousVegetablesTasteVeryBitter { get; set; }

        public List<EGenoType> GetGenoTypeFromTasterStatus()
        {
            var results = new List<EGenoType>();
            if (CruciferousVegetablesTasteVeryBitter == EYesNo.Yes)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Teacher);
            }

            if (CruciferousVegetablesTasteVeryBitter == EYesNo.No)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if (CruciferousVegetablesTasteVeryBitter == EYesNo.NotSure)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Gatherer);
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
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
            }

            return results;
        }

        [UIHint("SomatoType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GainsMuscleEasilyLabel)]
        [Required]
        public ESomatoType SomatoType { get; set; }

        //Waist to hip ratio

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
        public int GetNumberOfMatchingFingerprints()
        {
            var total = 0;

            if (LeftThumprintType == RightThumprintType)
            {
                total++;
            }
            if (LeftIndexFingerprintType == RightIndexFingerprintType)
            {
                total++;
            }
            if (LeftMiddleFingerprintType == RightMiddleFingerprintType)
            {
                total++;
            }
            if (LeftRingFingerprintType == RightRingFingerprintType)
            {
                total++;
            }
            if (LeftLittleFingerprintType == RightLittleFingerprintType)
            {
                total++;
            }

            return total;
        }
        
        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IndexFingersMatchLabel)]
        [Required]
        public EYesNo IndexFingerprintsMatch { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftThumprintLabel)]
        [Required]
        public EFingerprintType LeftThumprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerprintLabel)]
        [Required]
        public EFingerprintType LeftIndexFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftMiddleFingerprintLabel)]
        [Required]
        public EFingerprintType LeftMiddleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerprintLabel)]
        [Required]
        public EFingerprintType LeftRingFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftLittleFingerprintLabel)]
        [Required]
        public EFingerprintType LeftLittleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightThumprintLabel)]
        [Required]
        public EFingerprintType RightThumprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerprintLabel)]
        [Required]
        public EFingerprintType RightIndexFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightMiddleFingerprintLabel)]
        [Required]
        public EFingerprintType RightMiddleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerprintLabel)]
        [Required]
        public EFingerprintType RightRingFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightLittleFingerprintLabel)]
        [Required]
        public EFingerprintType RightLittleFingerprintType { get; set; }
        
        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftHandedLabel)]
        [Required]
        public EYesNo LeftHanded { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AmbidextrousLabel)]
        [Required]
        public EYesNo Ambidextrous { get; set; }

        public List<EGenoType> GetGenoTypeFromFingerprints()
        {
            var results = new List<EGenoType>();
            if (GetNumberOfMatchingFingerprints() <= 2)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Gatherer);
            }
            else if (GetNumberOfMatchingFingerprints() >= 4)
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

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IncisorShovellingLabel)]
        [Required]
        public EYesNo IncisorShovelling { get; set; }

        #endregion
    }
}