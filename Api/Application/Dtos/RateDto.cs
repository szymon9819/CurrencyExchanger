using Api.Domain.Enums;

namespace Api.Application.Dtos;

public record RateDto(
    Currency Currency,
    CurrencyProvider Provider,
    DateOnly Date,
    decimal Mid,
    decimal? Bid,
    decimal? Ask
);
