using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using K9.SharedLibrary.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    public partial class HealthQuestionnaire
    {
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("SkinType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinTypeLabel)]
        public ESkinType? SkinType { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("SleepQuality")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.SleepLabel)]
        public ESleepQuality? SleepQuality { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("WeightGain")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.WeightGainQuestionLabel)]
        public EWeightGain? WeightGain { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("BodyTemperature")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.BodyTemperatureQuestionLabel)]
        public EBodyTemperature? BodyTemperature { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("StressResponse")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.StressResponseQuestionLabel)]
        public EStressResponse? StressResponse { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("EyesType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.EyesTypeQuestionLabel)]
        public EEyesType? EyesType { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("Disposition")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.DispositionQuestionLabel)]
        public EDisposition? Disposition { get; set; }

        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [UIHint("HairType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.HairTypeQuestionLabel)]
        public EHairType? HairType { get; set; }

        #region Signs Of Vata Dosha Imbalance

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true, IsYin = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.NervousnessLabel)]
        public EYesNo? Nervousness { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.SpasmsLabel)]
        public EYesNo? Spasms { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true, IsYin = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.LowVigourLabel)]
        public EYesNo? LowVigour { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true, IsYin = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.WeightLossLabel)]
        public EYesNo? WeightLoss { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.NeuropathicPainLabel)]
        public EYesNo? NeuropathicPain { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.DryFlakySkinLabel)]
        public EYesNo? DryFlakySkin { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.IrregularDigestionLabel)]
        public EYesNo? IrregularDigestion { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.TroubleFallingAsleepLabel)]
        public EYesNo? TroubleFallingAsleep { get; set; }

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(VataDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.FrettingLabel)]
        public EYesNo? Fretting { get; set; }

        #endregion

        #region Signs of Pitta Dosha Imbalance

        [UIHint("YesNo")]
        [QuestionCategory(Category = EQuestionCategory.Doshas)]
        [Score(PittaDosha = true)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.FrettingLabel)]
        public EYesNo? UlcersAcidReflux { get; set; }

        #endregion

        public bool IsDoshasComplete()
        {
            return IsCategoryComplete(e => e.Category == EQuestionCategory.Doshas);
        }

        public Doshas GetPrakrutiDoshas()
        {
            var results = new List<EDosha>
            {
                SkinType.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                SleepQuality.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                WeightGain.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                BodyTemperature.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                EyesType.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                Disposition.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                HairType.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
                StressResponse.GetAttribute<DoshaEnumMetaDataAttribute>().Dosha,
            };

            var totalDoshas = results.Count;
            var vataCount = results.Count(e => e == EDosha.Vata);
            var pittaCount = results.Count(e => e == EDosha.Pitta);
            var kaphaCount = results.Count(e => e == EDosha.Kapha);

            return new Doshas
            {
                VataDoshaScore = vataCount > 0 ? (double)vataCount / totalDoshas : 0,
                PittaDoshaScore = pittaCount > 0 ? (double)pittaCount / totalDoshas : 0,
                KaphaDoshaScore = kaphaCount > 0 ? (double)kaphaCount / totalDoshas : 0
            };
        }

        public Doshas GetVikrutiDoshas()
        {
            var vataDoshaScore = GetScore(e => e.VataDosha);
            var pittaDoshaScore = GetScore(e => e.PittaDosha);
            var kaphaDoshaScore = GetScore(e => e.KaphaDosha);

            return new Doshas
            {
                VataDoshaScore = vataDoshaScore,
                PittaDoshaScore = pittaDoshaScore,
                KaphaDoshaScore = kaphaDoshaScore
            };
        }

    }
}
