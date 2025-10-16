using Api.Application.Services.Exchange.Strategies;
using Api.Domain.Enums;
using Api.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Unit.Application.Services.Exchange.Strategies;

public class PercentAndOnTargetStrategyTest
{
    public static IEnumerable<object[]> PercentAndOnTargetCases()
    {
        yield return new object[] { 100m, 0m, new Money(888888m, Currency.PLN), 4, new Money(0m, Currency.PLN) };
        yield return new object[] { 1m, 99m, new Money(100m, Currency.PLN), 0, new Money(1m, Currency.PLN) };
        yield return new object[] { 1m, 99m, new Money(100m, Currency.PLN), 2, new Money(0.99m, Currency.PLN) };
        yield return new object[] { 10m, 50m, new Money(100m, Currency.PLN), 0, new Money(45m, Currency.PLN) };
    }

    [Theory]
    [MemberData(nameof(PercentAndOnTargetCases))]
    public void Apply_Theory(decimal percent, decimal fee, Money convertedTarget, int roundingDecimals,
        Money expectedValue)
    {
        var strategy = new PercentAndFlatFeeOnSourceStrategy(percent, fee);

        strategy.Apply(convertedTarget, roundingDecimals).Should().Be(expectedValue);
    }
}
