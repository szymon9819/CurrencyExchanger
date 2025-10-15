using Api.Application.Dtos;
using Api.Application.Repositories;
using Api.Domain.Entities;
using Api.Domain.Enums;
using Api.Domain.ValueObjects;
using Api.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infra.Repositories;

public sealed class RateRepository(AppDbContext dbContext) : IRateRepository
{
    public async Task<RateDto?> GetAsync(Currency currency, DateOnly date, CurrencyProvider provider,
        CancellationToken ct)
    {
        var e = await dbContext.Rates.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Currency == currency && r.Date == date && r.CurrencyProvider == provider, ct);

        return e is null
            ? null
            : new RateDto(
                Currency: e.Currency,
                Provider: e.CurrencyProvider,
                Date: e.Date,
                Mid: e.Mid,
                Bid: e.Bid,
                Ask: e.Ask);
    }

    public async Task UpsertAsync(RateDto rate, CancellationToken ct)
    {
        var existing = await dbContext.Rates
            .FirstOrDefaultAsync(
                r => r.Currency == rate.Currency && r.Date == rate.Date && r.CurrencyProvider == rate.Provider, ct);

        if (existing is null)
        {
            dbContext.Rates.Add(new Rate
            {
                Currency = rate.Currency,
                CurrencyProvider = rate.Provider,
                Date = rate.Date,
                Mid = rate.Mid,
                Bid = rate.Bid,
                Ask = rate.Ask
            });
        }
        else
        {
            existing.Mid = rate.Mid;
            existing.Bid = rate.Bid;
            existing.Ask = rate.Ask;
        }

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task UpsertRangeAsync(IEnumerable<RateDto> rates, CancellationToken ct)
    {
        foreach (var rate in rates)
            await UpsertAsync(rate, ct);
    }

    public async Task<Money?> GetMidAsync(
        Currency currency,
        DateOnly date,
        CurrencyProvider provider, 
        CancellationToken ct)
    {
        var rate = await dbContext.Rates
            .AsNoTracking()
            .Where(r => r.Currency == currency && r.CurrencyProvider == provider && r.Date == date)
            .Select(r => new Money(r.Mid, r.Currency))
            .FirstOrDefaultAsync(ct);

        return rate == default ? null : rate;
    }
}
