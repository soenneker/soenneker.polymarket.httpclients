using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Polymarket.HttpClients.Abstract;

/// <summary>
/// Provides an HTTP client that routes generated Polymarket requests to the appropriate API host.
/// </summary>
public interface IPolymarketOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached routing client owned by this provider.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The configured HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
