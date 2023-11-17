using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;
using K9.Globalisation;

namespace K9.DataAccessLayer.Enums
{
    public enum ENineStarKiElement
    {
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Water)]
        [ENineStarKiElementEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Water,
            Element = Water,
            StrongYinOrgans = EOrgan.Kidneys,
            StrongYangOrgans = EOrgan.UrinaryBladder,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Heart },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.SmallIntestine })]
        Water,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Earth)]
        [ENineStarKiElementEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Earth,
            Element = Earth,
            StrongYinOrgans = EOrgan.SpleenPancreas,
            StrongYangOrgans = EOrgan.Stomach,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.GallBladder })]
        Earth,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Tree)]
        [ENineStarKiElementEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Tree,
            Element = Tree,
            StrongYinOrgans = EOrgan.GallBladder,
            StrongYangOrgans = EOrgan.Liver,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Lungs },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.LargeIntestine })]
        Tree,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Metal)]
        [ENineStarKiElementEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Metal,
            Element = Metal,
            StrongYinOrgans = EOrgan.Lungs,
            StrongYangOrgans = EOrgan.LargeIntestine,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.GallBladder })]
        Metal,
        [EnumDescription(ResourceType = typeof(Dictionary), Name = Strings.Names.Fire)]
        [ENineStarKiElementEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Fire,
            Element = Fire,
            StrongYinOrgans = EOrgan.Heart,
            StrongYangOrgans = EOrgan.SmallIntestine,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Lungs },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.LargeIntestine })]
        Fire
    }
}
