
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

    }
}
