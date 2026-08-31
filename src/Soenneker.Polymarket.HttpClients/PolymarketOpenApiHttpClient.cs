using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Polymarket.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Polymarket.HttpClients;

public sealed class PolymarketOpenApiHttpClient : IPolymarketOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(PolymarketOpenApiHttpClient)}:{Guid.NewGuid():N}";

    private const string _routingBaseUrl = "https://gamma-api.polymarket.com";

    public PolymarketOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, _config, static config =>
        {
            return new HttpClientOptions
            {
                BaseAddress = new Uri(_routingBaseUrl),
                DelegatingHandlerFactories = [() => new PolymarketRoutingHandler(config)]
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
