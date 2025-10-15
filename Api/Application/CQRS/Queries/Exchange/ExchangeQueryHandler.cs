using Api.Application.Dtos;
using Api.Application.Services.Exchange;
using MediatR;

namespace Api.Application.CQRS.Queries.Exchange;

public sealed class ExchangeQueryHandler(ICurrencyExchangeService exchange) : IRequestHandler<ExchangeQuery, ExchangeResultDto>
{
    public async Task<ExchangeResultDto> Handle(ExchangeQuery request, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
      
        return await exchange.ConvertAsync(request.From, request.To, request.Amount, today, ct);
    }
}
