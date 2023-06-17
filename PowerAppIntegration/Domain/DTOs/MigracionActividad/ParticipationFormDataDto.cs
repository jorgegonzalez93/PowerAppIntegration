namespace Migration.Domain.Domain.DTOs.MigracionActividad
{
    public class ParticipationFormDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public IEnumerable<ParticipationFormFieldDataDto> ParticipationFormFieldsData { get; set; } = Enumerable.Empty<ParticipationFormFieldDataDto>();
        public IEnumerable<ParticipationFormObservationDto> ParticipationFormObservation { get; set; } = Enumerable.Empty<ParticipationFormObservationDto>();
    }
}
