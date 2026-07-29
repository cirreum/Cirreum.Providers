# Cirreum.Providers 1.3.0 — A Shared Credential Vocabulary for Provider Instances

## Why this release exists

Several Cirreum provider implementations authenticate to their backing service with a platform
identity instead of a connection string — and until now, each did so with a hardcoded default
credential and no configuration surface at all. No tenant pinning, no way to select a user-assigned
identity on a host that has several, no deterministic mode for production. These are keyed,
multi-instance providers, where two instances in two tenants is a legitimate configuration; a
process-global environment variable is not an answer.

One provider recently grew its own local answer, which proved the shape but could not be shared:
the provider-family settings bases live in sibling packages that cannot reference each other. The
vocabulary itself belongs in the one package both families already build on — this one.

## What's new

Two types in `Cirreum.Providers.Configuration`:

**`CredentialMode`** — a vendor-neutral taxonomy of credential strategies:

| Mode | Meaning |
|---|---|
| `Default` | The platform's default credential chain, trying each source in order |
| `ManagedIdentity` | The platform-assigned workload identity, directly — deterministic, no chain probing |
| `Developer` | Developer tooling credentials only (IDE, CLI, shell sign-in) — authenticate as the signed-in developer for local runs against real services |

**`CredentialSettings`** — the configuration-bindable carrier, surfaced by provider instance
settings as a nested `Credential` block:

```json
"Instances": {
  "Primary": {
    "Credential": {
      "Mode": "ManagedIdentity",
      "IdentityId": "<platform identity id>"
    }
  }
}
```

`IdentityId` selects a specific platform identity when the host has more than one; each provider
implementation documents its mapping (Azure providers resolve it as a user-assigned managed
identity client ID). It is ignored by `Developer`.

The taxonomy is deliberately platform-neutral: the same three strategies exist on every major
cloud (default chain, workload identity, developer tooling), so future non-Azure providers adopt
the same vocabulary without new surface here.

## Coordinated downstream work

This release is the foundation rung of a wave. In order, each behind its own release cycle:

1. `Cirreum.SecretsProvider` and `Cirreum.ServiceProvider` (minors) — surface the nested
   `Credential` block on their instance settings bases; `ServiceProviderInstanceSettings` also
   gains the `Identifier` property its sibling base already has.
2. `Cirreum.Secrets.Azure` 2.0.0 — replaces its provider-local credential surface with this
   shared shape.
3. `Cirreum.Persistence.Azure` (major) — identity-based authentication gains the full credential
   surface, with fail-fast validation of unsupported combinations.
4. The remaining Azure providers (Storage, Messaging, Communications) adopt as minors.

## Compatibility

Purely additive. Nothing in this package consumes the new types; they take effect only as the
provider families adopt them in the downstream releases above. Existing consumers are unaffected.

## See also

- [CHANGELOG](CHANGELOG.md)
- [README — Credential Configuration](../README.md#credential-configuration)
