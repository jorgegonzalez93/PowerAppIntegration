using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class CreateCompanyService
    {
        public static CompanyDataCreateCommand CreateCompanyObject(DataRow company)
        {
            string companyName = company[Company.CompanyName.GetDescription()].ToString()!;
            string companyIdentification = company[Company.CompanyIdentification.GetDescription()].ToString()!;
            string companyTelephone = company[Company.CompanyTelephone.GetDescription()].ToString()!;
            string companyAddress = company[Company.CompanyAddress.GetDescription()].ToString()!;
            string CountryName = company[Company.CountryName.GetDescription()].ToString()!;
            string companyType;

            string companyStatus = CleanDataHelper.CleanStatus(company[Company.Status.GetDescription()].ToString()!);
            string dateCreateCompany = company[Company.DateCreate.GetDescription()].ToString()!;

            string documentType = company[Company.DocumentType.GetDescription()].ToString()!;

            companyType = GenerateCompanyType(companyName);

            companyTelephone = CleanDataHelper.CleanTelephoneNumber(companyTelephone);

            string companyAbbreviation = GenerateCompanyAbbreviation(companyName);

            string personType;
            if (!documentType.Contains("NIT", StringComparison.InvariantCultureIgnoreCase))
            {
                personType = GeneralData.NATURAL_PERSON;
            }
            else
            {
                personType = GeneralData.ENTITY_PERSON;
            }

            return new(
                CompanyTypeId: companyType,
                CountryId: CountryName,
                CompanyIdentification: companyIdentification,
                CompanyName: companyName,
                CompanyTelephone: companyTelephone,
                CompanyAddress: companyAddress,
                SarlaftDocument: string.Empty,
                companyAbreviation: companyAbbreviation,
                personType: personType,
                dateCreate: dateCreateCompany,
                status: companyStatus,
                documentType: documentType);
        }

        private static string GenerateCompanyType(string companyName)
        {
            string companyType;
            if (companyName.Contains("E.S.P") || companyName.Contains("ESP"))
            {
                companyType = GeneralData.PUBLIC_COMPANY;
            }
            else
            {
                companyType = GeneralData.PRIVATE_COMPANY;
            }

            return companyType;
        }

        private static string GenerateCompanyAbbreviation(string company_name)
        {
            string[] words = company_name.Split(' ');
            string abbreviation = string.Empty;

            foreach (string word in words)
            {
                abbreviation += word.Substring(0, 1);
            }

            return abbreviation.ToUpper().Replace(".", "");
        }
    }
}
