namespace Api.Infra.RateProviders.NBP.Responses;

public sealed record NbpAResponse(string Table, string Code, string Currency, NbpAMidRate[] Rates);
