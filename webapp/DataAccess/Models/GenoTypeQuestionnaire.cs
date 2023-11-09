using K9.Base.DataAccessLayer.Enums;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    public partial class HealthQuestionnaire
    {
        public GenoTypeStrengthTestResult CalculateGenotype()
        {
            var genotypes = CalculateGenoTypes();

            var strengthTestResults = GetGenoTypeFromSpaceBetweenThighs()
                .Concat(GetGenoTypeFromTendonsAndSinewsAndMuscles())
                .Concat(GetGenoTypeFromGonialAngle())
                .Concat(GetGenoTypeFromHeadShape())
                .Concat(GetGenoTypeFromRhesusFactor())
                .Concat(GetGenoTypeFromTasterStatus())
                .Concat(GetGenoTypeFromChemicalSensitivity())
                .Concat(GetGenoTypeFromSomatoType())
                .Concat(GetGenoTypesFromFamilyHistory())
                .Concat(GetGenoTypeFromHandedness())
                .Concat(GetGenoTypesFromIncisorShovelling())
                .Concat(GetGenoTypeFromFingerprints())
                .Where(e => genotypes.Contains(e))
                .GroupBy(e => e).Select(group => new GenoTypeStrengthTestResult
                {
                    GenoType = group.Key,
                    Count = group.Count()
                });

            return strengthTestResults.OrderByDescending(e => e.Count).First();
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
        public double LowerLegLength { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowerLegLengthLabel)]
        [Required]
        public double UpperLegLength { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthGreatherThanLegLengthLabel)]
        public bool IsTorsoLengthGreaterThanLegLength() => TorsoLength >= TotalLegLength;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLowerLegLengthGreaterThanUpperLegLengthLabel)]
        public bool IsLowerLegLengthGreaterThanUpperLegLength() => LowerLegLength >= UpperLegLength;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerLengthLabel)]
        [Required]
        public double IndexFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerLengthLabel)]
        [Required]
        public double IndexFingerLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerLengthLabel)]
        [Required]
        public int RingFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
        [Required]
        public double RingFingerLengthRight { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLeftIndexFingerLongerThanLeftRingFingerLabel)]
        public bool IsIndexFingerLongerThanRingFingerLeft() => IndexFingerLengthLeft >= RingFingerLengthLeft;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsRightIndexFingerLongerThanRightRingFingerLabel)]
        public bool IsIndexFingerLongerThanRingFingerRight() => IndexFingerLengthRight >= RingFingerLengthRight;

        public bool IndexFingersAreLongerThanRingFingersOnBothHands() =>
            IsIndexFingerLongerThanRingFingerLeft() && IsIndexFingerLongerThanRingFingerRight();

        public bool RingFingersAreLongerThanIndexFingersOnBothHands() =>
            !IsIndexFingerLongerThanRingFingerLeft() && !IsIndexFingerLongerThanRingFingerRight();

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
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
            }
            else if (SpaceBetweenThighs == EGapSize.Small)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);

                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Warrior);
            }
            else if (SpaceBetweenThighs == EGapSize.Large)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);

                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);

                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WaistSizeLabel)]
        [Required]
        public double WaistSize { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HipSizeLabel)]
        [Required]
        public double HipSize { get; set; }

        public ERatio GetWaistToHipRatio()
        {
            var ratio = WaistSize / HipSize;

            if (Gender == EGender.Male)
            {
                if (YearsOld < 50)
                {
                    if (ratio > 0.96)
                    {
                        return ERatio.High;
                    }

                    if (ratio >= 0.91)
                    {
                        return ERatio.Average;
                    }

                    return ERatio.Ideal;
                }
                else
                {
                    if (ratio > 0.99)
                    {
                        return ERatio.High;
                    }

                    if (ratio >= 0.93)
                    {
                        return ERatio.Average;
                    }

                    return ERatio.Ideal;
                }
            }
            else if (Gender == EGender.Female || Gender == EGender.Other)
            {
                if (YearsOld < 50)
                {
                    if (ratio > 0.79)
                    {
                        return ERatio.High;
                    }

                    if (ratio >= 0.71)
                    {
                        return ERatio.Average;
                    }

                    return ERatio.Ideal;
                }
                else
                {
                    if (ratio > 0.84)
                    {
                        return ERatio.High;
                    }

                    if (ratio >= 0.75)
                    {
                        return ERatio.Average;
                    }

                    return ERatio.Ideal;
                }
            }

            return ERatio.Average;
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
                results.Add(EGenoType.Gatherer);

                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);

                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }

            if (GonialAngle == EGonialAngle.NarrowAngle)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);

                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);

                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            return results;
        }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HeadShapeLabel)]
        public EHeadShape GetHeadShape()
        {
            var cephalicIndex = GetCephalicIndex();
            if (cephalicIndex < 75)
            {
                return EHeadShape.Dolichocephalic;
            }
            if (cephalicIndex <= 80)
            {
                return EHeadShape.Mesocephalic;
            }

            return EHeadShape.Brachycephalic;
        }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WaistSizeLabel)]
        [Required]
        public double HeadWidth { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WaistSizeLabel)]
        [Required]
        public double HeadLength { get; set; }

        public double GetCephalicIndex()
        {
            return (HeadWidth / HeadLength) * 100;
        }

        public List<EGenoType> GetGenoTypeFromHeadShape()
        {
            var results = new List<EGenoType>();
            if (GetHeadShape() == EHeadShape.Dolichocephalic)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if (GetHeadShape() == EHeadShape.Brachycephalic)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);

                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CruciferousVegetablesVeryBitterLabel)]
        [Required]
        public ETaste CruciferousVegetablesTasteVeryBitter { get; set; }

        public List<EGenoType> GetGenoTypeFromTasterStatus()
        {
            var results = new List<EGenoType>();
            if (CruciferousVegetablesTasteVeryBitter == ETaste.NoticeablyBitter)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);

                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            else if (CruciferousVegetablesTasteVeryBitter == ETaste.NotBitter)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);

                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            else if (CruciferousVegetablesTasteVeryBitter == ETaste.SlightlyBitter)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);

                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
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
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);

                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Explorer);
            }

            if (WristsAndAnklesLookPadded == EYesNo.Yes)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);

                results.Add(EGenoType.Warrior);
            }

            if (GainsMuscleEasily == EYesNo.Yes)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            return results;
        }

        [UIHint("SomatoType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GainsMuscleEasilyLabel)]
        [Required]
        public ESomatoType SomatoType { get; set; }

        public List<EGenoType> GetGenoTypeFromSomatoType()
        {
            var results = new List<EGenoType>();
            
            if (SomatoType == ESomatoType.Ectomorph)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
            }

            if (SomatoType == ESomatoType.Ectomesomorph || GetWaistToHipRatio() == ERatio.Ideal)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
            }

            if (SomatoType == ESomatoType.Endomorph || GetWaistToHipRatio() == ERatio.High)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
            }

            if (SomatoType == ESomatoType.Mesomorph || GetWaistToHipRatio() == ERatio.Ideal)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            if (SomatoType == ESomatoType.MesoEndomorph || GetWaistToHipRatio() == ERatio.High)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
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

        public List<EGenoType> GetGenoTypeFromRhesusFactor()
        {
            var results = new List<EGenoType>();
            
            if (RhesusFactor == ERhesusFactor.Negative)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }
            
            return results;
        }

        #endregion

        #region Acetylation Status

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.MedicationSensitivityLabel)]
        [Required]
        public EYesNo SensitivityToMedications { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CaffeineSensitivityLabel)]
        [Required]
        public EYesNo SensitiveToCaffeine { get; set; }

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

        public List<EGenoType> GetGenoTypeFromChemicalSensitivity()
        {
            var results = new List<EGenoType>();
            
            if (SensitivityToMedications == EYesNo.Yes 
                || SensitiveToCaffeine == EYesNo.Yes
                || CaffeineAffectsSleep == EYesNo.Yes
                || SensitiveToMold == EYesNo.Yes
                || SensitiveToEnvironmentalChemicals == EYesNo.Yes)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            if (SensitiveToCaffeine == EYesNo.No)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }
            
            return results;
        }

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

        public List<EGenoType> GetGenoTypesFromFamilyHistory()
        {
            var results = new List<EGenoType>();
            if (FamilyHistoryOfAutoimmuneDisease == EYesNo.Yes)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
            }

            if (FamilyHistoryOfHeartDiseaseStrokeOrDiabetes == EYesNo.Yes)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);

                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if (FamilyHistoryOfCancer == EYesNo.Yes)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
            }

            if (FamilyHistoryOfNeurologicalDisease == EYesNo.Yes)
            {
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }


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

        public List<EGenoType> GetGenoTypeFromHandedness()
        {
            var results = new List<EGenoType>();

            if (LeftHanded == EYesNo.Yes || Ambidextrous == EYesNo.Yes)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            return results;
        }

        public List<EGenoType> GetGenoTypeFromFingerprints()
        {
            var results = new List<EGenoType>();

            if (GetNumberOfMatchingFingerprints() <= 2)
            {
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
                results.Add(EGenoType.Gatherer);
            }
            else if (GetNumberOfMatchingFingerprints() >= 4)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);

                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }
            else
            {
                results.Add(EGenoType.Teacher);
            }

            if (IndexFingerprintsMatch == EYesNo.No)
            {
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
                results.Add(EGenoType.Explorer);
            }

            if(CountFingerprintsByType(EFingerprintType.Whorl) >= 5)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
            }

            if(CountFingerprintsByType(EFingerprintType.Arch) >= 2)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if(CountFingerprintsByType(EFingerprintType.Loop) >= 8)
            {
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        private int CountFingerprintsByType(EFingerprintType type)
        {
            var fingerPrints = new List<EFingerprintType>
            {
                LeftThumprintType,
                LeftIndexFingerprintType,
                LeftMiddleFingerprintType,
                LeftRingFingerprintType,
                LeftLittleFingerprintType,
                RightThumprintType,
                RightIndexFingerprintType,
                RightMiddleFingerprintType,
                RightRingFingerprintType,
                RightLittleFingerprintType
            };

            return fingerPrints.Count(e => e == type);
        }

        #endregion

        #region Dentition

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IncisorShovellingLabel)]
        [Required]
        public EYesNo IncisorShovelling { get; set; }

        public List<EGenoType> GetGenoTypesFromIncisorShovelling()
        {
            var results = new List<EGenoType>();

            if (IncisorShovelling == EYesNo.Yes)
            {
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);
                results.Add(EGenoType.Hunter);

                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
                results.Add(EGenoType.Nomad);
            }

            return results;
        }

        #endregion

        private List<EGenoType> CalculateGenoTypes()
        {
            var genotypes = new List<EGenoType>();

            if (IsTorsoLengthGreaterThanLegLength())
            {
                if (IsLowerLegLengthGreaterThanUpperLegLength())
                {
                    if (IndexFingersAreLongerThanRingFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Explorer,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (Gender == EGender.Male)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Nomad
                                    });
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer,
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (RingFingersAreLongerThanIndexFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Teacher,
                                EGenoType.Explorer,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Teacher
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Nomad,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Hunter,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Hunter
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        // Finger length assymetrical
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Teacher,
                                EGenoType.Explorer,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Teacher
                                });
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Nomad,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer,
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    // Upper leg is longer
                {
                    if (IndexFingersAreLongerThanRingFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Teacher,
                                EGenoType.Explorer,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Teacher,
                                    EGenoType.Explorer
                                });
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer,
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer,
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer
                                    });
                                }
                                else
                                {
                                    if (Gender == EGender.Male)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer,
                                            EGenoType.Explorer
                                        });
                                    }
                                    else
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                }
                            }
                        }
                    }
                    else if (RingFingersAreLongerThanIndexFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Teacher,
                                EGenoType.Explorer,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Teacher
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Teacher,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Nomad
                                        });
                                    }
                                    else
                                    {
                                        if (RhesusFactor == ERhesusFactor.Negative)
                                        {
                                            if (Gender == EGender.Male)
                                            {
                                                genotypes.AddRange(new[]
                                                {
                                                    EGenoType.Nomad,
                                                    EGenoType.Explorer
                                                });
                                            }
                                            else
                                            {
                                                genotypes.AddRange(new[]
                                                {
                                                    EGenoType.Explorer
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer,
                                                EGenoType.Nomad
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Hunter,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        // Finger length assymetrical
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Teacher,
                                EGenoType.Explorer,
                                EGenoType.Warrior
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Teacher
                                });
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                // Legs are longer than torso
            {
                if (IsLowerLegLengthGreaterThanUpperLegLength())
                {
                    if (IndexFingersAreLongerThanRingFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Explorer,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer,
                                                EGenoType.Warrior
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Nomad
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (RingFingersAreLongerThanIndexFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Teacher,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Teacher
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Teacher
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior,
                                        EGenoType.Nomad
                                    });
                                }
                                else if (RhesusFactor == ERhesusFactor.Positive)
                                {
                                    if (Gender == EGender.Male)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Nomad
                                        });
                                    }
                                    else
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                }
                                else if (RhesusFactor == ERhesusFactor.Negative)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior
                                    });
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Nomad,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Hunter
                                });
                            }
                        }
                    }
                    else
                        // Finger lengths are assymetrical
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Teacher,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Teacher,
                                    EGenoType.Warrior
                                });
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Nomad,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Hunter,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Hunter
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Hunter,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                        }

                    }
                }
                else
                    // Upper leg is longer
                {
                    if (IndexFingersAreLongerThanRingFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Gatherer,
                                EGenoType.Explorer,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Warrior,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Nomad,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Gatherer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (RingFingersAreLongerThanIndexFingersOnBothHands())
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Explorer,
                                EGenoType.Warrior,
                                EGenoType.Nomad
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer,
                                                EGenoType.Warrior
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Warrior
                                            });
                                        }
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer,
                                            EGenoType.Warrior
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Warrior
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Warrior,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Explorer,
                                        EGenoType.Nomad
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Nomad
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Nomad,
                                                EGenoType.Explorer
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Hunter,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Hunter
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        if (Gender == EGender.Male)
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Hunter
                                            });
                                        }
                                        else
                                        {
                                            genotypes.AddRange(new[]
                                            {
                                                EGenoType.Explorer
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        // Finger lengths are assymetrical
                    {
                        if (BloodGroup == EBloodGroup.NotSure)
                        {
                            genotypes.AddRange(new[]
                            {
                                EGenoType.Hunter,
                                EGenoType.Gatherer,
                                EGenoType.Teacher,
                                EGenoType.Explorer
                            });
                        }
                        else
                        {
                            if (BloodGroup == EBloodGroup.A)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Teacher,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Teacher,
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.AB)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Teacher,
                                    EGenoType.Explorer
                                });
                            }
                            else if (BloodGroup == EBloodGroup.B)
                            {
                                if (RhesusFactor == ERhesusFactor.NotSure)
                                {
                                    genotypes.AddRange(new[]
                                    {
                                        EGenoType.Gatherer,
                                        EGenoType.Explorer
                                    });
                                }
                                else
                                {
                                    if (RhesusFactor == ERhesusFactor.Positive)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Gatherer
                                        });
                                    }
                                    else if (RhesusFactor == ERhesusFactor.Negative)
                                    {
                                        genotypes.AddRange(new[]
                                        {
                                            EGenoType.Explorer
                                        });
                                    }
                                }
                            }
                            else if (BloodGroup == EBloodGroup.O)
                            {
                                genotypes.AddRange(new[]
                                {
                                    EGenoType.Hunter,
                                    EGenoType.Gatherer
                                });
                            }
                        }
                    }
                }
            }

            return genotypes;
        }
    }
}