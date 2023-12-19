using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.ViewModels
{
    public class RecommendedFoodsViewModel
    {
        public int? ClientId { get; set; }
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
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.HunterCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.HunterCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.HunterCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.HunterCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.HunterCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.HunterCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.HunterCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.HunterCompatibilityLevel == level)).ToList();

                case EGenoType.Gatherer:
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.GathererCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.GathererCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.GathererCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.GathererCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.GathererCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.GathererCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.GathererCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.GathererCompatibilityLevel == level)).ToList();

                case EGenoType.Teacher:
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.TeacherCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.TeacherCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.TeacherCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.TeacherCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.TeacherCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.TeacherCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.TeacherCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.TeacherCompatibilityLevel == level)).ToList();

                case EGenoType.Explorer:
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.ExplorerCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.ExplorerCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.ExplorerCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.ExplorerCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.ExplorerCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.ExplorerCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.ExplorerCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.ExplorerCompatibilityLevel == level)).ToList();

                case EGenoType.Warrior:
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.WarriorCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.WarriorCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.WarriorCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.WarriorCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.WarriorCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.WarriorCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.WarriorCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.WarriorCompatibilityLevel == level)).ToList();

                case EGenoType.Nomad:
                    // Upgrade highest-scoring items
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        if (recommendedFood.RelativeScore >= 90 && recommendedFood.NomadCompatibilityLevel <= ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.NomadCompatibilityLevel = ECompatibilityLevel.Optimal;
                        }
                        else if (recommendedFood.RelativeScore >= 80 && recommendedFood.NomadCompatibilityLevel == ECompatibilityLevel.Neutral)
                        {
                            recommendedFood.NomadCompatibilityLevel = ECompatibilityLevel.Excellent;
                        }
                        else if (recommendedFood.RelativeScore >= 70 && (recommendedFood.NomadCompatibilityLevel == ECompatibilityLevel.Suboptimal || recommendedFood.NomadCompatibilityLevel == ECompatibilityLevel.Unsuitable))

                        {
                            recommendedFood.RelativeScore = 0;
                        }
                    }

                    if (foodGroup == EFoodGroup.Other)
                    {
                        return RecommendedFoods?.Where(e =>
                            e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                            (e.NomadCompatibilityLevel == level && level != ECompatibilityLevel.Neutral)).ToList();
                    }

                    return RecommendedFoods?.Where(e =>
                        e.Category.GetAttribute<EFoodGroupMetaDataAttribute>().FoodGroup == foodGroup &&
                        (e.NomadCompatibilityLevel == level)).ToList();
            }

            return null;
        }
    }
}