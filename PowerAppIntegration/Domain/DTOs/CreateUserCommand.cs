using MediatR;

namespace Migration.Domain.Domain.DTOs
{
    public class CreateUserCommand : IRequest<ResponseDto>
    {
        public CreateUserCommand(string displayName, string username, string mail, string telephone)
        {
            DisplayName = displayName;
            Username = username;
            Mail = mail;
            Telephone = telephone;
        }

        public string DisplayName { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Mail { get; set; } = default!;
        public string Telephone { get; set; } = default!;
    }
}
