using System.Globalization;
using Api.Application.CQRS.Commands;
using Api.Application.CQRS.Queries.Exchange;
using Api.Application.CQRS.Queries.GetRage;
using Api.Domain.Enums;
using Api.Infra.Requests;
using FluentValidation;
using MediatR;

namespace Api.Extensions;

public static class RegisterEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/currencies", () =>
            {
                var items = Enum.GetValues<Currency>()
                    .Select(c => new { name = c.ToString() })
                    .ToArray();
                
                return Results.Ok(items);
            })
            .WithName("GetAvailableCurrencies")
            .WithTags("Metadata")
            .WithOpenApi();

        var rates = app.MapGroup("/rates")
            .WithTags("Rates")
            .WithOpenApi();

        rates.MapGet("/", async (
                [AsParameters] GetRateRequestDto request,  
                IValidator<GetRateRequestDto> validator,
                ISender sender,
                CancellationToken ct) =>
            {
                var validation = await validator.ValidateAsync(request, ct);
                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                var date = !string.IsNullOrWhiteSpace(request.Date)
                    ? DateOnly.ParseExact(request.Date!, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None)
                    : DateOnly.FromDateTime(DateTime.UtcNow);

                var dto = await sender.Send(new GetRateQuery(request.Currency, date), ct);
                return dto is null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetRate");

        rates.MapGet("/exchange", async (
                [AsParameters] ExchangeQuery request,
                IValidator<ExchangeQuery> validator,
                ISender sender,
                CancellationToken ct) =>
            {
                var validation = await validator.ValidateAsync(request, ct);

                if (!validation.IsValid)
                    return Results.ValidationProblem(validation.ToDictionary());
                
                var result = await sender.Send(request, ct);
                return Results.Ok(result);
            })
            .WithName("ExchangeMoney");

        rates.MapPost("/fetch", async (
                [AsParameters] FetchRatesRequestDto request,
                IValidator<FetchRatesRequestDto> validator,
                ISender sender,
                CancellationToken ct) =>
            {
                var validation = await validator.ValidateAsync(request, ct);
                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());
                
                var list = await sender.Send(new FetchHistoricalRatesCommand(request.Currency, request.From, request.To), ct);
                return Results.Ok(new { currency = request.Currency, from = request.From, to = request.To, count = list.Count, data = list });
            })
            .WithName("FetchRange");

        app.MapGet("/", () => Results.Redirect("/swagger"));

        return app;
    }
}