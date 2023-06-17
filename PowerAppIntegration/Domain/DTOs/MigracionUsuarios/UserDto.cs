namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
#nullable enable
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; } = default!;
        public string? Username { get; set; } = default!;
        public string? Mail { get; set; } = default!;
        public string? Telephone { get; set; } = default!;
        public DateTime? CreatedDateTime { get; set; } = default!;
        public string? Role { get; set; } = default!;
    }
}
