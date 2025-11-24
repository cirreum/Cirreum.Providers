namespace Cirreum.Providers;

/// <summary>
/// Represents the runtime context in which provider services are executing.
/// </summary>
public enum ProviderRuntimeType {
	/// <summary>
	/// Web API runtime - stateless API endpoints with token-based authentication.
	/// </summary>
	WebApi = 0,

	/// <summary>
	/// Web Application runtime - stateful application with cookie-based authentication and UI.
	/// </summary>
	WebApp = 1
}