using Api.Infra.Requests;
using FluentValidation;

namespace Api.Infra.Validators;

public sealed class FetchRatesRequestDtoValidator : AbstractValidator<FetchRatesRequestDto>
{
    public FetchRatesRequestDtoValidator()
    {
        RuleFor(x => x.Currency)
            .IsInEnum();

        RuleFor(x => x.From)
            .LessThanOrEqualTo(_ => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("'From' must be today or in the past.");

        RuleFor(x => x.To)
            .LessThanOrEqualTo(_ => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("'To' must be today or in the past.")
            .GreaterThanOrEqualTo(x => x.From)
            .WithMessage("'To' must be greater than or equal to 'From'.");
    }
}
