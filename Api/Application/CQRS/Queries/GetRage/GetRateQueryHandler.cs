using Api.Application.Dtos;
using Api.Application.Services.Rate;
using Api.Domain.Enums;
using MediatR;

namespace Api.Application.CQRS.Queries.GetRage;

public sealed class GetRateQueryHandler(
    IRateLookupService rateLookupService
    ) : IRequestHandler<GetRateQuery, RateDto?>
{
    public async Task<RateDto?> Handle(GetRateQuery request, CancellationToken ct)
    {
        return await rateLookupService.GetAsync(request.Currency, request.Date, CurrencyProvider.NBP, ct);
    }
}
