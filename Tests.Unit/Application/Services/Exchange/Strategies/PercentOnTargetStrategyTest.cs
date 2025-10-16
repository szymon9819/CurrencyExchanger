using Api.Application.Services.Exchange.Strategies;
using Api.Domain.Enums;
using Api.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Unit.Application.Services.Exchange.Strategies;

public class PercentOnTargetStrategyTest
{
    public static IEnumerable<object[]> PercentOnTargetCases()
    {
        yield return new object[] { 100m, new Money(11m, Currency.PLN), 4, new Money(0m, Currency.PLN) };
        yield return new object[] { 1m, new Money(100m, Currency.PLN), 0, new Money(99m, Currency.PLN) };
        yield return new object[] { 1m, new Money(100.000000001m, Currency.PLN), 1, new Money(99.0m, Currency.PLN) };
        yield return new object[] { 1m, new Money(100.000000001m, Currency.PLN), 4, new Money(99.0000m, Currency.PLN) };
    }

    [Theory]
    [MemberData(nameof(PercentOnTargetCases))]
    public void Apply_Theory(decimal percent, Money convertedTarget, int roundingDecimals, Money expectedValue)
    {
        var strategy = new PercentOnTargetStrategy(percent);

        strategy.Apply(convertedTarget, roundingDecimals).Should().Be(expectedValue);
    }
}
