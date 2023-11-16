using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using NLog;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IRepository<FoodItem> _foodItemsRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly ILogger _logger;

        public MaintenanceService(IRepository<FoodItem> foodItemsRepository, IRepository<Activity> activitiesRepository, ILogger logger)
        {
            _foodItemsRepository = foodItemsRepository;
            _activitiesRepository = activitiesRepository;
            _logger = logger;
        }

        public void AddFoodItems()
        {
            AddHunterFoodItems();
            AddGathererFoods();
        }

        private void AddHunterFoodItems()
        {
            CreateFoodItem("Beef", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Beef Bone Broth", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Beef Liver", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Beef Tongue", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Buffalo / Bison", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Goat", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Lamb", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Mutton", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);
            CreateFoodItem("Venison", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.RedMeat, EFoodFrequency.ThreeToFiveTimesWeekly);

            CreateFoodItem("Bacon", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);
            CreateFoodItem("Beef Heart", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.RedMeat, null);
            CreateFoodItem("Boar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);
            CreateFoodItem("Ham", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);
            CreateFoodItem("Kangaroo", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);
            CreateFoodItem("Opossum", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);
            CreateFoodItem("Pork", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.RedMeat, null);

            CreateFoodItem("Chicken", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Cornish Hen", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Duck", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Grouse", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Pheasant", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Squab", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);
            CreateFoodItem("Turkey", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Poultry, EFoodFrequency.TwoToFourTimesWeekly);

            CreateFoodItem("Chiken Liver", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Poultry, null);
            CreateFoodItem("Duck Liver", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Poultry, null);
            CreateFoodItem("Goose Liver", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Poultry, null);
            CreateFoodItem("Quail", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Poultry, null);

            CreateFoodItem("Striped Bass", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Chub", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Haddock", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Hake", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Herring", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Atlantic Mackerel", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Ocean Pout", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Pacific Flounder", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Pacific Sole", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Pilchard", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Pompano", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Atlanti Salmon", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Chinook Salmon", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Sockeye Salmon", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Sardine", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Scrod", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Smelt", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Sturgeon", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Rainbow Trout", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Seatrout", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);
            CreateFoodItem("Wild Trout", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FishAndSeafood, EFoodFrequency.AtLeastFourTimesWeekly);

            CreateFoodItem("Abalone", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Barracuda", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Blue Gill Bass", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Sea Bass", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Bluefish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Catfish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Conch", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Crab", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Grouper", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Frog", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Muskellunge", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Octopus", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Atlantic Pollock", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Farmed Salmon", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Squid / Calamari", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Swordfish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Tilefish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Toothfish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);
            CreateFoodItem("Turtle", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FishAndSeafood, null);

            CreateFoodItem("Chicken Egg", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.EggsAndRoes, EFoodFrequency.SevenToNineTimesWeekly);
            CreateFoodItem("Duck Egg", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.EggsAndRoes, EFoodFrequency.SevenToNineTimesWeekly);
            CreateFoodItem("Carp Roe", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.EggsAndRoes, EFoodFrequency.SevenToNineTimesWeekly);
            CreateFoodItem("Salfish Roe", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.EggsAndRoes, EFoodFrequency.SevenToNineTimesWeekly);
            CreateFoodItem("Salmon Roe", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.EggsAndRoes, EFoodFrequency.SevenToNineTimesWeekly);

            CreateFoodItem("Caviar", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.EggsAndRoes, null);
            CreateFoodItem("Herring Roe", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.EggsAndRoes, null);
            CreateFoodItem("Goose Egg", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.EggsAndRoes, null);
            CreateFoodItem("Quail Egg", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.EggsAndRoes, null);

            CreateFoodItem("Butter", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.AsNeeded);
            CreateFoodItem("Ghee", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Dairy, EFoodFrequency.AsNeeded);
            CreateFoodItem("Kefalotyri Cheese", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.ThreeTimesWeekly);
            CreateFoodItem("Manchego Cheese", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.ThreeTimesWeekly);
            CreateFoodItem("Parmesan Cheese", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.ThreeTimesWeekly);
            CreateFoodItem("Pecorino Cheese", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.ThreeTimesWeekly);
            CreateFoodItem("Romano Cheese", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Dairy, EFoodFrequency.ThreeTimesWeekly);

            CreateFoodItem("American Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Blue Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Brie Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Camembert Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Casein", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Cheddar Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Cheshire Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Colby Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Cottage Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Cream Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Edam Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Emmenthal Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Farmer Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Feta Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Gorgonzola Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Gouda Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Gruyere Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Half-And-Half", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Havarti Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Jarlsberg Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Limburger Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Buttermilk", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Skimmed Milk", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Cow'S Milk", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Goat'S Milk", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Monterey Jack Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Mozzarella Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Muenster Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Neufchatel Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Paneer Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Port Du Salut Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Provolone Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Quark Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Ricotta Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Romanian Urda Cheese", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Dairy, null);
            CreateFoodItem("Roquefort Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Sour Cream", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Stilton Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("String Cheese", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Whey Protein", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);
            CreateFoodItem("Yogurt", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Dairy, null);

            CreateFoodItem("Adzuki Bean", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Almond", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Almond Butter", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Black Bean", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Black-Eyed Pea", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Broad Bean", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Fava", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Butternut", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Carob", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Chinese Chestnut", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Chia Seed", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Flaxseed", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Chickpea", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Great Notthern Bean", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Green Bean", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Haricot Bean", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Hemp", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Lima Bean", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Lima Bean Flour", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Macadamia", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Pine Nut", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Pumpkin Seed", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Safflower Seed", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Snap Bean", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Sesame Seed", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Tahini", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Sesame Flour", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Walnut", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Watermelon Seeds", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);
            CreateFoodItem("Baker'S Yeast", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.VegetableProtein, EFoodFrequency.ThreeToSevenTimesWeekly);

            CreateFoodItem("Beechnut", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Brazil Nut", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Cashew", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Cashew Butter", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("European Chestnut", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Copper Bean", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Hazelnut", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Hickory Nuts", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Kidney Bean", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Lentils", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Sprouted Lentils", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Litchi", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Lotus Seeds", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Lupin Seeds", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Mung Bean", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Natto", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Navy Bean", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Peanut Butter", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Peanut Flour", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Peanuts", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Pinto Bean", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Sprouted Pinto Bean", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Pistachio", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Poppy Seed", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Soy", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Tempeh", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Tofu", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Sunflower Seed", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Tamarind", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);
            CreateFoodItem("Yard-Long Bean", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.VegetableProtein, null);

            CreateFoodItem("Butter", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Ghee", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Camelina Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Chia Seed Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Cod Liver Oil", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Flaxseed Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Hemp Seed Oil", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Herring Oil", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Olive Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Perilla Seed Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Pumpkin Seed Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Quinoa Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Rice Bran Oil", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Salmon Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Sesame Oil", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);
            CreateFoodItem("Walnut Oil", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.FatsAndOils, EFoodFrequency.ThreeToNineTimesWeekly);

            CreateFoodItem("Avocado Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Canola Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Coconut Oil", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Corn Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Cottonseed Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Grape Seed Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Hazelnut Oil", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Lard", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Margarine", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Oat Oil", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Palm Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Peanut Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Safflower Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Shea Nut Oil", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Soybean Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Sunflower Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);
            CreateFoodItem("Wheat Germ Oil", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.FatsAndOils, null);

            CreateFoodItem("100% Artichoke Flour", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Buckwheat", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Flaxseed Bread", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Fonio", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Lob'S Tears", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Millet", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Poi", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Quinoa", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Rice Bran", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Brown Rice Flour", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Basmatic Rice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Brown Rice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Wild Rice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);
            CreateFoodItem("Teff", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Carbohydrates, EFoodFrequency.TwoToFiveTimesWeekly);

            CreateFoodItem("Amaranth", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Barley", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Cornmeal / Hominy / Polenta", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Essene / Manna Breads", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Kudzu", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Lentil Flour / Dhal", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Oats", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Rye", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Soybean Flour", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Sprouted Wheat", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Wheat Bran / Germ", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Bulgur Wheat", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Durum Wheat / Semolina", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Emmer Wheat", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Wheat Gluten", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Kamut Wheat", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Spelt", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Wheat Flour", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);
            CreateFoodItem("Whole Grain Wheat", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Carbohydrates, null);

            CreateFoodItem("Artichoke", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Asparagus", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Broccoflower", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Broccoli", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Chinese Broccoli", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Broccoli Rabe", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Chicory", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Chinese Kale", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Dandelion", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Escarole", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Fenugreek", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Ginger", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Grape Leaves", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Jerusalem Artichoke", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Jew'S Eart", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Kale", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Enoki Mushroom", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fungi, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Mustard Greens", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Okra", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Onion", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Parsnip", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Chilli Pepper", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Pumpkin", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Rowal", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);

            CreateFoodItem("Rutabaga", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Kelp", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Spirulina", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Wakame", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Sweet Potato", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Sweet Potato Leaves", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Swiss Chard", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Turnip", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Turnip Greens", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Vegetables, EFoodFrequency.OneToFiveTImesDaily);

            CreateFoodItem("Alfalfa Sprout", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Aloe Vera", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Avocado", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Asparagus Pea", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Bamboo Shoot", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Beet Green", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Borage", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Brussels Sprout", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Carrot", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Cassava", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Collard Greens", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Corn", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Cucumber", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Eggplant", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Kanpyo", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Leek", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Lettuce", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Brown Mushroom", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fungi, null);
            CreateFoodItem("Oyster Mushroom", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fungi, null);
            CreateFoodItem("Portobello Mushroom", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fungi, null);
            CreateFoodItem("Shitake Mushroom", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fungi, null);
            CreateFoodItem("Straw Mushroom", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fungi, null);
            CreateFoodItem("White Mushroom", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fungi, null);
            CreateFoodItem("Black Olive", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Green Olive", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Pickle Brine", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Pickle Vinegar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Pimento", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("White Potato", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Purslane", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Quorn", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Rhubarb", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Sauerkraut", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Agar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Irish Moss", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);
            CreateFoodItem("Spinach", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Tomatillo", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Tomato", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Vegetables, null);
            CreateFoodItem("Water Chestnut", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Vegetables, null);

            CreateFoodItem("Acai Berry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Banana", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Blueberry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Canistel", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Cranberry", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Crenshaw Melon", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Date", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Dewberry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Elderberry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Goji", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Gooseberry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Groundcherry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Grapefruit", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Guava", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Huckleberry", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Jackfruit", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Lemon", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Lime", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Lingonberry", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Mamey Sapote", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Mango", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Passion Fruit", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Paw Paw", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Peach", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Pear", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Pineapple", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Quince", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Sago Palm", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);
            CreateFoodItem("Watermelon", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Fruits, EFoodFrequency.OneToFiveTImesDaily);

            CreateFoodItem("Nectarine", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fruits, null);
            CreateFoodItem("Orange", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);
            CreateFoodItem("Papaya", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);
            CreateFoodItem("Plantain", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);
            CreateFoodItem("Plum", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fruits, null);
            CreateFoodItem("Pomegranate", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fruits, null);
            CreateFoodItem("Prune", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fruits, null);
            CreateFoodItem("Raisin", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Fruits, null);
            CreateFoodItem("Tamarillo", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);
            CreateFoodItem("Tangerine", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);
            CreateFoodItem("Strawberry", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Fruits, null);

            CreateFoodItem("Anise", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Chocolate", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Cilantro", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Cinnamon", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Clove", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Curry", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Dulse", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Garlic", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Rosemary", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Saffron", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Sage", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Tarragon", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);
            CreateFoodItem("Turmeric", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.HerbsAndSpices, EFoodFrequency.OneToTwoTimesDaily);

            CreateFoodItem("Arabic Acacia", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Caper", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Caramel", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Chives", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Cornstarch", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Guarana", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Mace", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Nutmeg", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Parsley", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);
            CreateFoodItem("Black Pepper", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.HerbsAndSpices, null);

            CreateFoodItem("Cranberry Juice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Grape Juice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Grapefruit Juice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Pear Juice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Pineapple Juice", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Chamomile Tea", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Ginger Tea", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Green Tea", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Lemon Balm Tea", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Rooibos Tea", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);
            CreateFoodItem("Yerba Maté Tea", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Beverages, EFoodFrequency.TwoToFourTimesDaily);

            CreateFoodItem("Apple Juice", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Beer", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Beet Juice", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Blackberry Juice", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Carrot Juice", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Coffee", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Cola Beverages", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Cucumber Juice", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Liquor", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Almond Milk", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Coconut Milk", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Rice Milk", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("Soy Milk", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Orange Juice", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Tangerine Juice", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Black Tea", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Kombucha Tea", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Beverages, null);
            CreateFoodItem("Red Wine", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);
            CreateFoodItem("White Wine", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Beverages, null);

            CreateFoodItem("Agave Syrup", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Arrow Root", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Locust Bean Gum", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Molasses", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Blackstrap Molasses", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Mustard Powder", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Umeboshi Plum", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Vegetable Glycerine", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Nutritional Yeast", EGenoType.Hunter, ECompatibilityLevel.Excellent, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);
            CreateFoodItem("Yeast Extract", EGenoType.Hunter, ECompatibilityLevel.Good, EFoodCategory.Condiments, EFoodFrequency.AsNeeded);

            CreateFoodItem("Aspartame", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Bha /Bht", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Dextrose", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Fructose", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("High-Fructose Corn Syrup", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Ketchup", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Konjac", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Guar Gum", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Mastic Gum", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Condiments, null);
            CreateFoodItem("Msg", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Maple Syrup", EGenoType.Hunter, ECompatibilityLevel.Minimise, EFoodCategory.Condiments, null);
            CreateFoodItem("Mayonnaise", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Tofu Mayonnaise", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Miso", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Mustard With Vinegar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Phytic Acid", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Pickle Relish", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Potassium Bisulfite", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Potassium Metabisulfite", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Sodium Bisulfite", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Sodium Metabisulfite", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Soy / Tamari Sauce", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Brown Sugar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Vinegar (All Types)", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("Worcestershire Sauce", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);
            CreateFoodItem("White Sugar", EGenoType.Hunter, ECompatibilityLevel.Avoid, EFoodCategory.Condiments, null);

            CreateActivity("Asana", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Vigorous Walking", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Moderate Competitive Sports", EGenoType.Hunter, ECompatibilityLevel.Excellent, "Sports such as tennis, badminton, volleyball, etc.");
            CreateActivity("Resistance Training", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Intense Competitive Sports", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Dancing", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Gymnastics", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Running", EGenoType.Hunter, ECompatibilityLevel.Excellent);
            CreateActivity("Meditation", EGenoType.Hunter, ECompatibilityLevel.Excellent);
        }

        private void AddGathererFoods()
        {

        }

        private void CreateFoodItem(string name, EGenoType genoType, ECompatibilityLevel level, EFoodCategory category, EFoodFrequency? frequency = null)
        {
            FoodItem foodItem = _foodItemsRepository.Find(e => e.Name == name || (e.Name == $"{name}s") || ($"{e.Name}s" == name)).FirstOrDefault();

            if (foodItem == null)
            {
                foodItem = new FoodItem();
            }

            if (genoType == EGenoType.Hunter)
            {
                foodItem.Hunter = true;
                foodItem.HunterCompatibilityLevel = level;
                foodItem.HunterFrequency = frequency;
            }
            if (genoType == EGenoType.Gatherer)
            {
                foodItem.Gatherer = true;
                foodItem.GathererCompatibilityLevel = level;
                foodItem.GathererFrequency = frequency;
            }
            if (genoType == EGenoType.Teacher)
            {
                foodItem.Teacher = true;
                foodItem.TeacherCompatibilityLevel = level;
                foodItem.TeacherFrequency = frequency;
            }
            if (genoType == EGenoType.Explorer)
            {
                foodItem.Explorer = true;
                foodItem.ExplorerCompatibilityLevel = level;
                foodItem.ExplorerFrequency = frequency;
            }
            if (genoType == EGenoType.Warrior)
            {
                foodItem.Warrior = true;
                foodItem.WarriorCompatibilityLevel = level;
                foodItem.WarriorFrequency = frequency;
            }
            if (genoType == EGenoType.Nomad)
            {
                foodItem.Nomad = true;
                foodItem.NomadCompatibilityLevel = level;
                foodItem.NomadFrequency = frequency;
            }

            if (foodItem.Id == 0)
            {
                foodItem.Name = name;
                foodItem.Vegetarian = category.GetAttribute<ScoreAttribute>().Vegetarian;
                foodItem.Vegan = category.GetAttribute<ScoreAttribute>().Vegan;
                foodItem.Fruitarian = category.GetAttribute<ScoreAttribute>().Fruitarian;
                foodItem.Carnivore = category.GetAttribute<ScoreAttribute>().Carnivore;
                foodItem.Pescatarian = category.GetAttribute<ScoreAttribute>().Pescatarian;
                foodItem.Category = category;

                _foodItemsRepository.Update(foodItem);
            }
            else
            {
                _foodItemsRepository.Create(foodItem);
            }
        }

        private void CreateActivity(string name, EGenoType genoType, ECompatibilityLevel level, string description = "")
        {
            var activity = _activitiesRepository.Find(e => e.Name == name || (e.Name == $"{name}s") || ($"{e.Name}s" == name)).FirstOrDefault();
            
            if (activity == null)
            {
                activity = new Activity();
            }

            activity.ShortDescription = description;

            if (genoType == EGenoType.Hunter)
            {
                activity.Hunter = true;
                activity.HunterCompatibilityLevel = level;
            }
            if (genoType == EGenoType.Gatherer)
            {
                activity.Gatherer = true;
                activity.GathererCompatibilityLevel = level;
            }
            if (genoType == EGenoType.Teacher)
            {
                activity.Teacher = true;
                activity.TeacherCompatibilityLevel = level;
            }
            if (genoType == EGenoType.Explorer)
            {
                activity.Explorer = true;
                activity.ExplorerCompatibilityLevel = level;
            }
            if (genoType == EGenoType.Warrior)
            {
                activity.Warrior = true;
                activity.WarriorCompatibilityLevel = level;
            }
            if (genoType == EGenoType.Nomad)
            {
                activity.Nomad = true;
                activity.NomadCompatibilityLevel = level;
            }

            if (activity.Id == 0)
            {
                _activitiesRepository.Update(activity);
            }
            else
            {
                _activitiesRepository.Create(activity);
            }
        }
    }
}