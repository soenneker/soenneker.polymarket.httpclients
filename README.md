[![](https://img.shields.io/nuget/v/soenneker.polymarket.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.polymarket.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.polymarket.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.polymarket.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.polymarket.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.polymarket.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.polymarket.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.polymarket.httpclients/actions/workflows/codeql.yml)

# Soenneker.Polymarket.HttpClients

Provides one `HttpClient` that routes generated requests across Polymarket's Gamma, Data, CLOB, Bridge, Perpetuals, RFQ, and Relayer APIs.

## Installation

```bash
dotnet add package Soenneker.Polymarket.HttpClients
```

## Usage

```csharp
using Soenneker.Polymarket.HttpClients.Abstract;
using Soenneker.Polymarket.HttpClients.Registrars;

services.AddPolymarketOpenApiHttpClientAsSingleton();

IPolymarketOpenApiHttpClient polymarket = serviceProvider
    .GetRequiredService<IPolymarketOpenApiHttpClient>();

HttpClient client = await polymarket.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync(
    "gamma/events?limit=5",
    cancellationToken);
response.EnsureSuccessStatusCode();
```

The first path segment selects the API host and is removed before the request is sent. For example, `gamma/events` targets `https://gamma-api.polymarket.com/events`, while `clob/book` targets `https://clob.polymarket.com/book`.

Override individual hosts with `Polymarket:BaseUrls:Gamma`, `Data`, `Clob`, `Bridge`, `Perps`, `CombosRfq`, or `Relayer`. Public market-data endpoints require no credentials; callers must add the headers and signatures required by private trading or relayer endpoints.

The provider owns the cached routing client. Scoped provider instances receive separate cache entries, so disposing one scope does not invalidate another scope's client.
