using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using K9.DataAccessLayer.Attributes;
using K9.SharedLibrary.Extensions;

namespace K9.WebApplication.ViewModels
{
    public class RecommendedFoodsViewModel
    {
        public List<ECompatibilityLevel> Levels => new List<ECompatibilityLevel>
        {
            ECompatibilityLevel.Optimal,
            ECompatibilityLevel.Excellent,
            ECompatibilityLevel.Neutral,
            ECompatibilityLevel.Suboptimal,
            ECompatibilityLevel.Unsuitable
        };

        public List<EFoodGroup> FoodGroups => new List<EFoodGroup>
        {
            EFoodGroup.Vegetables,
            EFoodGroup.Fruits,
            EFoodGroup.Proteins,
            EFoodGroup.Carbohydrates,
            EFoodGroup.FatsAndOils,
            EFoodGroup.Dairy,
            EFoodGroup.HerbsAndSpices,
            EFoodGroup.Other
        };

        public List<FoodItem> RecommendedFoods { get; set; }

        public List<FoodItem> GetRecommendedFoodsForLevelAndGroup(EFoodGroup foodGroup, ECompatibilityLevel level)
        {
            return RecommendedFoods?.Where(e =>
                                                     e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                                                     (e.ExplorerCompatibilityLevel == level ||
                                                     e.GathererCompatibilityLevel == level ||
                                                     e.HunterCompatibilityLevel == level ||
                                                     e.NomadCompatibilityLevel == level ||
                                                     e.TeacherCompatibilityLevel == level ||
                                                     e.WarriorCompatibilityLevel == level)).ToList();
        }
    }
}