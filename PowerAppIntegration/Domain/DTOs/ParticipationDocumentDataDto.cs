namespace Migration.Domain.Domain.DTOs
{
    public class ParticipationDocumentDataDto
    {
        public Guid? Id { get; set; }
        public string State { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Value { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}