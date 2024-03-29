﻿using K9.DataAccessLayer.Helpers;
using K9.DataAccessLayer.Interfaces;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Activity = K9.DataAccessLayer.Models.Activity;

namespace K9.WebApplication.Services
{
    public abstract class CacheableServiceBase<T> : ICacheableService, ICategorisableService
        where T : class, IObjectBase
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPacksRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IRepository<Protocol> _protocolsRepository;
        private readonly IRepository<IngredientSubstitute> _ingredientSubstitutesRepository;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<ProductIngredientSubstitute> _productIngredientSubstitutesRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly IRepository<DietaryRecommendation> _dietaryRecommendationsRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductsRepository;
        private readonly IRepository<FoodItem> _foodItemsRepository;

        public CacheableServiceBase(IRepository<Product> productsRepository, IRepository<ProductPack> productPacksRepository, IRepository<Ingredient> ingredientsRepository, IRepository<Protocol> protocolsRepository, IRepository<IngredientSubstitute> ingredientSubstitutesRepository, IRepository<ProductIngredient> productIngredientsRepository, IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository, IRepository<Activity> activitiesRepository, IRepository<DietaryRecommendation> dietaryRecommendationsRepository, IRepository<ProductPackProduct> productPackProductsRepository, IRepository<FoodItem> foodItemsRepository)
        {
            _productsRepository = productsRepository;
            _productPacksRepository = productPacksRepository;
            _ingredientsRepository = ingredientsRepository;
            _protocolsRepository = protocolsRepository;
            _ingredientSubstitutesRepository = ingredientSubstitutesRepository;
            _productIngredientsRepository = productIngredientsRepository;
            _productIngredientSubstitutesRepository = productIngredientSubstitutesRepository;
            _activitiesRepository = activitiesRepository;
            _dietaryRecommendationsRepository = dietaryRecommendationsRepository;
            _productPackProductsRepository = productPackProductsRepository;
            _foodItemsRepository = foodItemsRepository;
        }

        public MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration)
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(duration));
        }

        public List<Activity> GetActivities()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _activitiesRepository.List().ToList();
            });
        }

        public List<DietaryRecommendation> GetDietaryRecommendations()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _dietaryRecommendationsRepository.List();
            });
        }

        public List<FoodItem> GetFoodItems()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _foodItemsRepository.List();
            });
        }

        public List<Product> GetProducts()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productsRepository.List();
            });
        }

        public List<ProductPack> GetProductPacks()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productPacksRepository.List();
            });
        }

        public List<ProductPackProduct> GetProductPackProducts()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productPackProductsRepository.List();
            });
        }

        public List<Ingredient> GetIngredients()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _ingredientsRepository.List();
            });
        }

        public List<IngredientSubstitute> GetIngredientSubstitutes()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _ingredientSubstitutesRepository.List();
            });
        }

        public List<ProductIngredient> GetProductIngredients()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productIngredientsRepository.List();
            });
        }

        public List<ProductIngredientSubstitute> GetProductIngredientSubstitutes()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productIngredientSubstitutesRepository.List();
            });
        }

        public List<Protocol> GetProtocols()
        {
            return MemoryCacheHelper.Cache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _protocolsRepository.List();
            });
        }

        public void ClearCache()
        {
            MemoryCacheHelper.ClearCache();
        }

        public string GetCacheKey(int id)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T).Name}-{id}";
        }

        public string GetCacheKey()
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T).Name}";
        }

        public string GetCacheKey(params object[] parameters)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T).Name}-{parameters.ToDelimitedList()}";
        }

        public string GetCacheKey<T2>(params object[] parameters) where T2 : class, IObjectBase
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T2).Name}-{parameters.ToDelimitedList()}";
        }

        public string GetCacheKey<T2>(int id) where T2 : class, IObjectBase
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T2).Name}-{id}";
        }

        public int GetItemCode(ICategorisable model, List<ICategorisable> items)
        {
            model.ItemCode = 0;

            var itemsInCategory = items.Where(e => e.Category == model.Category).ToList();
            var newItemCode = (int)model.Category;
            var maxItemCode = (int)model.Category + Constants.Constants.CategoryGap;

            while (newItemCode <= maxItemCode)
            {
                if (itemsInCategory.Any(e => e.ItemCode == newItemCode))
                {
                    // ItemCode already taken, decrease it
                    newItemCode++;
                }
                else
                {
                    return newItemCode;
                }
            }

            throw new Exception(Globalisation.Dictionary.NewItemCodeNotAvailable);
        }
    }
}