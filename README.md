# Cirreum - Provider Model Library

[![NuGet Version](https://img.shields.io/nuget/v/Cirreum.Providers.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Providers/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Cirreum.Providers.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Providers/)
[![GitHub Release](https://img.shields.io/github/v/release/cirreum/Cirreum.Providers?style=flat-square)](https://github.com/cirreum/Cirreum.Providers/releases)

A shared library that defines the core abstractions and utilities for implementing a consistent provider pattern across the Cirreum platform. This library enables pluggable, configuration-driven service providers for various infrastructure concerns including messaging, storage, persistence, authorization, and more.

## Overview

Cirreum.Providers establishes a standardized approach for integrating third-party services and cloud providers into applications built on the Cirreum foundation framework. It provides:

- **Common interfaces** for provider registration and configuration
- **Type-safe settings management** with support for multiple provider instances
- **Extensible provider categories** through the `ProviderType` enumeration
- **Service collection utilities** for tracking and managing provider registrations

## Key Concepts

### Provider Types

The library defines several core service categories through the `ProviderType` enum:

- **Authorization** - Identity and access management providers (Azure Entra, Okta, etc.)
- **Secrets** - Remote configuration and secrets management (Azure KeyVault, AWS Secrets Manager, HashiCorp Vault)
- **Messaging** - Distributed messaging systems (Azure Service Bus, AWS SQS, RabbitMQ)
- **Storage** - Cloud storage services (Azure Blob Storage, AWS S3)
- **Persistence** - Data persistence solutions (Azure Cosmos DB, AWS DocumentDB)
- **Communications** - Message delivery channels (SendGrid, Twilio)

### Provider Architecture

Each provider implementation consists of:

1. **Provider Registrar** - Implements `IProviderRegistrar<TSettings, TInstanceSettings>` to register services with the DI container
2. **Provider Settings** - Implements `IProviderSettings<TInstanceSettings>` to define configuration structure
3. **Instance Settings** - Implements `IProviderInstanceSettings` for per-instance configuration
4. **Configuration Section** - Named section in appsettings.json matching the `ProviderName`

## Core Interfaces

### IProviderRegistrar<TSettings, TInstanceSettings>

The primary interface that provider implementations must implement:

```csharp
public interface IProviderRegistrar<TSettings, TInstanceSettings>
    where TSettings : IProviderSettings<TInstanceSettings>
    where TInstanceSettings : IProviderInstanceSettings
{
    ProviderType ProviderType { get; }
    string ProviderName { get; }
    void ValidateSettings(TInstanceSettings settings);
}
```

**Properties:**
- `ProviderType` - Indicates which service category the provider supports
- `ProviderName` - Configuration section name in appsettings.json

**Methods:**
- `ValidateSettings()` - Performs validation before adding to the DI container

### IProviderSettings<TInstanceSettings>

Defines the settings structure containing multiple provider instances:

```csharp
public interface IProviderSettings<TInstanceSettings>
    where TInstanceSettings : IProviderInstanceSettings
{
    Dictionary<string, TInstanceSettings> Instances { get; set; }
}
```

### IProviderInstanceSettings

Marker interface for provider-specific instance settings:

```csharp
public interface IProviderInstanceSettings { }
```

## Configuration Pattern

Provider configuration follows a consistent structure in appsettings.json:

```json
{
  "ProviderName": {
    "Instances": {
      "InstanceName1": {
        // Instance-specific settings
      },
      "InstanceName2": {
        // Instance-specific settings
      }
    }
  }
}
```

### Naming Conventions

The `ProviderName` follows established conventions:

- **Assembly-based**: Uses the final segment of the assembly name
  - Example: `Azure` from `Cirreum.Storage.Azure`
- **Service-specific**: Uses a descriptive name for the implementation
  - Example: `AzureBlobs` for Azure Blob Storage

## Service Collection Extensions

The library provides extension methods for tracking provider registrations:

### IsMarkerTypeRegistered<T>()

Checks if a specific type has been registered in the service collection:

```csharp
if (!services.IsMarkerTypeRegistered<MyProvider>())
{
    // Register the provider
}
```

### MarkTypeAsRegistered<T>()

Marks a specific type as registered to prevent duplicate registrations:

```csharp
services.MarkTypeAsRegistered<MyProvider>();
```

These utilities help prevent duplicate provider registrations and enable conditional registration logic.

## Usage Example

### 1. Define Instance Settings

```csharp
public class MyProviderInstanceSettings : IProviderInstanceSettings
{
    public string ConnectionString { get; set; }
    public int Timeout { get; set; }
}
```

### 2. Define Provider Settings

```csharp
public class MyProviderSettings : IProviderSettings<MyProviderInstanceSettings>
{
    public Dictionary<string, MyProviderInstanceSettings> Instances { get; set; }
}
```

### 3. Implement Provider Registrar

```csharp
public class MyProviderRegistrar : IProviderRegistrar<MyProviderSettings, MyProviderInstanceSettings>
{
    public ProviderType ProviderType => ProviderType.Storage;
    
    public string ProviderName => "MyProvider";
    
    public void ValidateSettings(MyProviderInstanceSettings settings)
    {
        if (string.IsNullOrEmpty(settings.ConnectionString))
            throw new ArgumentException("ConnectionString is required");
            
        if (settings.Timeout <= 0)
            throw new ArgumentException("Timeout must be positive");
    }
}
```

### 4. Configure in appsettings.json

```json
{
  "MyProvider": {
    "Instances": {
      "Primary": {
        "ConnectionString": "...",
        "Timeout": 30
      },
      "Secondary": {
        "ConnectionString": "...",
        "Timeout": 60
      }
    }
  }
}
```

## Design Benefits

### Consistency
All providers follow the same registration and configuration patterns, making the codebase more maintainable and easier to understand.

### Flexibility
Support for multiple instances of the same provider type enables scenarios like primary/backup configurations or multi-region deployments.

### Type Safety
Generic constraints ensure compile-time validation of settings types and proper implementation contracts.

### Validation
Built-in validation hooks allow providers to enforce configuration requirements before registration.

### Discoverability
The `ProviderType` enumeration makes it easy to discover which provider categories are available in the framework.

## Dependencies

- **Microsoft.Extensions.DependencyInjection.Abstractions**

## Integration

This library is part of the Cirreum foundation framework and is referenced by specific provider implementations such as:

- `Cirreum.Storage.Azure`
- `Cirreum.Messaging.ServiceBus`
- `Cirreum.Secrets.KeyVault`
- And other provider-specific libraries

## Contributing

When implementing a new provider:

1. Reference this library
2. Implement the required interfaces (`IProviderRegistrar`, `IProviderSettings`, `IProviderInstanceSettings`)
3. Follow the established naming conventions for `ProviderName`
4. Implement thorough validation in `ValidateSettings()`
5. Add appropriate XML documentation
6. Ensure configuration follows the standard structure

---

**Cirreum Foundation Framework** - Layered simplicity for modern .NET