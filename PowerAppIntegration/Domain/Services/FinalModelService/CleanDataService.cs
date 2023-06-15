using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;
using System.Globalization;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class CleanDataService
    {
        public static void CleanDataRowCompany(DataRow newCompany)
        {
            newCompany[Company.CompanyIdentification.GetDescription()] = CleanDataHelper.CleanCompanyIdentification(newCompany[Company.CompanyIdentification.GetDescription()].ToString()!);

            newCompany[Company.CompanyName.GetDescription()] = newCompany[Company.CompanyName.GetDescription()]
              .ToString()!
              .ToUpperInvariant();

            newCompany[Company.CountryName.GetDescription()] = CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(newCompany[Company.CountryName.GetDescription()]
                .ToString()!
                .ToLower()!);

            newCompany[Company.CompanyTelephone.GetDescription()] = CleanDataHelper.CleanTelephoneNumber(newCompany[Company.CompanyTelephone.GetDescription()].ToString()!);

            newCompany[Company.CompanyAddress.GetDescription()] = newCompany[Company.CompanyAddress.GetDescription()]
              .ToString()!
              .ToUpperInvariant();

            newCompany[Company.DocumentType.GetDescription()] = CleanDataHelper.CleanIdentificationType(newCompany[Company.DocumentType.GetDescription()].ToString()!);

            newCompany[Company.DateCreate.GetDescription()] = CleanDataHelper.CleanDate(newCompany[Company.DateCreate.GetDescription()].ToString()!);
            newCompany[Company.Status.GetDescription()] = CleanDataHelper.CleanStatus(newCompany[Company.Status.GetDescription()].ToString()!);
        }

        public static void CleanDataRowContact(DataRow newContact)
        {
            newContact[Contact.Email.GetDescription()] = newContact[Contact.Email.GetDescription()]
                .ToString()!
                .ToLowerInvariant();

            newContact[Contact.UserName.GetDescription()] = newContact[Contact.UserName.GetDescription()]
                .ToString()!
                .ToLower();

            newContact[Contact.IdentityUsername.GetDescription()] = newContact[Contact.IdentityUsername.GetDescription()]
               .ToString()!
               .ToLower()!
               .Replace(" ", "");

            newContact[Contact.IdentificationType.GetDescription()] = newContact[Contact.IdentificationType.GetDescription()]
               .ToString()!
               .ToLower()!
               .Replace(" ", "");

            newContact[Contact.Telephone.GetDescription()] = newContact[Contact.Telephone.GetDescription()]
                .ToString()!
                .ToUpper();

            newContact[Contact.CompanyIdentification.GetDescription()] = 
                CleanDataHelper.CleanCompanyIdentification(newContact.GetValueData(Contact.CompanyIdentification.GetDescription()));

            newContact[Contact.JobTitle.GetDescription()] = CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(newContact[Contact.JobTitle.GetDescription()]
                .ToString()!
                .ToLower()!);

            newContact[Contact.Status.GetDescription()] = newContact[Contact.Status.GetDescription()]
              .ToString()!
              .ToLower()
              .ToLowerInvariant();

            newContact[Contact.JobTitle.GetDescription()] = CleanDataHelper.DefaultUserRol(newContact.GetValueData(Contact.JobTitle.GetDescription()));

            newContact[Contact.IdentificationType.GetDescription()] = CleanDataHelper.CleanIdentificationType(newContact.GetValueData(Contact.IdentificationType.GetDescription()));

            newContact[Contact.UserName.GetDescription()] =
                CleanDataHelper.CleanFullUserName(newContact.GetValueData(Contact.Email.GetDescription()), newContact.GetValueData(Contact.UserName.GetDescription()));

            newContact[Contact.Telephone.GetDescription()] = CleanDataHelper.CleanTelephoneNumber(newContact.GetValueData(Contact.Telephone.GetDescription()));

            newContact[Contact.IdentityUsername.GetDescription()] =
                CleanDataHelper.CleanUserNickName(newContact.GetValueData(Contact.Email.GetDescription()), newContact.GetValueData(Contact.IdentityUsername.GetDescription()));

            newContact[Contact.Status.GetDescription()] =
                CleanDataHelper.CleanStatus(newContact.GetValueData(Contact.Status.GetDescription()));

            IEnumerable<DataRow> companies = from DataRow line in GeneralData.DT_COMPANY.Rows
                                             where newContact[Contact.CompanyIdentification.GetDescription()].ToString()! != string.Empty
                                             && line[Company.CompanyIdentification.GetDescription()].ToString()!
                                                    .Contains(newContact[Contact.CompanyIdentification.GetDescription()].ToString()!, StringComparison.InvariantCultureIgnoreCase)
                                             select line;


            if (companies != null && companies.Any()!)
            {
                newContact[Contact.CompanyIdentification.GetDescription()] = companies.FirstOrDefault()![Company.CompanyIdentification.GetDescription()].ToString()!;
                newContact[Contact.Status.GetDescription()] = companies.FirstOrDefault()![Company.Status.GetDescription()].ToString()!;
                newContact[Contact.ApprobedDate.GetDescription()] = CleanDataHelper.CleanDate(companies.FirstOrDefault()![Company.DateCreate.GetDescription()].ToString()!);
            }
            else
            {
                newContact[Contact.ApprobedDate.GetDescription()] = CleanDataHelper.CleanDate(newContact[Contact.ApprobedDate.GetDescription()].ToString()!);
            }
        }
    }
}
