namespace Migration.Domain.Domain.DTOs
{

    public record CompanyDataCreateCommand(string CompanyTypeId,
                                           string CountryId,
                                           string CompanyIdentification,
                                           string CompanyName,
                                           string CompanyTelephone,
                                           string CompanyAddress,
                                           string SarlaftDocument,
                                           string companyAbreviation,
                                           string personType,
                                           string dateCreate,
                                           string status,
                                           string documentType);
}
