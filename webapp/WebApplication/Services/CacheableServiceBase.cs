using K9.DataAccessLayer.Interfaces;
using K9.SharedLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace K9.WebApplication.Services
{
    public abstract class CacheableServiceBase<T> : ICategorisableService where T : class, IObjectBase
    {
        protected MemoryCache MemoryCache { get; }

        public CacheableServiceBase()
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration)
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(duration));
        }

        public string GetCacheKey(int id)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{nameof(T)}-{id}";
        }

        public string GetCacheKey()
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{nameof(T)}";
        }

        public string GetCacheKey<T2>() where T2 : class, IObjectBase
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{nameof(T2)}";
        }

        public string GetCacheKey<T2>(int id) where T2 : class, IObjectBase
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{nameof(T2)}-{id}";
        }

        public int CreateItemCode(ICategorisable model, List<ICategorisable> items)
        {
            var itemsInCategory = items.Where(e => e.Category == model.Category).ToList();
            itemsInCategory.Add(model);
            itemsInCategory = itemsInCategory.OrderBy(e => e.Name).ToList();

            var indexedItems = itemsInCategory.Select((p, i) => new { Product = p, Index = i }).ToList();
            var newIndex = indexedItems.First(e => e.Product.Name == model.Name).Index;
            var firstIndex = indexedItems.Min(e => e.Index);
            var lastIndex = indexedItems.Max(e => e.Index);
            var previousItem = newIndex == firstIndex ? null : itemsInCategory[newIndex - 1];
            var nextItem = newIndex == lastIndex ? null : itemsInCategory[newIndex + 1];

            if (previousItem != null & nextItem != null)
            {
                // This will be in the middle of the list. Get Index half way between nextItem and beginning of category
                var newItemCode = previousItem.ItemCode + (int)Math.Round((double)(nextItem.ItemCode - previousItem.ItemCode) / 2, 0, MidpointRounding.AwayFromZero);

                while (newItemCode < nextItem.ItemCode)
                {
                    if (itemsInCategory.Any(e => e.ItemCode == newItemCode))
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
                var newItemCode = (int)model.Category + (int)Math.Round((double)(nextItem.ItemCode - (int)model.Category) / 2, 0);

                while (newItemCode >= (int)model.Category)
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

            if (nextItem == null)
            {
                // This will become the last in the list. Increment index by ItemCodeGap
                var newItemCode = previousItem.ItemCode + Constants.Constants.ItemCodeGap;

                while (newItemCode <= (int)model.Category + Constants.Constants.CategoryGap)
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
            }

            throw new Exception(Globalisation.Dictionary.NewItemCodeNotAvailable);
        }
    }
}