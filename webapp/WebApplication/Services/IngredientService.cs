﻿using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Models;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class IngredientService : CacheableServiceBase<Ingredient>, IIngredientService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IRepository<IngredientSubstitute> _ingredientSubstituesRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductsRepository;

        public IngredientService(
            ILogger logger, 
            IRepository<Ingredient> ingredientsRepository, 
            IRepository<IngredientSubstitute> ingredientSubstituesRepository, 
            IRepository<Product> productsRepository, 
            IRepository<ProductPack> productPackRepository, 
            IRepository<Protocol> protocolsRepository, 
            IRepository<IngredientSubstitute> ingredientSubstitutesRepository, 
            IRepository<ProductIngredient> productIngredientsRepository, IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository, 
            IRepository<Activity> activitiesRepository, 
            IRepository<DietaryRecommendation> dietaryRecommendationsRepository, 
            IRepository<FoodItem> foodItemsRepository, 
            IRepository<ProductPackProduct> productPackProductsRepository) 
            : base(productsRepository, productPackRepository, ingredientsRepository, protocolsRepository, ingredientSubstitutesRepository, productIngredientsRepository, productIngredientSubstitutesRepository, activitiesRepository, dietaryRecommendationsRepository, productPackProductsRepository, foodItemsRepository)
        {
            _logger = logger;
            _ingredientsRepository = ingredientsRepository;
            _ingredientSubstituesRepository = ingredientSubstituesRepository;
            _productPackProductsRepository = productPackProductsRepository;
        }

        public Ingredient Find(int id)
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                var ingredient = _ingredientsRepository.Find(id);
                if (ingredient != null)
                {
                    ingredient = GetFullIngredient(ingredient);
                }

                return ingredient;
            });
        }

        public Ingredient FindNext(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id]").FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient FindPrevious(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id] DESC").FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient Find(string seoFriendlyId)
        {
            var ingredient = _ingredientsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient GetFullIngredient(Ingredient ingredient)
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(ingredient.Id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                var ingredientSubstitutes = _ingredientSubstituesRepository.Find(e => e.IngredientId == ingredient.Id)
                    .OrderBy(e => e.Priority).ToList();

                var priority = 1;

                foreach (var ingredientSubstitute in ingredientSubstitutes)
                {
                    ingredientSubstitute.Ingredient = ingredient;
                    ingredientSubstitute.SubstituteIngredient = GetIngredients().FirstOrDefault(e => e.Id == ingredientSubstitute.SubstituteIngredientId);
                }

                ingredient.IngredientSubstitutes = ingredientSubstitutes;
                ingredient.Substitutes = ingredientSubstitutes;

                return ingredient;
            });
        }

        public Ingredient DuplicateIngredient(int id)
        {
            var ingredient = Find(id);
            if (ingredient == null)
            {
                return null;
            }

            var newIngredient = new Ingredient();
            ingredient.MapTo(newIngredient);
            newIngredient.Id = 0;
            var newIngredientName = $"{ingredient.Name} Copy";
            newIngredient.Name = newIngredientName;

            _ingredientsRepository.Create(newIngredient);
            newIngredient = Find(newIngredientName);
            if (newIngredient == null)
            {
                throw new Exception("Error duplicating ingredient");
            }

            return ingredient;
        }

        public List<Ingredient> List(bool retrieveFullIngredient = false, bool usedOnly = false)
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(retrieveFullIngredient, usedOnly), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.TenMinutes));

                var ingredients = _ingredientsRepository.Find(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

                if (usedOnly)
                {
                    var usedIngredientIds = _ingredientsRepository.CustomQuery<SortableItem>(
                        $"SELECT [{nameof(ProductIngredient.IngredientId)}] AS [{nameof(Ingredient.Id)}] FROM [{nameof(ProductIngredient)}] WHERE [{nameof(ProductIngredient.IsDeleted)}] = 0");

                    ingredients = ingredients.Where(e => usedIngredientIds.Select(s => s.Id).Contains(e.Id)).ToList();
                }

                if (retrieveFullIngredient)
                {
                    var fullIngredients = new List<Ingredient>();
                    foreach (var ingredient in ingredients)
                    {
                        fullIngredients.Add(GetFullIngredient(ingredient));
                    }

                    return fullIngredients;
                }

                return ingredients;
            });
        }
        
        public Ingredient FindWithSubstitutesSelectList(int id)
        {
            var model = Find(id);
            var existingSubstitutes = _ingredientSubstituesRepository.Find(e => e.IngredientId == id).ToList();

            var selectListItems = _ingredientsRepository.Find(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();
            foreach (var ingredient in selectListItems)
            {
                ingredient.IsSelected = existingSubstitutes.Exists(e => e.SubstituteIngredientId == ingredient.Id);
            }

            model.SubstitutesSelectList = selectListItems.OrderByDescending(e => e.IsSelected).ThenBy(e => e.Name).ToList();

            return model;
        }

        public void UpdateIngredientPriorities(List<SortableItem> items)
        {
            foreach (var sortableItem in items)
            {
                var substitute = _ingredientSubstituesRepository.Find(sortableItem.Id);
                substitute.Priority = sortableItem.DisplayIndex;
                _ingredientSubstituesRepository.Update(substitute);
                ClearCache();
            }
        }

        public void MarkIngredientAsOutOfStock(int id)
        {
            var ingredient = _ingredientsRepository.Find(id);
            ingredient.QuantityInStock = 0;
            _ingredientsRepository.Update(ingredient);
            ClearCache();
        }

        public void EditIngredientSubstitutes(Ingredient ingredient)
        {
            var existingSubstitutes = _ingredientSubstituesRepository.Find(e => e.IngredientId == ingredient.Id).ToList();
            var newItems = ingredient.SubstitutesSelectList.Where(e => e.IsSelected).ToList();

            foreach (var item in existingSubstitutes)
            {
                _ingredientSubstituesRepository.Delete(item.Id);
            }

            foreach (var item in newItems)
            {
                var newItem = new IngredientSubstitute
                {
                    IngredientId = ingredient.Id,
                    SubstituteIngredientId = item.Id
                };
                _ingredientSubstituesRepository.Create(newItem);
            }
        }

        public void UpdateIngredientCategories()
        {
            var ingredients = _ingredientsRepository.List();
            var ingredientCategories = Constants.Constants.IngredientCategories;

            if (ingredients.Any(e => !ingredientCategories.Contains(e.Category)))
            {
                throw new Exception("Cannot update ingredients. Not all items have a valid ItemCode");
            }

            var vitamins = ingredients.Where(e => e.Category == ECategory.Vitamin).ToList();
            var minerals = ingredients.Where(e => e.Category == ECategory.Mineral).ToList();
            var phytoNutrients = ingredients.Where(e => e.Category == ECategory.Phytonutrient).ToList();
            var herbs = ingredients.Where(e => e.Category == ECategory.Herb).ToList();
            var superfoods = ingredients.Where(e => e.Category == ECategory.Superfood).ToList();
            var others = ingredients.Where(e => e.Category == ECategory.Other).ToList();
            var aminoAcids = ingredients.Where(e => e.Category == ECategory.AminoAcid).ToList();
            var itemCode = 0;

            if (vitamins.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Vitamin + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in vitamins.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (minerals.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Mineral + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in minerals.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (phytoNutrients.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Phytonutrient + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in phytoNutrients.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (herbs.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Herb + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in herbs.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (superfoods.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Superfood + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in superfoods.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (others.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.Other + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in others.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            if (aminoAcids.All(e => e.ItemCode == 0))
            {
                itemCode = (int) ECategory.AminoAcid + Constants.Constants.ItemCodeGap;
                foreach (var ingredient in aminoAcids.OrderBy(e => e.Name).ToList())
                {
                    ingredient.ItemCode = itemCode;
                    _ingredientsRepository.Update(ingredient);
                    itemCode += Constants.Constants.ItemCodeGap;
                }
            }

            ClearCache();
        }

        public List<IngredientItem> ListIngredientItems()
        {
            var ingredients = List(true);
            var ingredientsItems = new List<IngredientItem>();

            foreach (var ingredient in ingredients)
            {
                var ingredientItem = ingredient.MapTo<IngredientItem>();
                ingredientItem.IngredientTypeText = ingredient.GetIngredientTypeText();
                ingredientsItems.Add(ingredientItem);
            }

            return ingredientsItems;
        }
    }
}

