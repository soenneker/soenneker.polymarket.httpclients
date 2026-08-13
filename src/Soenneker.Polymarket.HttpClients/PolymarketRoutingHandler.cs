using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Soenneker.Polymarket.HttpClients;

internal sealed class PolymarketRoutingHandler : DelegatingHandler
{
    private readonly IReadOnlyDictionary<string, Uri> _baseUrls;

    public PolymarketRoutingHandler(IConfiguration configuration)
    {
        _baseUrls = new Dictionary<string, Uri>(StringComparer.OrdinalIgnoreCase)
        {
            ["bridge"] = GetBaseUrl(configuration, "Bridge", "https://bridge.polymarket.com"),
            ["data"] = GetBaseUrl(configuration, "Data", "https://data-api.polymarket.com"),
            ["perps"] = GetBaseUrl(configuration, "Perps", "https://api.perpetuals.polymarket.com"),
            ["clob"] = GetBaseUrl(configuration, "Clob", "https://clob.polymarket.com"),
            ["combos-rfq"] = GetBaseUrl(configuration, "CombosRfq", "https://combos-rfq-api.polymarket.com"),
            ["gamma"] = GetBaseUrl(configuration, "Gamma", "https://gamma-api.polymarket.com"),
            ["relayer"] = GetBaseUrl(configuration, "Relayer", "https://relayer-v2.polymarket.com")
        };
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Uri requestUri = request.RequestUri ?? throw new InvalidOperationException("The Polymarket request URI is missing.");
        string path = requestUri.AbsolutePath;
        int secondSlash = path.IndexOf('/', 1);
        string prefix = secondSlash < 0 ? path[1..] : path[1..secondSlash];

        if (!_baseUrls.TryGetValue(prefix, out Uri? baseUrl))
            throw new InvalidOperationException($"Unknown Polymarket API prefix '{prefix}'.");

        string routedPath = secondSlash < 0 ? "/" : path[secondSlash..];
        var routedUri = new UriBuilder(baseUrl)
        {
            Path = CombinePaths(baseUrl.AbsolutePath, routedPath),
            Query = requestUri.Query.TrimStart('?')
        };

        request.RequestUri = routedUri.Uri;
        return base.SendAsync(request, cancellationToken);
    }

    private static Uri GetBaseUrl(IConfiguration configuration, string key, string fallback)
    {
        return new Uri(configuration[$"Polymarket:BaseUrls:{key}"] ?? fallback, UriKind.Absolute);
    }

    private static string CombinePaths(string basePath, string requestPath)
    {
        if (string.IsNullOrEmpty(basePath) || basePath == "/")
            return requestPath;

        return $"{basePath.TrimEnd('/')}/{requestPath.TrimStart('/')}";
    }
}
