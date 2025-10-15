using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Application.Repositories;

public interface IRateRepository
{
    Task<RateDto?> GetAsync(Currency currency, DateOnly date, CurrencyProvider provider, CancellationToken ct);
    Task UpsertAsync(RateDto rate, CancellationToken ct);
    Task UpsertRangeAsync(IEnumerable<RateDto> rates, CancellationToken ct);
}
