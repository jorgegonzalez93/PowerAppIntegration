namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public class SaveFileDto
    {
        public string ContainerName { get; set; } = default!;
        public string FolderName { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public Stream FileData { get; set; } = default!;
    }
}