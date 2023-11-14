using K9.Base.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum ECompatibilityLevel
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Good)]
        Good,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Excellent)]
        Excellent,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Minimise)]
        Minimise,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Avoid)]
        Avoid
    }
}
