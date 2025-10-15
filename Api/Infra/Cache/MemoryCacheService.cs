using Api.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Infra.Cache;

public class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    public Task<T?> GetAsync<T>(string key, CancellationToken ct)
        => Task.FromResult(cache.TryGetValue<T>(key, out var v) ? v : default);

    public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct)
    {
        cache.Set(key, value, ttl);
        return Task.CompletedTask;
    }
}
