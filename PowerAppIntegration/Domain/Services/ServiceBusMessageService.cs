using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace Migration.Domain.Domain.Services
{
    public class ServiceBusMessageService
    {
        private readonly ServiceBusClient _serviceBusClient;

        private const string CONNECTIONSTRING = "Endpoint=sb://asb-ceiba-xm-inf8469-dllo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WLJE3pV017egGU5z3hSbgRa7R2IloqXaI+ASbIjPgWs=";

        public ServiceBusMessageService()
        {
            _serviceBusClient = new(CONNECTIONSTRING);
        }

        public async Task SendMessageAsync(DTOs.Message message, string queueOrTopicName, string label = null)
        {
            var oMessage = new ServiceBusMessage(JsonConvert.SerializeObject(message));

            if (!string.IsNullOrWhiteSpace(label))
            {
                oMessage.Subject = label;
            }

            await GetSender(queueOrTopicName).SendMessageAsync(oMessage).ConfigureAwait(false);
        }

        private ServiceBusSender GetSender(string queueOrTopicName) => _serviceBusClient.CreateSender(queueOrTopicName);

        public ServiceBusClient GetServiceBusClient()
            => _serviceBusClient;
    }
}
