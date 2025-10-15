using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Application.Services.Rate;

public sealed class CachedRateLookupService(
    IRateLookupService inner,
    ICacheService cache,
    ILogger<CachedRateLookupService> logger
) : IRateLookupService
{
    private static readonly TimeSpan CacheTtl = TimeSpan.FromDays(30);

    public async Task<RateDto?> GetAsync(
        Currency currency,
        DateOnly date,
        CurrencyProvider provider,
        CancellationToken ct)
    {
        var key = ICacheService.Key(currency, date, provider);

        if (await cache.GetAsync<RateDto>(key, ct) is { } cached)
        {
            logger.LogDebug("Rate {Currency} {Date} retrieved from cache.", currency, date);
            return cached;
        }

        var result = await inner.GetAsync(currency, date, provider, ct);
        if (result is not null)
        {
            await cache.SetAsync(key, result, CacheTtl, ct);
        }

        return result;
    }
}
