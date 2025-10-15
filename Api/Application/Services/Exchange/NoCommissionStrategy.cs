using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange;

public sealed class NoCommissionStrategy : IExchangeStrategy
{
    public Money Apply(Money convertedTarget, int roundingDecimals)
        => new(decimal.Round(convertedTarget.Amount, roundingDecimals), convertedTarget.Currency);
}
