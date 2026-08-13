using Soenneker.Polymarket.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Polymarket.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PolymarketOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPolymarketOpenApiHttpClient _httpclient;

    public PolymarketOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IPolymarketOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
