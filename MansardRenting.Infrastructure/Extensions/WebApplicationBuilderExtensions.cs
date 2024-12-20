using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MansardRenting.Infrastructure.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		/// <summary>
		/// This method registers all services with their interfaces and implementations of given assembly.
		/// The assembly is taken from the type of random service interface or implementation provided.
		/// </summary>
		/// <param name="serviceType"></param>
		/// <exception cref="InvalidOperationException"></exception>
		public static void AddApplicationServices(this IServiceCollection service, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
			if (serviceAssembly == null)
			{
				throw new InvalidOperationException("No service type provided.");
			}

			Type[] serviceTypes = serviceAssembly.GetTypes().Where(s => s.Name.EndsWith("Service") && !s.IsInterface).ToArray();

			foreach (Type implementationType in serviceTypes)
			{
				Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

				if (interfaceType == null)
				{
					throw new InvalidOperationException($"No interface is provided for service with name: {implementationType.Name}.");
				}

				service.AddScoped(interfaceType, implementationType);
			}
		}
	}
}
