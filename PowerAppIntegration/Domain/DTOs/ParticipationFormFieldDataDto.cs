namespace Migration.Domain.Domain.DTOs
{
    public class ParticipationFormFieldDataDto
    {
        public Guid Id { get; set; }
        public string Field { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
