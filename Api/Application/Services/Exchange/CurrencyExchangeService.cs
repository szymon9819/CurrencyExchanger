using Api.Application.Dtos;
using Api.Application.Services.Rate;
using Api.Domain.Enums;
using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange;

public sealed class CurrencyExchangeService(
    IRateLookupService rateLookup,
    IExchangeStrategyFactory strategyFactory
) : ICurrencyExchangeService
{
    private const int Rounding = 4;

    public async Task<ExchangeResultDto> ConvertAsync(
        Currency from,
        Currency to,
        decimal amount,
        DateOnly date,
        CancellationToken ct)
    {
        var fromRateDto = await rateLookup.GetAsync(from, date, CurrencyProvider.NBP, ct);
        var toRateDto   = await rateLookup.GetAsync(to,   date, CurrencyProvider.NBP, ct);

        if (fromRateDto is null || toRateDto is null)
            throw new InvalidOperationException($"Exchange rate data missing for {date:yyyy-MM-dd}.");

        var gross = ConvertAmount(from, to, amount, fromRateDto, toRateDto);

        var strategy = strategyFactory.ResolveStrategy(amount);
        var net = strategy.Apply(gross, Rounding);
        var commission = gross.Subtract(net);

        return new ExchangeResultDto(
            From: from,
            To: to,
            OriginalAmount: amount,
            ConvertedAmount: net.Amount,
            CommissionAmount: commission.Amount,
            CommissionCurrency: commission.Currency,
            Date: date
        );
    }
    
    private static Money ConvertAmount(Currency from, Currency to, decimal amount, RateDto fromRateDto,
        RateDto toRateDto)
    {
        var fromRate = new Money(fromRateDto.Mid, from);
        var toRate = new Money(toRateDto.Mid, to);

        var sourceAmount = new Money(amount, from);
        return sourceAmount.ConvertTo(fromRate, toRate);
    }
}
