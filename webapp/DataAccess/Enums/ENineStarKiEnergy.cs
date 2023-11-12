using K9.DataAccessLayer.Attributes;
using K9.Globalisation;

namespace K9.DataAccessLayer.Enums
{
    public enum ENineStarKiEnergy
    {
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary), Name = Strings.Names.Unspecified)]
        Unspecified,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Water,
            Element = ENineStarKiElement.Water,
            StrongYinOrgans = EOrgan.Kidneys,
            StrongYangOrgans = EOrgan.UrinaryBladder,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Heart },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.SmallIntestine })]
        Water,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Soil,
            Element = ENineStarKiElement.Earth,
            StrongYinOrgans = EOrgan.SpleenPancreas,
            StrongYangOrgans = EOrgan.Stomach,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.GallBladder })]
        Soil,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Thunder,
            Element = ENineStarKiElement.Tree,
            StrongYinOrgans = EOrgan.GallBladder,
            StrongYangOrgans = EOrgan.Liver,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Lungs },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.LargeIntestine })]
        Thunder,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Wind,
            Element = ENineStarKiElement.Tree,
            StrongYinOrgans = EOrgan.GallBladder,
            StrongYangOrgans = EOrgan.Liver,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Lungs },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.LargeIntestine })]
        Wind,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.CoreEarth,
            Element = ENineStarKiElement.Earth,
            StrongYinOrgans = EOrgan.SpleenPancreas,
            StrongYangOrgans = EOrgan.Stomach,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.GallBladder })]
        CoreEarth,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Heaven,
            Element = ENineStarKiElement.Metal,
            StrongYinOrgans = EOrgan.Lungs,
            StrongYangOrgans = EOrgan.LargeIntestine,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.GallBladder })]
        Heaven,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Lake,
            Element = ENineStarKiElement.Metal,
            StrongYinOrgans = EOrgan.Lungs,
            StrongYangOrgans = EOrgan.LargeIntestine,
            WeakYinOrgans = new EOrgan[] { EOrgan.SpleenPancreas, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.Stomach, EOrgan.GallBladder })]
        Lake,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Mountain,
            Element = ENineStarKiElement.Earth,
            StrongYinOrgans = EOrgan.SpleenPancreas,
            StrongYangOrgans = EOrgan.Stomach,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Liver },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.GallBladder })]
        Mountain,
        [NineStarKiEnumMetaData(ResourceType = typeof(Dictionary),
            Name = Strings.Names.Fire,
            Element = ENineStarKiElement.Fire,
            StrongYinOrgans = EOrgan.Heart,
            StrongYangOrgans = EOrgan.SmallIntestine,
            WeakYinOrgans = new EOrgan[] { EOrgan.Kidneys, EOrgan.Lungs },
            WeakYangOrgans = new EOrgan[] { EOrgan.UrinaryBladder, EOrgan.LargeIntestine })]
        Fire
    }
}
