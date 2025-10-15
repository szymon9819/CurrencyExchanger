using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Application.Services.Rate;

public interface IRateLookupService
{
    Task<RateDto?> GetAsync(Currency currency, DateOnly date, CurrencyProvider provider, CancellationToken ct);
}
