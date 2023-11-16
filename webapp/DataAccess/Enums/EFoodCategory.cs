using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum EFoodCategory
    {
        [Score(
            Carnivore = true,
            Vegan = false,
            Vegetarian = false,
            Fruitarian = false,
            Pescatarian = false)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.RedMeat)]
        RedMeat,

        [Score(
            Carnivore = true,
            Vegan = false,
            Vegetarian = false,
            Fruitarian = false,
            Pescatarian = false)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Poultry)]
        Poultry,

        [Score(
            Carnivore = true,
            Vegan = false,
            Vegetarian = false,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.FishAndSeafood)]
        FishAndSeafood,

        [Score(
            Carnivore = true,
            Vegan = false,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.EggsAndRoes)]
        EggsAndRoes,

        [Score(
            Carnivore = true,
            Vegan = false,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = false)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Dairy)]
        Dairy,

        [Score(
            Carnivore = false,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = true,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.VegetableProtein)]
        VegetableProtein,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.FatsAndOils)]
        FatsAndOils,

        [Score(
            Carnivore = false,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Carbohydrates)]
        Carbohydrates,

        [Score(
            Carnivore = false,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = true,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Vegetables)]
        Vegetables,

        [Score(
            Carnivore = false,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Fungi)]
        Fungi,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = true,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Fruits)]
        Fruits,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = true,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.HerbsAndSpices)]
        HerbsAndSpices,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = true,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Beverages)]
        Beverages,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Condiments)]
        Condiments,

        [Score(
            Carnivore = true,
            Vegan = true,
            Vegetarian = true,
            Fruitarian = false,
            Pescatarian = true)]
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.FermentedFoods)]
        FermentedFoods
    }
}