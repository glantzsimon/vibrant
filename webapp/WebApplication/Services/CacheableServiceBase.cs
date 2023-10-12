using K9.SharedLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;

namespace K9.WebApplication.Services
{
    public class CacheableServiceBase<T> where T : class, IObjectBase
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
    }
}