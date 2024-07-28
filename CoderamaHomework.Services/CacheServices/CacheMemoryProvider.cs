using CoderamaHomework.ServiceContracts.CacheServiceContracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CoderamaHomework.Services.CacheServices;

public class CacheMemoryProvider : ICacheProvider
{
    private IMemoryCache _cache;
    public CacheMemoryProvider(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    public Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class
    {
        return Task.FromResult(_cache.Get<T>(key));
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        return Task.FromResult(_cache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = options.AbsoluteExpiration,
            AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
            SlidingExpiration = options.SlidingExpiration,
        }));
    }
}
