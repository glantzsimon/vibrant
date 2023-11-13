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
        [UIHint("SkinType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SkinTypeLabel)]
        public ESkinType? SkinType { get; set; }

        [UIHint("SleepQuality")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.SleepLabel)]
        public ESleepQuality? SleepQuality { get; set; }

        [UIHint("WeightGain")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.WeightGainQuestionLabel)]
        public EWeightGain? WeightGain { get; set; }

        [UIHint("BodyTemperature")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.BodyTemperatureQuestionLabel)]
        public EBodyTemperature? BodyTemperature { get; set; }

        [UIHint("StressResponse")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.StressResponseQuestionLabel)]
        public EStressResponse? StressResponse { get; set; }

        [UIHint("EyesType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.EyesTypeQuestionLabel)]
        public EEyesType? EyesType { get; set; }

        [UIHint("Disposition")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.DispositionQuestionLabel)]
        public EDisposition? Disposition { get; set; }

        [UIHint("HairType")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.HairTypeQuestionLabel)]
        public EHairType? HairType { get; set; }

        public bool IsDoshasComplete()
        {
            return SkinType.HasValue &&
                    SleepQuality.HasValue &&
                    WeightGain.HasValue &&
                    BodyTemperature.HasValue &&
                    EyesType.HasValue &&
                    Disposition.HasValue &&
                    HairType.HasValue &&
                    StressResponse.HasValue;
        }

        public Doshas GetDoshas()
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

    }
}
