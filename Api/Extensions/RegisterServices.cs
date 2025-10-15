using Api.Application;
using Api.Application.Repositories;
using Api.Application.Services;
using Api.Application.Services.Exchange;
using Api.Application.Services.Rate;
using Api.Infra.Cache;
using Api.Infra.Data;
using Api.Infra.RateProviders.NBP;
using Api.Infra.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        // DbContext
        var cs = config.GetConnectionString("Default")!;
        services.AddDbContext<AppDbContext>(o => o.UseMySql(cs, ServerVersion.AutoDetect(cs)));

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Cache
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();

        // HttpClient + resilience
        services.AddHttpClient<INbpApiClient, NbpApiClient>(client =>
            {
                client.BaseAddress = new Uri(config["Nbp:BaseUrl"] ?? "https://api.nbp.pl/api/exchangerates/rates");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            })
            .AddStandardResilienceHandler();

        // Currency Providers
        services.AddScoped<IRateProvider, NbpRateProvider>();

        // Repositories
        services.AddScoped<IRateRepository, RateRepository>();

        // MediatR
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

        // FluentValidation
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        // Options (bind + validate)
        services
            .AddOptions<CommissionOptions>()
            .BindConfiguration(CommissionOptions.SectionName)
            .ValidateDataAnnotations()
            .Validate(o =>
                o.FlatFeeSmallTxn >= 0m &&
                o.PercentStandard >= 0m &&
                o.PercentHighValue >= 0m, "Commission values must be non-negative.")
            .ValidateOnStart();

        // Services
        services.AddScoped<IExchangeStrategyFactory, DefaultExchangeStrategyFactory>();
        services.AddScoped<ICurrencyExchangeService, CurrencyExchangeService>();
        services.AddScoped<IRateLookupService, RateLookupService>();
        // services.AddScoped<IRateLookupService, CachedRateLookupService>(); 

        return services;
    }
}
