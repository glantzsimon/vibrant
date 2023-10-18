using Microsoft.Extensions.Caching.Memory;

namespace K9.WebApplication.Services
{
    public interface ICacheableService
    {
        MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int duration);
        void ClearCache();
    }
}