using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange.Strategies;

public sealed class FlatFeeOnTargetStrategy : IExchangeStrategy
{
    private readonly decimal _fee;

    public FlatFeeOnTargetStrategy(decimal fee) => _fee = fee;

    public Money Apply(Money convertedTarget, int roundingDecimals)
    {
        var net = convertedTarget.Amount - _fee;

        if (net < 0m)
        {
            net = 0m;
        }
        
        return new(decimal.Round(net, roundingDecimals), convertedTarget.Currency);
    }
}
