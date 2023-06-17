using Migration.Domain.Domain.DTOs;

namespace Migration.Domain.Infrastructure.Adapters;

public class MechanismService
{
    private const string PATH_DYNAMIC_FORM = "https://localhost:5443/api/Participation/SaveDynamicForm";

    private readonly GenericHttpRequest GenericHttpRequest;

    public MechanismService()
    {
        GenericHttpRequest = new();
    }

    public async Task SaveDynamicFormAsync(ParticipationDataInputDto participationDataInput)
    {
        Guid participationId = await GenericHttpRequest.DynamicFromPostDataAsync<Guid>(PATH_DYNAMIC_FORM, participationDataInput);
    }
}
