namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class ResponseDto
    {
        public string Message { get; set; } = default!;
        public int Code { get; set; } = default!;

        public ResponseDto() { }
    }
}
