using Microsoft.Extensions.Caching.Memory;
using System;

namespace K9.WebApplication.Services
{
    public static class MemoryCacheHelper
    {
        
#if DEBUG
        public static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
#else
        public static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
#endif

        public static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration)
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(duration));
        }

        public static void ClearCache()
        {
            Cache.Clear();
        }
    }
}