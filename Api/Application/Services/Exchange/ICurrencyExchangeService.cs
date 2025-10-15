using Api.Application.Dtos;
using Api.Domain.Enums;

namespace Api.Application.Services.Exchange;

public interface ICurrencyExchangeService
{
    Task<ExchangeResultDto> ConvertAsync(Currency from, Currency to, decimal amount, DateOnly date, CancellationToken ct);
}
