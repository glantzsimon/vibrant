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

        #region Blood Analysis

        [UIHint("BloodGroup")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BloodGroupLabel)]
        public EBloodGroup BloodGroup { get; set; }

        [UIHint("RhesusFactor")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RhesusFactorLabel)]
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

        public bool IsBloodAnalysisComplete() =>
            BloodGroup != EBloodGroup.NotSure && RhesusFactor != ERhesusFactor.NotSure;

        #endregion

        #region Acetylation Status

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.MedicationSensitivityLabel)]
        public EYesNo SensitivityToMedications { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CaffeineSensitivityLabel)]
        public EYesNo SensitiveToCaffeine { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CaffeineAffectsSleepLabel)]
        public EYesNo CaffeineAffectsSleep { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SensitiveToMoldLabel)]
        public EYesNo SensitiveToMold { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SensitiveToEnvironmentalChemicalsLabel)]
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

        public bool IsAcetylationStatusComplete() =>
            SensitivityToMedications != EYesNo.Unanswered &&
            SensitiveToCaffeine != EYesNo.Unanswered &&
            CaffeineAffectsSleep != EYesNo.Unanswered &&
            SensitiveToMold != EYesNo.Unanswered &&
            SensitiveToEnvironmentalChemicals != EYesNo.Unanswered;

        #endregion

        #region Biometrics

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StandingHeightLabel)]
        public double StandingHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StandingHeightLabel)]
        public double SittingHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ChairHeightLabel)]
        public double ChairHeight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthLabel)]
        public double TorsoLength => SittingHeight - ChairHeight;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalLegLengthLabel)]
        public double TotalLegLength => StandingHeight - TorsoLength;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowerLegLengthLabel)]
        public double LowerLegLength { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowerLegLengthLabel)]
        public double UpperLegLength { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthGreatherThanLegLengthLabel)]
        public bool IsTorsoLengthGreaterThanLegLength() => TorsoLength >= TotalLegLength;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IsLowerLegLengthGreaterThanUpperLegLengthLabel)]
        public bool IsLowerLegLengthGreaterThanUpperLegLength() => LowerLegLength >= UpperLegLength;

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerLengthLabel)]
        public double IndexFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerLengthLabel)]
        public double IndexFingerLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerLengthLabel)]
        public int RingFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
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
        public double WaistSize { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HipSizeLabel)]
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
        public double HeadWidth { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WaistSizeLabel)]
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
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TendonsAndSinewsVisibleLabel)]
        public EYesNo TendonsAndSinewsVisible { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WristsAndAnklesPaddedLabel)]
        public EYesNo WristsAndAnklesLookPadded { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GainsMuscleEasilyLabel)]
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

        public bool IsBiometricsComplete() =>
            StandingHeight > 0 &&
            SittingHeight > 0 &&
            ChairHeight > 0 &&
            TorsoLength > 0 &&
            TotalLegLength > 0 &&
            LowerLegLength > 0 &&
            UpperLegLength > 0 &&
            IndexFingerLengthLeft > 0 &&
            IndexFingerLengthRight > 0 &&
            RingFingerLengthLeft > 0 &&
            RingFingerLengthRight > 0 &&
            WaistSize > 0 &&
            HipSize > 0 &&
            HeadWidth > 0 &&
            HeadLength > 0 &&

            TendonsAndSinewsVisible != EYesNo.Unanswered &&
            WristsAndAnklesLookPadded != EYesNo.Unanswered &&
            GainsMuscleEasily != EYesNo.Unanswered &&

            SpaceBetweenThighs != EGapSize.Unanswered &&

            GonialAngle != EGonialAngle.Unanswered &&

            SomatoType != ESomatoType.Unanswered;

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
        public EYesNo IndexFingerprintsMatch { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftThumprintLabel)]
        public EFingerprintType LeftThumprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerprintLabel)]
        public EFingerprintType LeftIndexFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftMiddleFingerprintLabel)]
        public EFingerprintType LeftMiddleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerprintLabel)]
        public EFingerprintType LeftRingFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftLittleFingerprintLabel)]
        public EFingerprintType LeftLittleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightThumprintLabel)]
        public EFingerprintType RightThumprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerprintLabel)]
        public EFingerprintType RightIndexFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightMiddleFingerprintLabel)]
        public EFingerprintType RightMiddleFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerprintLabel)]
        public EFingerprintType RightRingFingerprintType { get; set; }

        [UIHint("FingerPrint")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightLittleFingerprintLabel)]
        public EFingerprintType RightLittleFingerprintType { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftHandedLabel)]
        public EYesNo LeftHanded { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AmbidextrousLabel)]
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

            if (CountFingerprintsByType(EFingerprintType.Whorl) >= 5)
            {
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
                results.Add(EGenoType.Teacher);
            }

            if (CountFingerprintsByType(EFingerprintType.Arch) >= 2)
            {
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
                results.Add(EGenoType.Warrior);
            }

            if (CountFingerprintsByType(EFingerprintType.Loop) >= 8)
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

        public bool IsDermatoglyphicsComplete() =>
            LeftThumprintType != EFingerprintType.Unanswered &&
            LeftIndexFingerprintType != EFingerprintType.Unanswered &&
            LeftMiddleFingerprintType != EFingerprintType.Unanswered &&
            LeftRingFingerprintType != EFingerprintType.Unanswered &&
            LeftLittleFingerprintType != EFingerprintType.Unanswered &&
            RightThumprintType != EFingerprintType.Unanswered &&
            RightIndexFingerprintType != EFingerprintType.Unanswered &&
            RightMiddleFingerprintType != EFingerprintType.Unanswered &&
            RightRingFingerprintType != EFingerprintType.Unanswered &&
            RightLittleFingerprintType != EFingerprintType.Unanswered &&

            IndexFingerprintsMatch != EYesNo.Unanswered &&
            LeftHanded != EYesNo.Unanswered &&
            Ambidextrous != EYesNo.Unanswered;

        #endregion

        #region Dentition

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IncisorShovellingLabel)]
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

        public bool IsDentitionComplete() =>
            IncisorShovelling != EYesNo.Unanswered;

        #endregion

        #region Taster Status

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CruciferousVegetablesVeryBitterLabel)]
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

        public bool IsTasterStatusComplete() => CruciferousVegetablesTasteVeryBitter != ETaste.Unanswered;

        #endregion

        #region Family History

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfNeurologicalDiseaseLabel)]
        public EYesNo FamilyHistoryOfNeurologicalDisease { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfHeartDiseaseStrokeOrDiabetesLabel)]
        public EYesNo FamilyHistoryOfHeartDiseaseStrokeOrDiabetes { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfCancerLabel)]
        public EYesNo FamilyHistoryOfCancer { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfAutoimmuneDiseaseLabel)]
        public EYesNo FamilyHistoryOfAutoimmuneDisease { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FamilyHistoryOfSubstanceDependencyLabel)]
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

        public bool IsFamilyHistoryComplete() =>
            FamilyHistoryOfNeurologicalDisease != EYesNo.Unanswered &&
            FamilyHistoryOfHeartDiseaseStrokeOrDiabetes != EYesNo.Unanswered &&
            FamilyHistoryOfCancer != EYesNo.Unanswered &&
            FamilyHistoryOfAutoimmuneDisease != EYesNo.Unanswered &&
            FamilyHistoryOfSubstanceDependency != EYesNo.Unanswered;

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