using Api.Infra.Requests;
using FluentValidation;

namespace Api.Infra.Validators;

public class GetRateRequestDtoValidator : AbstractValidator<GetRateRequestDto>
{
    public GetRateRequestDtoValidator()
    {
        RuleFor(x => x.Currency)
            .IsInEnum();

        RuleFor(x => x.Date)
            .Must(BeValidDate)
            .WithMessage("Invalid date format. Use yyyy-MM-dd.")
            .Must(BeTodayOrPast)
            .When(x => !string.IsNullOrWhiteSpace(x.Date))
            .WithMessage("Date must be today or in the past (yyyy-MM-dd).");
    }

    private static bool BeValidDate(string? date) =>
        date is null || DateOnly.TryParseExact(
            date,
            "yyyy-MM-dd",
            null,
            System.Globalization.DateTimeStyles.None,
            out _);

    private static bool BeTodayOrPast(string? date)
    {
        if (string.IsNullOrWhiteSpace(date)) return true;

        return DateOnly.TryParseExact(
                   date,
                   "yyyy-MM-dd",
                   null,
                   System.Globalization.DateTimeStyles.None,
                   out var parsed)
               && parsed <= DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
