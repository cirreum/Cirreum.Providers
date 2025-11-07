namespace Cirreum.Providers;

using Cirreum.Providers.Configuration;

/// <summary>
/// Defines the common registrar.
/// </summary>
/// <typeparam name="TSettings">The Type of the provider's settings</typeparam>
/// <typeparam name="TInstanceSettings">The Type of the instance settings.</typeparam>
public interface IProviderRegistrar<TSettings, TInstanceSettings>
	where TSettings : IProviderSettings<TInstanceSettings>
	where TInstanceSettings : IProviderInstanceSettings {

	/// <summary>
	/// Gets the service category that this registrar handles.
	/// </summary>
	/// <value>
	/// A <see cref="ProviderType"/> value indicating which category this registrar supports
	/// (e.g., Messaging, Storage).
	/// </value>
	ProviderType ProviderType { get; }

	/// <summary>
	/// Defines the name of the provider and is used as the configuration section name in appsettings.json
	/// where this provider's settings are defined.
	/// </summary>
	/// <value>
	/// A string identifying the name of the provider.
	/// </value>
	/// <remarks>
	/// The section name follows established naming conventions:
	/// <list type="bullet">
	///   <item>
	///     <description>Assembly-based: Uses the final segment of the assembly name (e.g., 'Azure' from 'Cirreum.Storage.Azure')</description>
	///   </item>
	///   <item>
	///     <description>Service-specific: Or use a descriptive name for the service implementation (e.g., 'AzureBlobs' for Azure Blob Storage)</description>
	///   </item>
	/// </list>
	/// This section name is used to locate and load provider-specific instance configuration settings during service registration.
	/// </remarks>
	string ProviderName { get; }

	/// <summary>
	/// Allows the implementor to perform settings validation prior to adding to the DI Container.
	/// </summary>
	/// <param name="settings">An instance of the provider-specific settings populated from application settings.</param>
	void ValidateSettings(TInstanceSettings settings);

}