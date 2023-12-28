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
        public Protocol Protocol { get; set; }

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
                    foreach (var recommendedFood in RecommendedFoods)
                    {
                        recommendedFood.HunterCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.HunterCompatibilityLevel);
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
                        recommendedFood.GathererCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.GathererCompatibilityLevel);
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
                        recommendedFood.TeacherCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.TeacherCompatibilityLevel);
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
                        recommendedFood.ExplorerCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.ExplorerCompatibilityLevel);
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
                        recommendedFood.WarriorCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.WarriorCompatibilityLevel);
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
                        recommendedFood.NomadCompatibilityLevel = GetCompatibilityLevel(recommendedFood, recommendedFood.NomadCompatibilityLevel);
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

        private static ECompatibilityLevel GetCompatibilityLevel(FoodItem recommendedFood, ECompatibilityLevel compatibilityLevel)
        {
            var score = recommendedFood.GetScore();

            // Upgrade highest-scoring items
            if (score == EScore.VeryHigh
                && compatibilityLevel != ECompatibilityLevel.Unsuitable
                && compatibilityLevel != ECompatibilityLevel.Suboptimal)
            {
                return ECompatibilityLevel.Optimal;
            }

            else if (score == EScore.High
                     && compatibilityLevel == ECompatibilityLevel.Neutral)
            {
                return ECompatibilityLevel.Excellent;
            }

            else if (score >= EScore.High && (compatibilityLevel == ECompatibilityLevel.Suboptimal ||
                                              compatibilityLevel == ECompatibilityLevel.Unsuitable))

            {
                recommendedFood.RelativeScore = 0;
            }

            // Downgrade lowest scoring items
            else if (score == EScore.VeryLow)
            {
                if (compatibilityLevel == ECompatibilityLevel.Optimal)
                {
                    return ECompatibilityLevel.Neutral;
                }

                else if (compatibilityLevel == ECompatibilityLevel.Excellent)
                {
                    return ECompatibilityLevel.Suboptimal;
                }

                else if (compatibilityLevel == ECompatibilityLevel.Neutral
                         || compatibilityLevel == ECompatibilityLevel.Suboptimal)
                {
                    return ECompatibilityLevel.Unsuitable;
                }
            }

            else if (score == EScore.Low)
            {
                if (compatibilityLevel == ECompatibilityLevel.Optimal)
                {
                    return ECompatibilityLevel.Excellent;
                }

                else if (compatibilityLevel == ECompatibilityLevel.Excellent)
                {
                    return ECompatibilityLevel.Neutral;
                }

                else if (compatibilityLevel == ECompatibilityLevel.Neutral)
                {
                    return ECompatibilityLevel.Suboptimal;
                }
            }

            return compatibilityLevel;
        }
    }
}