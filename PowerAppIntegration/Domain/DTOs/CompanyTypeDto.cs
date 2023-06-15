namespace Migration.Domain.Domain.DTOs
{
    public class CompanyTypeDto
    {
        public Guid Id { get; set; }
        public string CompanyTypeName { get; set; } = default!;
    }
}
