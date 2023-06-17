namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class MigrationPackDto
    {
        public class RegistryMigrationDto
        {
            public RegistryCompanyMitrationDto CompanyInformation { get; set; } = default!;
            public RegistryEmployeeMitrationDto EmployeeInformation { get; set; } = default!;
            public ClarificationRegistryDto? ClarificationRegistry { get; set; } = default!;
            public bool IsEdit { get; set; }
            public Guid RegistryId { get; set; }
            public string Status { get; set; } = default!;
            public string Observation { get; set; } = default!;
            public DateTime? CreatedDateTime { get; set; } = default!;
            public DateTime? ApprovedDate { get; set; } = default!;
            public string AdAdminUserId { get; set; }
            public string FullName { get; set; } = default!;
        }

        public class RegistryCompanyMitrationDto
        {
            public Guid Id { get; set; }
            public string? CompanyTypeId { get; set; }
            public string CountryId { get; set; }
            public string CompanyIdentification { get; set; } = default!;
            public string CompanyName { get; set; } = default!;
            public string CompanyTelephone { get; set; } = default!;
            public string CompanyAddress { get; set; } = default!;
            public string CompanyAbbreviation { get; set; } = default!;
            public bool IsMarketAgent { get; set; }
        }

        public class RegistryEmployeeMitrationDto
        {
            public Guid AdUserId { get; set; }
            public string IdentificationTypeId { get; set; }
            public string JobTitle { get; set; } = default!;
            public string Identification { get; set; } = default!;
            public string FullName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Mobile { get; set; } = default!;
            public string Telephone { get; set; } = default!;
            public string IdentificationCard { get; set; } = string.Empty;
            public string ExistenceDocument { get; set; } = string.Empty;
            public string Rut { get; set; } = string.Empty;
            public string Sarlaft { get; set; } = string.Empty;
            public string PersonType { get; set; } = default!;
            public Guid? Id { get; set; }
        }
        public class ClarificationRegistryDto
        {
            public Guid ClarificationRegistryId { get; set; }
            public string UserName { get; set; } = default!;
            public string? Observation { get; set; } = default!;
            public DateTime? ObservationDate { get; set; }
            public Guid OEFRegistryId { get; set; }
            public string? UserId { get; set; }
        }
    }
}
