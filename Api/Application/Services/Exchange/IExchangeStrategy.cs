using Api.Domain.ValueObjects;

namespace Api.Application.Services.Exchange;

public interface IExchangeStrategy
{
    Money Apply(Money convertedTarget, int roundingDecimals);
}
