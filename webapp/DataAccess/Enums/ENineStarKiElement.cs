using K9.Base.DataAccessLayer.Attributes;
using K9.Globalisation;

namespace K9.DataAccessLayer.Enums
{
    public enum ENineStarKiElement
    {
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Water)]
        Water,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Earth)]
        Earth,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Tree)]
        Tree,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Metal)]
        Metal,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Fire)]
        Fire
    }
}
