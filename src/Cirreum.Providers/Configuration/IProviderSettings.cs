namespace Cirreum.Providers.Configuration;

/// <summary>
/// Defines a providers settings which contains a collection of provider instances settings.
/// </summary>
public interface IProviderSettings<TInstanceSettings>
	where TInstanceSettings : IProviderInstanceSettings {

	/// <summary>
	/// Collection of Provider instance settings
	/// </summary>
	Dictionary<string, TInstanceSettings> Instances { get; set; }

}