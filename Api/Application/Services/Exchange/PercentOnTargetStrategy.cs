using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange;

public sealed class PercentOnTargetStrategy(decimal percent) : IExchangeStrategy
{
    public Money Apply(Money convertedTarget, int roundingDecimals)
    {
        var factor = 1m - (percent / 100m);
        
        var net = convertedTarget.Amount * factor;
        
        if (net < 0m) net = 0m;
        
        return new(decimal.Round(net, roundingDecimals), convertedTarget.Currency);
    }
}
