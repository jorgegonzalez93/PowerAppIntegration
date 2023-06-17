namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class EmployeeDto
    {
        public Guid IdentificationTypeId { get; set; }
        public Guid AdUserId { get; set; }
        public string JobTitle { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Telephone { get; set; } = default!;
        public string IdentificationCard { get; set; } = default!;
        public string ExistenceDocument { get; set; } = default!;
    }
}
