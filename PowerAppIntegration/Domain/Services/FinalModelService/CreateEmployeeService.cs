using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Helpers;
using System.Data;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class CreateEmployeeService
    {
        public static EmployeeMigrationDto CreateEmployeeObject(DataRow newEmployee, bool isb2cCreateCommand)
        {
            string email = newEmployee[Enums.Contact.Email.GetDescription()].ToString()!;

            string fullName = newEmployee[Enums.Contact.UserName.GetDescription()].ToString()!;

            string userNickName = newEmployee[Enums.Contact.IdentityUsername.GetDescription()].ToString()!;

            string identificationType = newEmployee[Enums.Contact.IdentificationType.GetDescription()].ToString()!;

            string telephoneNumber = newEmployee[Enums.Contact.Telephone.GetDescription()].ToString()!;

            string companyIdentification = newEmployee[Enums.Contact.CompanyIdentification.GetDescription()].ToString()!;

            string jobTitle = newEmployee[Enums.Contact.JobTitle.GetDescription()].ToString()!;

            jobTitle = CleanDataHelper.DefaultUserRol(jobTitle);

            identificationType = CleanDataHelper.CleanIdentificationType(identificationType);

            userNickName = CleanDataHelper.CleanUserNickName(email, userNickName);

            fullName = CleanDataHelper.CleanFullUserName(email, fullName);

            telephoneNumber = CleanDataHelper.CleanTelephoneNumber(telephoneNumber);

            companyIdentification = CleanDataHelper.CleanCompanyIdentification(companyIdentification);

            string personTypeName = string.Empty;

            if (!isb2cCreateCommand)
            {
                RegistryCompanyDto? company = GeneralData.REGISTRY_COMPANYS.FirstOrDefault(company => company?.CompanyIdentification == companyIdentification);

                personTypeName = company?.CompanyTypeId!;

                personTypeName = CleanDataHelper.CleanPersonType(personTypeName);
            }

            return new()
            {
                IsFromMvn = true,
                IsFromOds = false,
                FullName = fullName,
                Email = email,
                Telephone = telephoneNumber,
                JobTitle = jobTitle,
                IdentificationTypeId = identificationType,
                AdUserId = Guid.Empty,
                ExistenceDocument = string.Empty,
                Identification = string.Empty,
                IdentificationCard = string.Empty,
                Mobile = telephoneNumber,
                UserName = userNickName,
                PersonTypeId = string.Empty,
                PersonType = personTypeName,
                CompanyIdentification = companyIdentification
            };
        }
    }
}
