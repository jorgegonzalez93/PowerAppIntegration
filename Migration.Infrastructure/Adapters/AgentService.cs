using Microsoft.Extensions.Configuration;
using Migration.Domain.DTOs;
using Migration.Domain.Ports;

namespace Migration.Infrastructure.Adapters
{
    public class AgentService : IAgentService
    {
        private readonly IConfiguration configuration;
        private readonly IGenericRestClient genericRestClient;

        private readonly string baseUrl;
        private readonly string ocpSubscriptionKey;

        public AgentService(IConfiguration configuration, IGenericRestClient genericRestClient)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.genericRestClient = genericRestClient;

            baseUrl = configuration.GetSection("AgentMicroservice:BaseUrl").Value ?? throw new ArgumentNullException(nameof(configuration));
            ocpSubscriptionKey = configuration.GetSection("AgentMicroservice:HeaderOcpKey").Value ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<EmployeeDto> RegisterEmployee(EmployeeMigrationDto userMigration)
        {
            var pathRequest = baseUrl + configuration.GetSection("AgentMicroservice:ResourcePath:RegisterByUserIdArray").Value;
            return await genericRestClient.PostAsJsonAsync<EmployeeDto>($"{pathRequest}", userMigration, ocpSubscriptionKey);
        }
    }
}
