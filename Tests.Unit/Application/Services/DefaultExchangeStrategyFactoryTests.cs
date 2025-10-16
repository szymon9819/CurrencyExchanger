using Api.Application.Services;
using Api.Application.Services.Exchange;
using Api.Application.Services.Exchange.Strategies;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Tests.Unit.Application.Services;

public class DefaultExchangeStrategyFactoryTests
{
    public static IEnumerable<object[]> GetStrategyCases()
    {
        yield return new object[] { 0m, typeof(FlatFeeOnTargetStrategy) };
        yield return new object[] { 99.999m, typeof(FlatFeeOnTargetStrategy) };
        yield return new object[] { 1000m, typeof(PercentOnTargetStrategy) };
        yield return new object[] { 9999m, typeof(PercentOnTargetStrategy) };
        yield return new object[] { 100m, typeof(PercentAndFlatFeeOnSourceStrategy) };
        yield return new object[] { 990.999m, typeof(PercentAndFlatFeeOnSourceStrategy) };
    }

    [Theory]
    [MemberData(nameof(GetStrategyCases))]
    public void ResolveStrategy_Theory(decimal amount, Type expectedType)
    {
        var options = Options.Create(new CommissionOptions
        {
            FlatFeeSmallTxn = 5m,
            PercentStandard =  0.02m,
            PercentHighValue = 0.01m
        });

        var factory = new DefaultExchangeStrategyFactory(options);

        var strategy = factory.ResolveStrategy(amount);
        
        strategy.Should().BeOfType(expectedType);
    }
}
