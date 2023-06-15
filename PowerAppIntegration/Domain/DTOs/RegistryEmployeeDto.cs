namespace Migration.Domain.Domain.DTOs
{
    public class RegistryEmployeeDto
    {
        public Guid AdUserId { get; set; }
        public string IdentificationTypeId { get; set; }
        public string JobTitle { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Telephone { get; set; } = default!;
        public string IdentificationCard { get; set; } = default!;
        public string ExistenceDocument { get; set; } = default!;
        public string Rut { get; set; } = default!;
        public string Sarlaft { get; set; } = default!;
        public string PersonType { get; set; } = default!;
    }
}
