using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;


namespace App.Caching.Caching
{
    public class CacheService(IMemoryCache _memoryCache) : ICacheService
    {
        public Task TCreateAsync<T>(string cacheKey, T t, TimeSpan cacheTime)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = cacheTime,
            };
            _memoryCache.Set(cacheKey, t, cacheOptions);
            return Task.CompletedTask;
        }

        public Task<T?> TGetListAllAsync<T>(string cacheKey)
        {
            if(_memoryCache.TryGetValue(cacheKey,out T cacheValues)) return Task.FromResult(cacheValues);
            return Task.FromResult(default(T));
        }

        public Task TRemoveAsync(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }
    }
}
