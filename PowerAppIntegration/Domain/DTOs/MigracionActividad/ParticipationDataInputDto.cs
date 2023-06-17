namespace Migration.Domain.Domain.DTOs.MigracionActividad
{
    public class ParticipationDataInputDto
    {
        public string CorreoApoderado { get; set; } = string.Empty;
        public Guid ParticipationId { get; set; }
        public Guid MechanismActivityId { get; set; }
        public Guid UserId { get; set; }
        public Guid AdminId { get; set; }
        public string AdminName { get; set; } = string.Empty;
        public string NIT { get; set; } = string.Empty;
        public Guid AgentId { get; set; }
        public Guid? PlantId { get; set; }
        public string? PlantName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool Draft { get; set; }
        public string State { get; set; } = string.Empty;
        public IEnumerable<string> ContactEmails { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<ParticipationFormDataDto> ParticipationFormData { get; set; } = Enumerable.Empty<ParticipationFormDataDto>();
        public IEnumerable<ParticipationDocumentDataDto> ParticipationDocumentData { get; set; } = Enumerable.Empty<ParticipationDocumentDataDto>();
    }
}
