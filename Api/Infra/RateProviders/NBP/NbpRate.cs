namespace Api.Infra.RateProviders.NBP;

public record NbpRate(
    string EffectiveDate,
    decimal Mid,
    decimal? Bid,
    decimal? Ask
);
