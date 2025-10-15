using Api.Domain.Enums;

namespace Api.Infra.Requests;

public sealed record FetchRatesRequestDto(
    Currency Currency,
    DateOnly From,
    DateOnly To
);
