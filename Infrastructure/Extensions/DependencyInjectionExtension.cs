
using Azure.Messaging.ServiceBus;
using System.Diagnostics.CodeAnalysis;

namespace Migration.Infrastructure.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHelpers(configuration)
                .AddRepositories(configuration)
                .AddServices(configuration);

            return services;
        }

        private static IServiceCollection AddHelpers(this IServiceCollection services, IConfiguration configuration)
        {

            var serviceBusClient = new ServiceBusClient(configuration.GetValue<string>("ServiceBusClient:ConnectionString"));
            services.AddSingleton(serviceBusClient);
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAgentService, AgentService>();
            return services;
        }
    }
}
