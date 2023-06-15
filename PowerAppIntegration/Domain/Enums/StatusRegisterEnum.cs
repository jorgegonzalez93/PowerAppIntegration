using System.ComponentModel;

namespace Migration.Domain.Domain.Enums
{
    public enum StatusRegisterEnum
    {
        [Description("Aprobado")]
        Approbed,
        [Description("En revisión")]
        Pending,
        [Description("Solicitud de aclaración")]
        Reject,
    }
}
