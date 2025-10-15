using Api.Application.Services.Exchange;

namespace Api.Application.Services;

public interface IExchangeStrategyFactory
{
    IExchangeStrategy ResolveStrategy(decimal sourceAmount);
}