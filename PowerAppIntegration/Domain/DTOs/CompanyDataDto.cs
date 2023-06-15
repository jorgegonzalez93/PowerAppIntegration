namespace Migration.Domain.Domain.DTOs
{
    public class CompanyDataDto
    {
        public Guid Id { get; set; }
        public Guid CompanyTypeId { get; set; }
        public Guid CountryId { get; set; }
        public string CompanyIdentification { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CompanyTelephone { get; set; } = default!;
        public string CompanyAddress { get; set; } = default!;
        public string CreateDate { get; set; } = default!;
    }
}
