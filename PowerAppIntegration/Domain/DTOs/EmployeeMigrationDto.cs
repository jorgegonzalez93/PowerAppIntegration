namespace Migration.Domain.Domain.DTOs
{
#nullable enable
    public class EmployeeMigrationDto
    {
        public string IdentificationTypeId { get; set; }
        public Guid AdUserId { get; set; }
        public string JobTitle { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Telephone { get; set; } = default!;
        public string IdentificationCard { get; set; } = default!;
        public string ExistenceDocument { get; set; } = default!;
        public bool IsFromOds { get; set; }
        public bool IsFromMvn { get; set; }
        public string UserName { get; set; } = default!;
        public string PersonTypeId { get; set; }
        public string PersonType { get; set; }
        public string CompanyId { get; set; }
        public string CompanyIdentification { get; set; }
    }
}
