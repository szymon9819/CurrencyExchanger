namespace Api.Infra.RateProviders.NBP;

public record NbpResponse(
    string Table,
    string Code,
    string Currency,
    NbpRate[] Rates
);
