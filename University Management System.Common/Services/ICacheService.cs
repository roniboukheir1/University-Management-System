namespace University_Management_System.Common.Servinces;

public interface ICacheService
{
    public Task<T> GetAsync<T>(string cacheKey);
    public Task SetAsync<T>(string cacheKey, T value, TimeSpan cacheDuration);
    public Task RemoveAsync(string cacheKey);
}