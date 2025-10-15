using Api.Application.CQRS.Queries.Exchange;
using FluentValidation;

namespace Api.Infra.Validators;

public class ExchangeQueryValidator : AbstractValidator<ExchangeQuery>
{
    public ExchangeQueryValidator()
    {
        RuleFor(x => x.From)
            .IsInEnum();

        RuleFor(x => x.To)
            .IsInEnum()
            .NotEqual(x => x.From)
            .WithMessage("Source and target currencies must be different.");

        RuleFor(x => x.Amount)
            .GreaterThan(0m)
            .WithMessage("Amount must be greater than zero.");
    }
}
