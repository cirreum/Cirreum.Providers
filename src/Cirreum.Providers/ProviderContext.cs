namespace Cirreum.Providers;

/// <summary>
/// Provides runtime context information for the provider model, enabling providers to adapt
/// their behavior based on the type of application runtime they are executing within.
/// </summary>
/// <remarks>
/// This static context must be initialized during application startup by calling
/// <see cref="SetRuntimeType"/> before any provider services are used. The runtime type
/// remains constant throughout the application's lifetime.
/// </remarks>
public static class ProviderContext {
	private static ProviderRuntimeType? _runtimeType;

	/// <summary>
	/// Sets the runtime type for the provider model.
	/// </summary>
	/// <param name="runtimeType">The runtime type to configure.</param>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the runtime type has already been configured.
	/// </exception>
	/// <remarks>
	/// This method should be called once during application startup, typically within
	/// a runtime extension's service registration method.
	/// </remarks>
	public static void SetRuntimeType(ProviderRuntimeType runtimeType) {
		if (_runtimeType.HasValue) {
			throw new InvalidOperationException("Provider runtime type has already been configured.");
		}

		_runtimeType = runtimeType;
	}

	/// <summary>
	/// Gets the configured runtime type for the provider model.
	/// </summary>
	/// <returns>The configured <see cref="ProviderRuntimeType"/>.</returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the runtime type has not been configured via <see cref="SetRuntimeType"/>.
	/// </exception>
	/// <remarks>
	/// Providers use this method to determine the runtime context and adapt their behavior
	/// accordingly (e.g., choosing between JWT-based or cookie-based authentication schemes).
	/// </remarks>
	public static ProviderRuntimeType GetRuntimeType() {
		return _runtimeType ?? throw new InvalidOperationException(
			"Provider runtime type has not been configured. " +
			"Ensure the hosting application calls ProviderContext.SetRuntimeType during startup.");
	}
}