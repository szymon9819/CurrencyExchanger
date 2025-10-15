namespace Api.Application.Services.Exchange;

public sealed class CommissionOptions
{
    public const string SectionName = "Commission";

    public decimal FlatFeeSmallTxn { get; init; } = 2.00m;
    public decimal PercentStandard { get; init; } = 1.25m;
    public decimal PercentHighValue { get; init; } = 0.50m;
}
