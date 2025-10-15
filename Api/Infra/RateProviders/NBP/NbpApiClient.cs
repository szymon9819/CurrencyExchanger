using Api.Domain.Enums;
using Api.Infra.RateProviders.NBP.Responses;

namespace Api.Infra.RateProviders.NBP;

public class NbpApiClient : INbpApiClient
{
    private const string BaseUrl = "https://api.nbp.pl/api/exchangerates/rates";

    private readonly HttpClient _httpClient;

    public NbpApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<NbpAResponse?> GetTableAAsync(Currency currency, DateOnly date, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpAResponse>(
            $"{BaseUrl}/A/{currency.ToString()}/{date:yyyy-MM-dd}?format=json",
            ct
        );

    public Task<NbpAResponse?>
        GetTableARangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpAResponse>(
            $"{BaseUrl}/A/{currency.ToString()}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}?format=json",
            ct
        );

    public Task<NbpAResponse?> GetTableBAsync(Currency currency, DateOnly date, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpAResponse>(
            $"{BaseUrl}/B/{currency.ToString()}/{date:yyyy-MM-dd}?format=json",
            ct
        );

    public Task<NbpAResponse?>
        GetTableBRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpAResponse>(
            $"{BaseUrl}/B/{currency.ToString()}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}?format=json",
            ct
        );

    public Task<NbpCResponse?> GetTableCAsync(Currency currency, DateOnly date, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpCResponse>(
            $"{BaseUrl}/C/{currency.ToString()}/{date:yyyy-MM-dd}?format=json",
            ct
        );

    public Task<NbpCResponse?>
        GetTableCRangeAsync(Currency currency, DateOnly from, DateOnly to, CancellationToken ct) =>
        _httpClient.GetFromJsonAsync<NbpCResponse>(
            $"{BaseUrl}/C/{currency.ToString()}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}?format=json",
            ct
        );
}
