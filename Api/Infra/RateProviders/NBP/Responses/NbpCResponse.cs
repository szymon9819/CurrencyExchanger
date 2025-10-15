namespace Api.Infra.RateProviders.NBP.Responses;

public sealed record NbpCResponse(string Table, string Code, string Currency, NbpCBidAskRate[] Rates);
