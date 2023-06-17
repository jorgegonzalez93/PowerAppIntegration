using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;
using static Migration.Domain.Domain.DTOs.MigrationPackDto;
using static Migration.Domain.Domain.Services.FinalModelService.DocumentModelService;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class RegistryMigrationCreateService
    {
        public static RegistryMigrationDto? MigrateContacPlanBAsync(DataRow validContact, EmailDocuments documents)
        {
            RegistryCompanyDto? existingCompany =
                GeneralData.REGISTRY_COMPANYS
                            .FirstOrDefault<RegistryCompanyDto>(company => company.CompanyIdentification.Contains(validContact[Enums.Contact.CompanyIdentification.GetDescription()].ToString()!));


            string status = existingCompany.Status;
            string approbedDate = existingCompany.CreateDate;


            ClarificationRegistryDto? clarification = null;
            if (status.Contains(StatusRegisterEnum.Reject.GetDescription(), StringComparison.InvariantCultureIgnoreCase))
            {
                clarification = new()
                {
                    Observation = validContact[Enums.Contact.Observation.GetDescription()].ToString()!,
                    UserId = GeneralData.AdminUser
                };
            }


            RegistryCompanyMitrationDto companyData = new()
            {
                CompanyAbbreviation = existingCompany.CompanyAbbreviation,
                CompanyAddress = existingCompany.CompanyAddress,
                IsMarketAgent = GeneralData.DEFAULT_ISMARKET_AGENT,
                CompanyIdentification = existingCompany.CompanyIdentification,
                CompanyName = existingCompany.CompanyName,
                CompanyTelephone = existingCompany.CompanyTelephone,
                CompanyTypeId = existingCompany.CompanyTypeId,
                CountryId = existingCompany.CountryId,
            };


            RegistryEmployeeMitrationDto registryEmployee = new()
            {
                AdUserId = Guid.Empty,
                IdentificationTypeId = validContact[Enums.Contact.IdentificationType.GetDescription()].ToString()!,
                Identification = validContact[Enums.Contact.IdentificationNumber.GetDescription()].ToString()!,
                FullName = validContact[Enums.Contact.UserName.GetDescription()].ToString()!,
                Email = validContact[Enums.Contact.Email.GetDescription()].ToString()!,
                JobTitle = validContact[Enums.Contact.JobTitle.GetDescription()].ToString()!,
                Mobile = validContact[Enums.Contact.Telephone.GetDescription()].ToString()!,
                Telephone = validContact[Enums.Contact.Telephone.GetDescription()].ToString()!,
                PersonType = validContact[Enums.Contact.PersonType.GetDescription()].ToString()!,

                ExistenceDocument = Base64Document(documents, DocumentTypeEnum.CertificadoExistencia.GetDescription()),
                IdentificationCard = Base64Document(documents, DocumentTypeEnum.DocumentoIdentidad.GetDescription()),
                Sarlaft = Base64Document(documents, DocumentTypeEnum.Sarlaft.GetDescription()),
                Rut = Base64Document(documents, DocumentTypeEnum.Rut.GetDescription())
            };


            return new()
            {
                CompanyInformation = companyData,
                EmployeeInformation = registryEmployee,
                ClarificationRegistry = clarification,

                Status = status,
                ApprovedDate = DateTime.Parse(approbedDate),
                CreatedDateTime = DateTime.Parse(approbedDate),

                Observation = string.Empty,
                AdAdminUserId = GeneralData.AdminUser,
                FullName = string.Empty
            };
        }

        private static string Base64Document(EmailDocuments documents, string documentName)
        {
            var query = from Document doc in documents.Documents
                        where documentName.Contains(doc.FileType, StringComparison.InvariantCultureIgnoreCase)
                        select doc.Base64Document;

            if (query is null || !query.Any())
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(query.FirstOrDefault()))
            {
                return string.Empty;
            }

            return query.FirstOrDefault()!;
        }
    }
}
