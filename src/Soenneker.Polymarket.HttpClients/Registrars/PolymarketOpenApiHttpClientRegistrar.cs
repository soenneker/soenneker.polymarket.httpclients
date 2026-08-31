using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Polymarket.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Polymarket.HttpClients.Registrars;

/// <summary>
/// Registers the Polymarket multi-API HTTP client provider.
/// </summary>
public static class PolymarketOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="PolymarketOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddPolymarketOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IPolymarketOpenApiHttpClient, PolymarketOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PolymarketOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddPolymarketOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IPolymarketOpenApiHttpClient, PolymarketOpenApiHttpClient>();

        return services;
    }
}
