namespace Migration.Domain.Domain.DTOs
{
    public class RegistryDto
    {
        public RegistryCompanyDto CompanyInformation { get; set; } = default!;
        public RegistryEmployeeDto EmployeeInformation { get; set; } = default!;
        public bool IsEdit { get; set; }
    }
}
