using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange.Strategies;

public sealed class PercentAndFlatFeeOnSourceStrategy(decimal percent, decimal fee) : IExchangeStrategy
{
    public Money Apply(Money convertedTarget, int roundingDecimals)
    {
        var factor = 1m - (percent / 100m);

        var net = (convertedTarget.Amount - fee) * factor;

        if (net < 0m) net = 0m;

        return new(decimal.Round(net, roundingDecimals), convertedTarget.Currency);
    }
}
