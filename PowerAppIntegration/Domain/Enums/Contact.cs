using System.ComponentModel;

namespace Migration.Domain.Domain.Enums
{
    public enum Contact
    {

        [Description("IdentificationNumber")]
        IdentificationNumber,
        [Description("IdentificationType")]
        IdentificationType,
        [Description("fullname")]
        UserName,
        [Description("emailaddress1")]
        Email,        
        [Description("jobtitle")]
        JobTitle,
        [Description("telephone1")]
        Telephone,
        [Description("Identificacion Compañia")]
        CompanyIdentification,      
        [Description("ApprobedDate")]
        ApprobedDate,
        [Description("Status")]
        Status,
        [Description("Observation")]
        Observation,
        [Description("PersonType")]
        PersonType,
        [Description("adx_identity_username")]
        IdentityUsername,
    }
}

