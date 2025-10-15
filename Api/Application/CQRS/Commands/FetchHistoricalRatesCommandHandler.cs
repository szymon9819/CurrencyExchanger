using Api.Application.Dtos;
using Api.Application.Repositories;
using MediatR;

namespace Api.Application.CQRS.Commands;

public class FetchHistoricalRatesCommandHandler(
    IRateProvider provider,
    IRateRepository repo) : IRequestHandler<FetchHistoricalRatesCommand, IReadOnlyList<RateDto>>
{
    private const int NbpRatesWindow = 67;
    
    public async Task<IReadOnlyList<RateDto>> Handle(FetchHistoricalRatesCommand request, CancellationToken ct)
    {
        var result = new List<RateDto>();

        for (var start = request.From; start <= request.To; start = start.AddDays(NbpRatesWindow))
        {
            var end = start.AddDays(NbpRatesWindow - 1);
            if (end > request.To) end = request.To;

            var chunk = await provider.GetRatesRangeAsync(request.Currency, start, end, ct);
         
            if (chunk.Count == 0) continue;

            await repo.UpsertRangeAsync(chunk, ct);
            result.AddRange(chunk);
        }

        return result;
    }
}
