# Changelog

All notable changes to **Cirreum.Providers** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.1] - 2026-05-06

### Fixed

- **Renamed `ProviderType.Connection` → `ProviderType.Invocation`** (value `8` unchanged). Reflects ADR-0002's framing: each provider in this family is a *source* of invocations into the framework (HTTP, SignalR, WebSockets, gRPC, queue triggers, …); transports deliver/manifest invocations through the unified `IInvocationContext` seam. "Connection" was too narrow — it fit the long-lived sub-state but not the family. **Safe rename:** the value was added in 1.1.0 specifically for the new package family that has not yet shipped, so no consumer references `ProviderType.Connection` in published code.

## [1.1.0] - 2026-05-06

### Added

- `ProviderType.Connection = 8` — new enum value identifying provider impls that bridge inbound dispatch shapes (HTTP, SignalR, raw WebSockets, gRPC, …) into the framework. Adopted by the new `Cirreum.ConnectionProvider` family per [ADR-0002](https://github.com/cirreum/Cirreum.DevOps/blob/main/docs/adr/0002-unified-invocation-context.md). Strictly additive — `switch` consumers without a default arm will warn at compile time but do not break at runtime.

> **Renamed in 1.1.1.** This entry is preserved for historical accuracy; the active enum value is `ProviderType.Invocation` per the 1.1.1 entry above.
