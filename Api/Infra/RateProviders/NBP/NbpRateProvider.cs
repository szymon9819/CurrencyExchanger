using Api.Application;
using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Infra.RateProviders.NBP;

public class NbpRateProvider : IRateProvider
{
    private readonly INbpApiClient _nbpApiClient;

    public NbpRateProvider(INbpApiClient nbpApiClient)
    {
        _nbpApiClient = nbpApiClient;
    }

    public async Task<RateDto?> GetRateAsync(Currency currency, DateOnly date, CancellationToken ct)
    {
        // todo make some kind of resolver based on currecy that will return correct table,
        // for now i onyl take table A (Currency enum has only 4 currencies from table A)
        
        var nbpAResponse = await _nbpApiClient.GetTableAAsync(currency, date, ct)
                 ?? await _nbpApiClient.GetTableBAsync(currency, date, ct);

        var mid = nbpAResponse?.Rates.FirstOrDefault()?.Mid;

        var nbpCResponse = await _nbpApiClient.GetTableCAsync(currency, date, ct);
        var cItem = nbpCResponse?.Rates.FirstOrDefault();

        if (mid is null && cItem is null) return null;

        return new RateDto(
            Currency: currency,
            Provider: CurrencyProvider.NBP,
            Date: cItem is not null ? DateOnly.Parse(cItem.EffectiveDate)
                 : nbpAResponse is not null ? DateOnly.Parse(nbpAResponse.Rates.First().EffectiveDate)
                 : date,
            Mid: mid ?? default,
            Bid: cItem?.Bid,
            Ask: cItem?.Ask
        );
    }

    public async Task<IReadOnlyList<RateDto>> GetRatesRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var a = await _nbpApiClient.GetTableARangeAsync(currency, from, to, ct) 
                ?? await _nbpApiClient.GetTableBRangeAsync(currency, from, to, ct);

        var c = await _nbpApiClient.GetTableCRangeAsync(currency, from, to, ct);

        var midByDate = a?.Rates.ToDictionary(
            r => DateOnly.Parse(r.EffectiveDate),
            r => r.Mid) ?? new();

        var bidAskByDate = c?.Rates.ToDictionary(
            r => DateOnly.Parse(r.EffectiveDate),
            r => (r.Bid, r.Ask)) ?? new();

        var allDates = midByDate.Keys.Union(bidAskByDate.Keys).OrderBy(d => d);

        var rateDtos = new List<RateDto>();
        
        foreach (var d in allDates)
        {
            midByDate.TryGetValue(d, out var mid);
            bidAskByDate.TryGetValue(d, out var ba);

            rateDtos.Add(new RateDto(
                Currency: currency,
                Provider: CurrencyProvider.NBP,
                Date: d,
                Mid: mid,
                Bid: ba.Bid,
                Ask: ba.Ask
            ));
        }

        return rateDtos;
    }
}
