using System.ComponentModel;

namespace Migration.Domain.Domain.Enums
{
    public enum DocumentTypeEnum
    {
        [Description("CertificadoExistencia")]
        CertificadoExistencia,
        [Description("Rut")]
        Rut,
        [Description("DocumentoIdentidad")]
        DocumentoIdentidad,
        [Description("Sarlaft")]
        Sarlaft,

    }
}
