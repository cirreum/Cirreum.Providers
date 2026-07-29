namespace Cirreum.Providers.Configuration;

/// <summary>
/// The credential strategy a provider instance uses to authenticate to its backing service
/// when connecting with a platform identity rather than a connection string or key.
/// </summary>
public enum CredentialMode {

	/// <summary>
	/// Uses the platform's default credential chain, trying each available credential
	/// source in order (for example: environment, workload identity, developer tooling).
	/// </summary>
	Default,

	/// <summary>
	/// Uses the platform-assigned workload identity directly, with no chain probing.
	/// Deterministic, and typically enables retry behavior the default chain cannot offer.
	/// Use <see cref="CredentialSettings.IdentityId"/> to select a specific identity
	/// when the host has more than one.
	/// </summary>
	ManagedIdentity,

	/// <summary>
	/// Uses local developer tooling credentials only (for example: IDE, CLI, or shell sign-in),
	/// authenticating as the signed-in developer. Intended for local runs against real services;
	/// not for deployed environments.
	/// </summary>
	Developer,

}
