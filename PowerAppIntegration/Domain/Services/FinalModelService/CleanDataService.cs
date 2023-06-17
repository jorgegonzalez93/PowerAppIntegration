using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;
using System.Globalization;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class CleanDataService
    {
        public static void CleanDataRowContact(DataRow newContact)
        {
            newContact[Contact.Email.GetDescription()] = newContact[Contact.Email.GetDescription()]
                .ToString()!
                .ToLowerInvariant();

            newContact[Contact.UserName.GetDescription()] = newContact[Contact.UserName.GetDescription()]
                .ToString()!
                .ToLower();

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

            newContact[Contact.JobTitle.GetDescription()] = CleanDataHelper.DefaultUserRol(newContact.GetValueData(Contact.JobTitle.GetDescription()));

            newContact[Contact.IdentificationType.GetDescription()] = CleanDataHelper.CleanIdentificationType(newContact.GetValueData(Contact.IdentificationType.GetDescription()));

            newContact[Contact.UserName.GetDescription()] =
                CleanDataHelper.CleanFullUserName(newContact.GetValueData(Contact.Email.GetDescription()), newContact.GetValueData(Contact.UserName.GetDescription()));

            newContact[Contact.Telephone.GetDescription()] = CleanDataHelper.CleanTelephoneNumber(newContact.GetValueData(Contact.Telephone.GetDescription()));              
        }
    }
}
