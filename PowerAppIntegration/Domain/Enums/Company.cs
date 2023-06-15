using System.ComponentModel;

namespace Migration.Domain.Domain.Enums
{
    public enum Company
    {
        [Description("xm_numerodedocumento")]
        CompanyIdentification,
        [Description("xm_name")]
        CompanyName,
        [Description("xm_pais")]
        CountryName,
        [Description("xm_numerodecelular")]
        CompanyTelephone,
        [Description("xm_direccion")]
        CompanyAddress,
        [Description("DocumentType")]
        DocumentType,
        [Description("DateCreate")]
        DateCreate,
        [Description("Status")]
        Status,
    }
}

