namespace Migration.Domain.Domain.DTOs
{
    public class ResponseDto
    {
        public string Message { get; set; } = default!;
        public int Code { get; set; } = default!;

        public ResponseDto() { }
    }
}
