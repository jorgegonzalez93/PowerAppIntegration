using Migration.Domain.Domain.Enums;

namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class UserFilter
    {
        public string? Search { get; set; } = default!;
        public FilterTypeDateTime? FilterTypeDateTime { get; set; }
        public DateTime? ModifDateTime { get; set; }
        public DateTime? ModifDateTimeEnd { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
