using K9.Base.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum ESaleType
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Sale)]
        Sale = 1,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Trial)]
        Trial = 2,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.PersonalUse)]
        PersonalUse = 3,
    }
}
