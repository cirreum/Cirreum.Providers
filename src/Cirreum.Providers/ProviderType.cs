namespace Cirreum.Providers;

/// <summary>
/// Represents the core service categories available within the Provider model of the 
/// cirreum framework.
/// </summary>
/// <remarks>
/// Each value identifies a distinct service category that can be registered and configured
/// through an IServiceProviderRegistrar implementations.
/// </remarks>
public enum ProviderType {
	/// <summary>
	/// None or unknown
	/// </summary>
	None = 0,
	/// <summary>
	/// Represents service providers that enable authorization
	/// (e.g., Azure Entra, OKta, etc) for applications built on the framework.
	/// </summary>
	Authorization = 1,
	/// <summary>
	/// Represents secrets configuration providers that enable remote configuration capabilities 
	/// (e.g., Azure KeyVault, AWS Secrets Manager, HashiCorp Vault) for applications built on
	/// the framework.
	/// </summary>
	Secrets = 2,
	/// <summary>
	/// Represents service providers that enable distributed messaging capabilities 
	/// (e.g., Azure Service Bus, AWS SQS, RabbitMQ) for applications built on the framework.
	/// </summary>
	Messaging = 3,
	/// <summary>
	/// Represents service providers that enable cloud-based data storage capabilities
	/// (e.g., Azure Blob Storage, AWS S3) for applications built on the framework.
	/// </summary>
	Storage = 4,
	/// <summary>
	/// Represents service providers that enable cloud-based data persistence capabilities
	/// (e.g., Azure Cosmos DB, AWS Documents) for applications built on the framework.
	/// </summary>
	Persistence = 5,
	/// <summary>
	/// Represents service providers that enable delivering messages via a communication channel
	/// (e.g., SendGrid, Twilio) for applications built on the framework.
	/// </summary>
	Communications = 6,
	/// <summary>
	/// Represents service providers that enable identity integrations via identity-provider
	/// callbacks — for example user provisioning via a webhook/HTTP Connector from Descope or
	/// Auth0, or via the Entra External ID custom-claims extension — for applications built
	/// on the framework.
	/// </summary>
	Identity = 7,
	/// <summary>
	/// Represents service providers that bring inbound invocations into the framework — each
	/// is a source of invocations (e.g., HTTP, SignalR, raw WebSockets, gRPC, queue triggers)
	/// that delivers or manifests work into the framework's pipeline. Each impl populates the
	/// unified IInvocationContext seam at its inbound boundary.
	/// </summary>
	Invocation = 8
}