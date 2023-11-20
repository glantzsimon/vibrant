using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum ECompatibilityLevel
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Neutral)]
        [ECompatibilityEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Neutral, Description = Globalisation.Strings.Names.NeutralDescription)]
        Neutral,
        [ECompatibilityEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Excellent, Description = Globalisation.Strings.Names.Excellent)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Excellent)]
        Excellent,
        [ECompatibilityEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Optimal, Description = Globalisation.Strings.Names.OptimalDescription)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Optimal)]
        Optimal,
        [ECompatibilityEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Suboptimal, Description = Globalisation.Strings.Names.SuboptimalDescription)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Suboptimal)]
        Suboptimal,
        [ECompatibilityEnumMetaData(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Unsuitable, Description = Globalisation.Strings.Names.UnsuitableDescription)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Unsuitable)]
        Unsuitable
    }
}
