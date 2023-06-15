using Migration.Domain.Domain.DTOs;

namespace Migration.Domain.Domain.Services
{
    public class B2CCreateUser
    {
        private ServiceBusMessageService ServiceBusMessageService { get; set; }

        public B2CCreateUser()
        {
            ServiceBusMessageService = new();

        }
        private const string TOPIC_MIGRATION = "xm-suicc-users-migration-local";
        public async Task CreateUserB2CAsync(EmployeeMigrationDto employee)
        {
            CreateUserCommand createCommand = new(employee.FullName, employee.UserName, employee.Email, employee.Telephone);

            DTOs.Message topicMessage = new DTOs.Message(createCommand);

            await SendMessageTopicAsync(topicMessage, string.Empty);
        }

        public async Task SendMessageTopicAsync(DTOs.Message message, string Subject)
        {
            await ServiceBusMessageService.SendMessageAsync(message, TOPIC_MIGRATION, Subject);
        }
    }
}
