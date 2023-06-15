using static Migration.Domain.Domain.DTOs.MigrationPackDto;

namespace Migration.Domain.Domain.DTOs
{
    public class MigrationPack
    {
        public List<List<RegistryMigrationDto>> userPack { get; set; } = default!;
        public List<List<B2CDataUser>> userB2C { get; set; } = default!;
    }
}
