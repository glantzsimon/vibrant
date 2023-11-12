using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum EStressResponse
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.StressReclusiveLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Kapha)]
        DepressedReclusive = 1,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.StressIrritableLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Pitta)]
        IrritableAggressive,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.StressAnxiousLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Vata)]
        AnxiousFearful
    }
}
