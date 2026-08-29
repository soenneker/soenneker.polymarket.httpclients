[![](https://img.shields.io/nuget/v/soenneker.polymarket.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.polymarket.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.polymarket.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.polymarket.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.polymarket.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.polymarket.httpclients/)

# Soenneker.Polymarket.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Polymarket.HttpClients
```

## Quick start

```csharp
using Soenneker.Polymarket.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddPolymarketOpenApiHttpClientAsSingleton();
```

Adds `PolymarketOpenApiHttpClient` as a singleton service.

## What you get

- `IPolymarketOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `PolymarketOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `PolymarketOpenApiHttpClientRegistrar.AddPolymarketOpenApiHttpClientAsSingleton(services)` | Adds `PolymarketOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `PolymarketOpenApiHttpClientRegistrar.AddPolymarketOpenApiHttpClientAsScoped(services)` | Adds `PolymarketOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
