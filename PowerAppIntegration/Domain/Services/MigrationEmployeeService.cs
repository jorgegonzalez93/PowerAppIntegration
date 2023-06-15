using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Services.FinalModelService;
using Migration.Domain.Infrastructure.Adapters;
using Migration.Domain.Infrastructure.Logs;
using System.Data;

namespace Migration.Domain.Domain.Services
{
    public class MigrationEmployeeService
    {
        private ValidationMigrationService RequiredFieldsMigration { get; set; }
        private AgentService AgentService { get; set; }

        private B2CCreateUser B2CCreateUser { get; set; }

        public MigrationEmployeeService()
        {
            RequiredFieldsMigration = new();
            AgentService = new();
            B2CCreateUser = new();
        }

        public async Task<RegistryDto?> MigrateContactAsync(DataRow contact)
        {
            string fileName = "Employee";

            ValidationMigrationService requiredFields = RequiredFieldsMigration.EmployeeRequired();

            bool createUser = RequiredFieldsMigration.ProspectiveData(contact, requiredFields);

            if (!createUser)
            {
                return null;
            }

            EmployeeMigrationDto employee = CreateEmployeeService.CreateEmployeeObject(contact, false);

            RegistryCompanyDto? existingCompany = GeneralData.REGISTRY_COMPANYS.FirstOrDefault(company => company.CompanyIdentification.Contains(employee.CompanyIdentification));

            RegistryCompanyDto companyData = new()
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


            var validationList = new List<string>();

            string existenceDocument64 = DocumentService.UploadDocuments(employee, GeneralData.CERTIFICADO_EXISTENCIA);
            validationList.Add(existenceDocument64);
            string RUT64 = DocumentService.UploadDocuments(employee, GeneralData.RUT);
            validationList.Add(RUT64);
            string documentIdentity64 = DocumentService.UploadDocuments(employee, GeneralData.DOCUMENTOIDENTIDAD);
            validationList.Add(documentIdentity64);
            string sarlaft64 = DocumentService.UploadDocuments(employee, GeneralData.SARLAFT);
            validationList.Add(sarlaft64);

            if (validationList.Any(validation => validation.Contains("error de documento", StringComparison.InvariantCultureIgnoreCase)))
            {
                ApplicationLogService.GenerateLogError(contact, ValidateDocumentData(validationList), fileName);
                return null;
            }

            RegistryEmployeeDto registryEmployee = new()
            {
                AdUserId = Guid.Empty,
                IdentificationTypeId = employee.IdentificationTypeId,
                Identification = employee.Identification,
                FullName = employee.FullName,
                Email = employee.Email,
                JobTitle = employee.JobTitle,
                Mobile = employee.Mobile,
                PersonType = employee.PersonType,
                Telephone = employee.Telephone,
                ExistenceDocument = existenceDocument64,
                IdentificationCard = documentIdentity64,
                Sarlaft = sarlaft64,
                Rut = RUT64,
            };

            ApplicationLogService.GenerateLogError(contact, "Usuario migrado con exito", fileName);

            return new()
            {
                CompanyInformation = companyData,
                EmployeeInformation = registryEmployee
            };
        }

        private string ValidateDocumentData(List<string> validationList)
        {
            if (validationList.Any(validation => validation.Contains("error de documento", StringComparison.InvariantCultureIgnoreCase)))
            {

                string documentosFaltantes = string.Join(" - ", validationList
                    .Where(validation => validation.Contains("error de documento", StringComparison.InvariantCultureIgnoreCase)));

                return $"Documento requerido no encontrado: {documentosFaltantes}";
            }

            return string.Empty;
        }

        public RegistryCompanyDto? MigrateCompanyAsync(DataRow company)
        {
            RegistryCompanyDto registryCompanyDto;

            ValidationMigrationService requiredFields = RequiredFieldsMigration.CompanyRequired();

            bool createCompany = RequiredFieldsMigration.ProspectiveData(company, requiredFields);

            if (!createCompany)
            {
                return null;
            }

            CompanyDataCreateCommand newCompany = CreateCompanyService.CreateCompanyObject(company);

            registryCompanyDto = new()
            {
                CompanyIdentification = newCompany.CompanyIdentification,
                CompanyAbbreviation = newCompany.companyAbreviation,
                CompanyAddress = newCompany.CompanyAddress,
                IsMarketAgent = false,
                CompanyName = newCompany.CompanyName,
                CompanyTelephone = newCompany.CompanyTelephone,
                CountryId = newCompany.CountryId,
                CompanyTypeId = newCompany.CompanyTypeId,
                Status = newCompany.status,
                CreateDate = newCompany.dateCreate,
                PersonTypeCompany = newCompany.personType
            };

            return registryCompanyDto;
        }
    }
}

