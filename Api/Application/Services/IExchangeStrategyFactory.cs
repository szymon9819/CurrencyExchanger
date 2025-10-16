using Api.Application.Services.Exchange.Strategies;

namespace Api.Application.Services;

public interface IExchangeStrategyFactory
{
    IExchangeStrategy ResolveStrategy(decimal sourceAmount);
}