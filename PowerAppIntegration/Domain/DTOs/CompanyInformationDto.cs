namespace Migration.Domain.Domain.DTOs
{
    public class CompanyInformationDto
    {
        public Guid Id { get; set; }
        public CompanyTypeDto CompanyType { get; set; }
        public CountryDto Country { get; set; }
        public string CompanyAbbreviation { get; set; } = string.Empty;
        public string CompanyIdentification { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CompanyTelephone { get; set; } = default!;
        public string CompanyAddress { get; set; } = default!;
        public bool Active { get; set; }
        public EmployeeDto? LegalRepresentative { get; set; } = default!;
    }
}
