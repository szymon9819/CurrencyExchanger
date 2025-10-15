using Api.Domain.Enums;

namespace Api.Application.Services;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken ct);
    Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct);

    static string Key(Currency currency, DateOnly date, CurrencyProvider provider)
        => $"rate:{provider}:{currency}:{date:yyyy-MM-dd}";
}
