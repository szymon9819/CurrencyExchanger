using Api.Application.Services.Exchange.Strategies;
using Api.Domain.Enums;
using Api.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Unit.Application.Services.Exchange.Strategies;

public class FlatFeeOnTargetStrategyTest
{
    public static IEnumerable<object[]> FlatFeeCases()
    {
        yield return new object[] { new Money(11m, Currency.PLN), 4, new Money(1m, Currency.PLN) };
        yield return new object[] { new Money(11.11m, Currency.PLN), 0, new Money(1m, Currency.PLN) };
        yield return new object[] { new Money(1m, Currency.PLN), 0, new Money(0m, Currency.PLN) };
    }

    [Theory]
    [MemberData(nameof(FlatFeeCases))]
    public void Apply_Theory(Money convertedTarget, int roundingDecimals, Money expectedValue)
    {
        var strategy = new FlatFeeOnTargetStrategy(10m);

        strategy.Apply(convertedTarget, roundingDecimals).Should().Be(expectedValue);
    }
}
