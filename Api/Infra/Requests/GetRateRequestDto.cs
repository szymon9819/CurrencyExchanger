using Api.Domain.Enums;

namespace Api.Infra.Requests;

public sealed record GetRateRequestDto(
    Currency Currency,
    string? Date = null
);
