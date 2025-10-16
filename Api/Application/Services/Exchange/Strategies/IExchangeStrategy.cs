using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange.Strategies;

public interface IExchangeStrategy
{
    Money Apply(Money convertedTarget, int roundingDecimals);
}
