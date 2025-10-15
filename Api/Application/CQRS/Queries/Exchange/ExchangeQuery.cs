using Api.Application.Dtos;
using Api.Domain.Enums;
using MediatR;

namespace Api.Application.CQRS.Queries.Exchange;

public sealed record ExchangeQuery(Currency From, Currency To, decimal Amount) : IRequest<ExchangeResultDto>;
