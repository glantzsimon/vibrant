using K9.Base.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum ECategory
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Vitamin)]
        Vitamin = 100000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Mineral)]
        Mineral = 200000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Phytonutrient)]
        Phytonutrient = 300000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Herb)]
        Herb = 400000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Superfood)]
        Superfood = 500000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.DietarySupplement)]
        DietarySupplement = 600000,
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.PersonalCare)]
        PersonalCare = 700000
    }
}
