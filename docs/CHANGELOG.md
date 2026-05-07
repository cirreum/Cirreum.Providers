# Changelog

All notable changes to **Cirreum.Providers** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- `ProviderType.Connection = 8` — new enum value identifying provider impls that bridge inbound dispatch shapes (HTTP, SignalR, raw WebSockets, gRPC, …) into the framework. Adopted by the new `Cirreum.ConnectionProvider` family per [ADR-0002](https://github.com/cirreum/Cirreum.DevOps/blob/main/docs/adr/0002-unified-invocation-context.md). Strictly additive — `switch` consumers without a default arm will warn at compile time but do not break at runtime.
