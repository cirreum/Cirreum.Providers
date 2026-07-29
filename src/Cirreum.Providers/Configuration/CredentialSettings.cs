namespace Cirreum.Providers.Configuration;

/// <summary>
/// Identity-based credential configuration for a provider instance, used when the instance
/// authenticates with a platform identity rather than a connection string or key.
/// </summary>
/// <remarks>
/// The meaning of each value is mapped by the provider implementation. Providers that only
/// support key- or connection-string-based access do not consume these settings.
/// </remarks>
public sealed class CredentialSettings {

	/// <summary>
	/// The credential strategy used to authenticate.
	/// Defaults to <see cref="CredentialMode.Default"/>.
	/// </summary>
	public CredentialMode Mode { get; set; } = CredentialMode.Default;

	/// <summary>
	/// Optionally selects a specific platform identity when the host has more than one
	/// available. If not supplied, the platform-assigned identity is used. Ignored by
	/// <see cref="CredentialMode.Developer"/>. Provider implementations document the mapping.
	/// </summary>
	public string? IdentityId { get; set; }

}
