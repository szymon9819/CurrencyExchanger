using Api.Application.Dtos;
using Api.Application.Repositories;
using Api.Domain.Enums;

namespace Api.Application.Services.Rate;

public sealed class RateLookupService(
    IRateRepository repo,
    IRateProvider provider,
    ILogger<RateLookupService> logger
) : IRateLookupService
{
    public async Task<RateDto?> GetAsync(
        Currency currency,
        DateOnly date,
        CurrencyProvider providerType,
        CancellationToken ct)
    {
        var fromDb = await repo.GetAsync(currency, date, providerType, ct);
        if (fromDb is not null)
        {
            logger.LogDebug("Rate {Currency} {Date} retrieved from database.", currency, date);
            return fromDb;
        }

        logger.LogInformation("Fetching rate {Currency} {Date} from external provider {Provider}.",
            currency, date, providerType);

        var fetched = await provider.GetRateAsync(currency, date, ct);
        if (fetched is null)
        {
            logger.LogWarning("External provider returned no data for {Currency} {Date}.", currency, date);
            return null;
        }

        await repo.UpsertAsync(fetched, ct);
        logger.LogInformation("Rate {Currency} {Date} persisted to database.", currency, date);

        return fetched;
    }
}
