namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Extension methods for IServiceCollection to support type-based registration tracking.
/// </summary>
public static class ServiceRegistrationExtensions {

	// Private class to store registered types
	private class MarkerTypeRegistrationTracker {

		// Set of registered types
		public HashSet<Type> RegisteredTypes { get; } = [];

		public bool IsTypeRegistered(Type type) {
			return this.RegisteredTypes.Contains(type);
		}

		public void MarkTypeAsRegistered(Type type) {
			this.RegisteredTypes.Add(type);
		}

	}

	/// <summary>
	/// Checks if a specific type has been registered.
	/// </summary>
	/// <typeparam name="T">The type to check registration for.</typeparam>
	/// <param name="services">The service collection.</param>
	/// <returns>True if already registered, false otherwise.</returns>
	public static bool IsMarkerTypeRegistered<T>(this IServiceCollection services) {

		// Get or create the registration tracker
		var tracker = GetOrCreateTypeTracker(services);

		// Check if this specific type is registered
		return tracker.IsTypeRegistered(typeof(T));
	}

	/// <summary>
	/// Marks a specific type as registered.
	/// </summary>
	/// <typeparam name="T">The type to register.</typeparam>
	/// <param name="services">The service collection.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection MarkTypeAsRegistered<T>(this IServiceCollection services) {

		// Get or create the registration tracker
		var tracker = GetOrCreateTypeTracker(services);

		// Mark this specific type as registered
		tracker.MarkTypeAsRegistered(typeof(T));

		return services;
	}

	// Helper method to get or create the registration tracker
	private static MarkerTypeRegistrationTracker GetOrCreateTypeTracker(IServiceCollection services) {

		// Look for an existing tracker
		var descriptor = services.FirstOrDefault(d =>
			d.ServiceType == typeof(MarkerTypeRegistrationTracker) &&
			d.Lifetime == ServiceLifetime.Singleton);

		if (descriptor != null) {

			if (descriptor.ImplementationInstance is MarkerTypeRegistrationTracker existingTracker) {
				// Return the existing tracker
				return existingTracker;
			}

			throw new InvalidOperationException(
				$"Expected private instance registration but found {(descriptor.ImplementationFactory != null ? "factory" : "implementation type")} registration.");

		}

		// Create a new tracker and register it
		var tracker = new MarkerTypeRegistrationTracker();
		services.Replace(new ServiceDescriptor(typeof(MarkerTypeRegistrationTracker), tracker));
		return tracker;

	}

}