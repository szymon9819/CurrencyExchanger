using Api.Domain.Enums;
using Api.Infra.RateProviders.NBP.Responses;

namespace Api.Infra.RateProviders.NBP;

public interface INbpApiClient
{
    Task<NbpAResponse?> GetTableAAsync(Currency currency, DateOnly date, CancellationToken ct);
    Task<NbpAResponse?> GetTableARangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct);

    Task<NbpAResponse?> GetTableBAsync(Currency currency, DateOnly date, CancellationToken ct);
    Task<NbpAResponse?> GetTableBRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct);

    Task<NbpCResponse?> GetTableCAsync(Currency currency, DateOnly date, CancellationToken ct);
    Task<NbpCResponse?> GetTableCRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct);
}
