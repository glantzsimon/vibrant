using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum ESleepQuality
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.LightSleep)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Vata)]
        Light,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.ModerateSleep)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Pitta)]
        Moderate,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.DeepSleep)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Kapha)]
        Deep
    }
}
