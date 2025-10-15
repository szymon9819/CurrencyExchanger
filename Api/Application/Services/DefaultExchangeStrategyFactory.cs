using Api.Application.Services.Exchange;

namespace Api.Application.Services;

public sealed class DefaultExchangeStrategyFactory : IExchangeStrategyFactory
{
    private const decimal SmallTxnThreshold = 100m;
    private const decimal HighValueThreshold = 1000m;

    private readonly CommissionOptions _options;

    public DefaultExchangeStrategyFactory(Microsoft.Extensions.Options.IOptions<CommissionOptions> options)
    {
        _options = options.Value;
    }
    
    public IExchangeStrategy ResolveStrategy(decimal sourceAmount)
    {
        if (sourceAmount < SmallTxnThreshold)
            return new FlatFeeOnTargetStrategy(_options.FlatFeeSmallTxn);

        if (sourceAmount >= HighValueThreshold)
            return new PercentOnTargetStrategy(_options.PercentHighValue);

        return new PercentOnTargetStrategy(_options.PercentStandard);
    }
}
