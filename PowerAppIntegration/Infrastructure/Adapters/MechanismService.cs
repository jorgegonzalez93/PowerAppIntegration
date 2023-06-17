using MediatR;
using Migration.Domain.Domain.DTOs.MigracionActividad;
using Migration.Domain.Infrastructure.Logs;

namespace Migration.Domain.Infrastructure.Adapters;

public class MechanismService
{
    //private const string LOGS_PATH = @"C:\\Users\\guillermo.gallego\\Downloads\\PARTICIPACIONES.TXT";
    private const string PATH_DYNAMIC_FORM = "https://localhost:5443/api/Participation/SaveDynamicForm";

    private readonly GenericHttpRequest GenericHttpRequest;

    public MechanismService()
    {
        GenericHttpRequest = new();
    }

    public async Task SaveDynamicFormAsync(ParticipationDataInputDto participationDataInput)
    {


        Guid participationId = await GenericHttpRequest.DynamicFromPostDataAsync<Guid>(PATH_DYNAMIC_FORM, new CreateDynamicFormCommand(participationDataInput));
        ApplicationLogService.GenerateLogByMessage(nameof(participationId), participationId.ToString(), "PARTICIPATIONIDS");
    }
}

public record CreateDynamicFormCommand(ParticipationDataInputDto ParticipationDataDto);