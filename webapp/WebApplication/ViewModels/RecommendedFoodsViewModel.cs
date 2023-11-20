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
        public EGenoType GenoType { get; set; }

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
            switch (GenoType)
            {
                case EGenoType.Hunter:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.HunterCompatibilityLevel == level)).ToList();

                case EGenoType.Gatherer:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.GathererCompatibilityLevel == level)).ToList();

                case EGenoType.Teacher:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.TeacherCompatibilityLevel == level)).ToList();

                case EGenoType.Explorer:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.ExplorerCompatibilityLevel == level)).ToList();

                case EGenoType.Warrior:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.WarriorCompatibilityLevel == level)).ToList();

                case EGenoType.Nomad:
                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.NomadCompatibilityLevel == level)).ToList();
            }

            return null;
        }
    }
}