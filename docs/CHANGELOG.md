# Changelog

All notable changes to **Cirreum.Providers** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.3.1] - 2026-08-16

### Updated

- Updated NuGet packages.

## [1.3.0] - 2026-07-29

### Added

- `CredentialMode` — a vendor-neutral credential taxonomy for provider instances that authenticate
  with a platform identity rather than a connection string or key: `Default` (the platform's default
  credential chain), `ManagedIdentity` (the platform-assigned workload identity — deterministic, no
  chain probing), and `Developer` (developer tooling credentials only, authenticating as the
  signed-in developer for local runs against real services). Provider implementations map each mode
  to their platform's credential type.
- `CredentialSettings` — configuration-bindable settings carrying the selected `Mode` plus an
  optional `IdentityId` that selects a specific platform identity when the host has more than one
  (for example, Azure providers resolve it as a user-assigned managed identity client ID; other
  platforms document their own mapping). Surfaced by the provider-family instance settings bases as
  a nested `Credential` block in a coordinated downstream wave; providers that only support key- or
  connection-string-based access do not consume it.

## [1.2.3] - 2026-07-18

### Updated

- Updated NuGet packages.

## [1.2.2] - 2026-07-04

### Updated

- Updated NuGet packages.

## [1.2.1] - 2026-06-05

### Updated

- Updated NuGet packages.

## [1.2.0] - 2026-05-17

### Added

- `ProviderType.Authentication = 9` — new enum value recognizing Authentication as a first-class provider pillar distinct from `Authorization`. Auth-pillar provider registrars (ApiKey, SignedRequest, SessionTicket, OIDC bearer schemes) report this value to distinguish "prove who the caller is" from "decide what an authenticated caller may do." Strictly additive — existing consumers of `ProviderType.Authorization` are unaffected.

## [1.1.1] - 2026-05-06

### Fixed

- **Renamed `ProviderType.Connection` → `ProviderType.Invocation`** (value `8` unchanged). Reflects the framing that each provider in this family is a *source* of invocations into the framework (HTTP, SignalR, WebSockets, gRPC, queue triggers, …); transports deliver/manifest invocations through the unified `IInvocationContext` seam. "Connection" was too narrow — it fit the long-lived sub-state but not the family. **Safe rename:** the value was added in 1.1.0 specifically for the new package family that has not yet shipped, so no consumer references `ProviderType.Connection` in published code.

## [1.1.0] - 2026-05-06

### Added

- `ProviderType.Connection = 8` — new enum value identifying provider impls that bridge inbound dispatch shapes (HTTP, SignalR, raw WebSockets, gRPC, …) into the framework. Adopted by the new `Cirreum.ConnectionProvider` family. Strictly additive — `switch` consumers without a default arm will warn at compile time but do not break at runtime.

> **Renamed in 1.1.1.** This entry is preserved for historical accuracy; the active enum value is `ProviderType.Invocation` per the 1.1.1 entry above.
