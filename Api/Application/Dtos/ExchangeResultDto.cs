using Api.Domain.Enums;

namespace Api.Application.Dtos;

public record ExchangeResultDto(
    Currency From,
    Currency To,
    decimal OriginalAmount,
    decimal ConvertedAmount,
    decimal CommissionAmount,
    Currency CommissionCurrency,
    DateOnly Date
);
