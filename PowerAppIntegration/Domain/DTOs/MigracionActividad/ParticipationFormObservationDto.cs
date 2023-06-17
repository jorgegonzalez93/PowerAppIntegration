namespace Migration.Domain.Domain.DTOs.MigracionActividad
{
    public class ParticipationFormObservationDto
    {
        public Guid ParticipationFormDataId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
