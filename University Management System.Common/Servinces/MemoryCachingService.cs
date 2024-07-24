using Microsoft.Extensions.Caching.Memory;

namespace University_Management_System.Common.Servinces;

public class MemoryCachingService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCachingService(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public async Task<T> GetAsync<T>(string cacheKey)
    {
        return await Task.FromResult(_cache.TryGetValue(cacheKey, out T value) ? value : default);
    }

    public async Task SetAsync<T>(string cacheKey, T value, TimeSpan cacheDuration)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration
        };
        _cache.Set(cacheKey, value, cacheDuration);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        _cache.Remove(cacheKey);
        await Task.CompletedTask;
    }
}