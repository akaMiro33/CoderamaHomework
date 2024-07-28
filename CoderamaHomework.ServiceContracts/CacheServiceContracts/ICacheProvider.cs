using Microsoft.Extensions.Caching.Distributed;

namespace CoderamaHomework.ServiceContracts.CacheServiceContracts
{
    public interface ICacheProvider
    {
        public Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);
        public Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class;
        Task RemoveAsync(string key, CancellationToken token = default);
    }
}
