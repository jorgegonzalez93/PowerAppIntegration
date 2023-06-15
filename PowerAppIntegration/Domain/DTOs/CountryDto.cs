namespace Migration.Domain.Domain.DTOs
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; } = default!;
        public string CallingCode { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
    }
}
