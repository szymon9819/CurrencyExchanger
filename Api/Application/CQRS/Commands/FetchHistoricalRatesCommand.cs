using Api.Application.Dtos;
using Api.Domain.Enums;
using MediatR;

namespace Api.Application.CQRS.Commands;

public record FetchHistoricalRatesCommand(
    Currency Currency,
    DateOnly From,
    DateOnly To
) : IRequest<IReadOnlyList<RateDto>>;
