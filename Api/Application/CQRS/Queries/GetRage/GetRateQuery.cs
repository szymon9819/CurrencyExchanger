using Api.Application.Dtos;
using Api.Domain.Enums;
using MediatR;

namespace Api.Application.CQRS.Queries.GetRage;

public sealed record GetRateQuery(Currency Currency, DateOnly Date) : IRequest<RateDto?>;
