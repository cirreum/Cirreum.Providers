# Changelog

All notable changes to **Cirreum.Providers** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Updated

- Updated NuGet packages.

## [1.2.0] - 2026-05-17

### Added

- `ProviderType.Authentication = 9` â€” new enum value recognizing Authentication as a first-class provider pillar distinct from `Authorization`. Auth-pillar provider registrars (ApiKey, SignedRequest, SessionTicket, OIDC bearer schemes) report this value to distinguish "prove who the caller is" from "decide what an authenticated caller may do." Strictly additive â€” existing consumers of `ProviderType.Authorization` are unaffected.

## [1.1.1] - 2026-05-06

### Fixed

- **Renamed `ProviderType.Connection` â†’ `ProviderType.Invocation`** (value `8` unchanged). Reflects the framing that each provider in this family is a *source* of invocations into the framework (HTTP, SignalR, WebSockets, gRPC, queue triggers, â€¦); transports deliver/manifest invocations through the unified `IInvocationContext` seam. "Connection" was too narrow â€” it fit the long-lived sub-state but not the family. **Safe rename:** the value was added in 1.1.0 specifically for the new package family that has not yet shipped, so no consumer references `ProviderType.Connection` in published code.

## [1.1.0] - 2026-05-06

### Added

- `ProviderType.Connection = 8` â€” new enum value identifying provider impls that bridge inbound dispatch shapes (HTTP, SignalR, raw WebSockets, gRPC, â€¦) into the framework. Adopted by the new `Cirreum.ConnectionProvider` family. Strictly additive â€” `switch` consumers without a default arm will warn at compile time but do not break at runtime.

> **Renamed in 1.1.1.** This entry is preserved for historical accuracy; the active enum value is `ProviderType.Invocation` per the 1.1.1 entry above.
