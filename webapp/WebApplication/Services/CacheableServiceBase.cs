using K9.DataAccessLayer.Interfaces;
using K9.SharedLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using K9.WebApplication.Models;

namespace K9.WebApplication.Services
{
    public abstract class CacheableServiceBase<T> : ICacheableService, ICategorisableService
        where T : class, IObjectBase
    {
#if DEBUG
        protected static MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
#else
        protected static MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
#endif

    public MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration)
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(duration));
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

        public string GetCacheKey<T2>() where T2 : class, IObjectBase
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            return $"{callingMethod}-{typeof(T2).Name}";
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