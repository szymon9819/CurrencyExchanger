using Api.Domain.Enums;

namespace Api.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

        Amount = decimal.Round(amount, 4);
        Currency = currency;
    }

    public Money Multiply(decimal factor) => new(Amount * factor, Currency);
    
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot subtract different currencies.");
        return new Money(Amount - other.Amount, Currency);
    }
    
    public Money ConvertTo(Money fromRate, Money toRate)
    {
        var inBase = Multiply(fromRate.Amount);
        var converted = new Money(inBase.Amount / toRate.Amount, toRate.Currency);
        
        return new Money(decimal.Round(converted.Amount, 4), toRate.Currency);
    }

    public override string ToString() => $"{Amount:0.####} {Currency}";
}
