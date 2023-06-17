namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class RegistryCompanyDto
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
        public string PersonTypeCompany { get; set; } = default!;
        public string CreateDate { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
