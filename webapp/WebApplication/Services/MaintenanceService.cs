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
        private readonly ILogger _logger;

        public MaintenanceService(IRepository<FoodItem> foodItemsRepository, ILogger logger)
        {
            _foodItemsRepository = foodItemsRepository;
            _logger = logger;
        }

        public void AddFoodItems()
        {

            AddHunterFoodItems();

        }

        private void AddHunterFoodItems()
        {
            #region Red Meats



            #endregion


        }

        private void CreateFoodItem(string name, EGenoType genoType, ECompatibilityLevel level, EFoodCategory category, int minTimesPerWeek, int maxTimesPerWeek)
        {
            FoodItem foodItem = _foodItemsRepository.Find(e => e.Name == name).FirstOrDefault();

            if (foodItem == null)
            {
                foodItem = new FoodItem();
            }

            if (genoType == EGenoType.Hunter)
            {
                foodItem.Hunter = true;
                foodItem.HunterCompatibilityLevel = level;
                foodItem.HunterMinTimesPerWeek = minTimesPerWeek;
                foodItem.HunterMaxTimesPerWeek = maxTimesPerWeek;
            }
            if (genoType == EGenoType.Gatherer)
            {
                foodItem.Gatherer = true;
                foodItem.GathererCompatibilityLevel = level;
                foodItem.GathererMinTimesPerWeek = minTimesPerWeek;
                foodItem.GathererMaxTimesPerWeek = maxTimesPerWeek;
            }
            if (genoType == EGenoType.Teacher)
            {
                foodItem.Teacher = true;
                foodItem.TeacherCompatibilityLevel = level;
                foodItem.TeacherMinTimesPerWeek = minTimesPerWeek;
                foodItem.TeacherMaxTimesPerWeek = maxTimesPerWeek;
            }
            if (genoType == EGenoType.Explorer)
            {
                foodItem.Explorer = true;
                foodItem.ExplorerCompatibilityLevel = level;
                foodItem.ExplorerMinTimesPerWeek = minTimesPerWeek;
                foodItem.ExplorerMaxTimesPerWeek = maxTimesPerWeek;
            }
            if (genoType == EGenoType.Warrior)
            {
                foodItem.Warrior = true;
                foodItem.WarriorCompatibilityLevel = level;
                foodItem.WarriorMinTimesPerWeek = minTimesPerWeek;
                foodItem.WarriorMaxTimesPerWeek = maxTimesPerWeek;
            }
            if (genoType == EGenoType.Nomad)
            {
                foodItem.Nomad = true;
                foodItem.NomadCompatibilityLevel = level;
                foodItem.NomadMinTimesPerWeek = minTimesPerWeek;
                foodItem.NomadMaxTimesPerWeek = maxTimesPerWeek;
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
    }
}