namespace DemoRedis.Services
{
    public interface IResponseCacheService
    {
        Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
        Task<string> GetCachedResponseAsync(string cacheKey);
        Task RemoveCacheResponseAsync(string pattern);
    }
}
