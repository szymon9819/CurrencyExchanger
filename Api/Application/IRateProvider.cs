using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Application;

public interface IRateProvider
{
    Task<RateDto?> GetRateAsync(Currency currency, DateOnly date, CancellationToken ct);
    Task<IReadOnlyList<RateDto>> GetRatesRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct);
}
