namespace Api.Infra.RateProviders.NBP.Responses;

public sealed record NbpCBidAskRate(string EffectiveDate, decimal Bid, decimal Ask);
