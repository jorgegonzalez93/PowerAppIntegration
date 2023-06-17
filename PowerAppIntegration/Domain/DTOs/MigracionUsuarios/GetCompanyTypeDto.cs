namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class GetCompanyTypeDto
    {
        public Guid Id { get; set; }
        public string CompanyIdentification { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CompanyTelephone { get; set; } = default!;
        public string CompanyAddress { get; set; } = default!;
        public string CompanyAbbreviation { get; set; } = default!;
    }
}
