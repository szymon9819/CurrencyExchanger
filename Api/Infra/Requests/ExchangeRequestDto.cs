using Api.Domain.Enums;

namespace Api.Infra.Requests;

public record ExchangeRequestDto(
    Currency From,
    Currency To,
    decimal Amount
);
