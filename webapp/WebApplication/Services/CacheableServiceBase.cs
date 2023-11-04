using K9.DataAccessLayer.Interfaces;
using K9.SharedLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using K9.DataAccessLayer.Helpers;
using K9.DataAccessLayer.Models;
using K9.WebApplication.Models;
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

        public CacheableServiceBase(IRepository<Product> productsRepository, IRepository<ProductPack> productPacksRepository, IRepository<Ingredient> ingredientsRepository, IRepository<Protocol> protocolsRepository, IRepository<IngredientSubstitute> ingredientSubstitutesRepository, IRepository<ProductIngredient> productIngredientsRepository, IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository, IRepository<Activity> activitiesRepository, IRepository<DietaryRecommendation> dietaryRecommendationsRepository, IRepository<ProductPackProduct> productPackProductsRepository)
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
        }

#if DEBUG
        protected static MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
#else
        protected static MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
#endif

        public MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration)
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(duration));
        }

        public List<Activity> GetActivities()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _activitiesRepository.List();
            });
        }

        public List<DietaryRecommendation> GetDietaryRecommendations()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _dietaryRecommendationsRepository.List();
            });
        }

        public List<Product> GetProducts()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productsRepository.List();
            });
        }

        public List<ProductPack> GetProductPacks()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productPacksRepository.List();
            });
        }

        public List<ProductPackProduct> GetProductPackProducts()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productPackProductsRepository.List();
            });
        }

        public List<Ingredient> GetIngredients()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _ingredientsRepository.List();
            });
        }

        public List<IngredientSubstitute> GetIngredientSubstitutes()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _ingredientSubstitutesRepository.List();
            });
        }

        public List<ProductIngredient> GetProductIngredients()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productIngredientsRepository.List();
            });
        }

        public List<ProductIngredientSubstitute> GetProductIngredientSubstitutes()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _productIngredientSubstitutesRepository.List();
            });
        }

        public List<Protocol> GetProtocols()
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.FiveMinutes));
                return _protocolsRepository.List();
            });
        }

        public void ClearCache()
        {
            MemoryCache.Clear();
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

            var itemsInCategory = items.Where(e => e.Category == model.Category).Select(e => new SortableItem
            {
                Id = e.Id,
                Name = e.Name,
                DisplayIndex = e.ItemCode
            }).ToList();

            itemsInCategory.Add(new SortableItem
            {
                Id = model.Id,
                Name = model.Name,
                DisplayIndex = model.ItemCode
            });

            itemsInCategory = itemsInCategory.OrderBy(e => e.Name).ToList();

            var indexedItems = itemsInCategory.Select((p, i) => new { Model = p, Index = i }).ToList();
            var newIndex = indexedItems.First(e => e.Model.Name == model.Name).Index;
            var firstIndex = indexedItems.Min(e => e.Index);
            var lastIndex = indexedItems.Max(e => e.Index);
            var previousItem = newIndex == firstIndex ? null : itemsInCategory[newIndex - 1];
            var nextItem = newIndex == lastIndex ? null : itemsInCategory[newIndex + 1];

            if (previousItem != null & nextItem != null)
            {
                // This will be in the middle of the list. Get Index half way between nextItem and beginning of category
                var newItemCode = previousItem.DisplayIndex + (int)Math.Round((double)(nextItem.DisplayIndex - previousItem.DisplayIndex) / 2, 0, MidpointRounding.AwayFromZero);

                while (newItemCode < nextItem.DisplayIndex)
                {
                    if (itemsInCategory.Any(e => e.DisplayIndex == newItemCode))
                    {
                        // ItemCode already taken, increment it
                        newItemCode++;
                    }
                    else
                    {
                        return newItemCode;
                    }
                }

                throw new Exception(Globalisation.Dictionary.NewItemCodeNotAvailable);
            }

            if (previousItem == null)
            {
                // This will become the first in the list. Get Index half way between nextItem and beginning of category
                var newItemCode = (int)model.Category + (int)Math.Round((double)(nextItem.DisplayIndex - (int)model.Category) / 2, 0);

                while (newItemCode >= (int)model.Category)
                {
                    if (itemsInCategory.Any(e => e.DisplayIndex == newItemCode))
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

            if (nextItem == null)
            {
                // This will become the last in the list. Increment index by ItemCodeGap
                var newItemCode = previousItem.DisplayIndex + Constants.Constants.ItemCodeGap;

                while (newItemCode <= (int)model.Category + Constants.Constants.CategoryGap)
                {
                    if (itemsInCategory.Any(e => e.DisplayIndex == newItemCode))
                    {
                        // ItemCode already taken, decrease it
                        newItemCode++;
                    }
                    else
                    {
                        return newItemCode;
                    }
                }
            }

            throw new Exception(Globalisation.Dictionary.NewItemCodeNotAvailable);
        }
    }
}