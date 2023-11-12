using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum EWeightGain
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.WeightGainEasy)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Vata)]
        Easily = 1,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.WeightGainModerate)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Pitta)]
        Moderate,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.WeightGainDifficult)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Kapha)]
        Difficult
    }
}
