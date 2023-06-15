namespace Migration.Domain.Domain.DTOs
{
    public class PagedListUsersDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
