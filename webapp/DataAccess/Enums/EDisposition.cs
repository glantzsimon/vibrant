﻿using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum EDisposition
    {
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Undefined)]
        Undefined,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.LivelyDispositionLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Vata)]
        Lively,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.DrivenDispositionLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Pitta)]
        Driven,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.EvenDispositionLabel)]
        [DoshaEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Dosha = EDosha.Kapha)]
        Even
    }
}