using Migration.Domain.Domain.Enums;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Migration.Domain.Domain.Helpers
{
    public static class CleanDataHelper
    {
        public static string CleanPersonType(string personTypeName)
        {
            if (string.IsNullOrEmpty(personTypeName))
            {
                personTypeName = "false";
            }

            bool naturalPerson = personTypeName.Contains("true", StringComparison.InvariantCultureIgnoreCase);

            personTypeName = naturalPerson ? GeneralData.NATURAL_PERSON : GeneralData.ENTITY_PERSON;

            return personTypeName;
        }

        public static string CleanPersonTypeByName(string personType)
        {
            if (string.IsNullOrEmpty(personType))
            {
                return GeneralData.ENTITY_PERSON;
            }

            if (personType.Contains("natura", StringComparison.InvariantCultureIgnoreCase))
            {
                return GeneralData.NATURAL_PERSON;
            }
            else
            {
                return GeneralData.ENTITY_PERSON;
            }
        }

        public static string CleanTelephoneNumber(string telephoneNumber)
        {
            if (string.IsNullOrEmpty(telephoneNumber))
            {
                telephoneNumber = GeneralData.GENERIC_TELEPHONE_NUMBER;
            }

            return telephoneNumber.ToUpperInvariant();
        }

        public static string CleanFullUserName(string email, string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                fullName = DefaultUserNameFromEmail(email);
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullName.ToLower());
        }

        public static string CleanUserNickName(string email, string userNickName)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(userNickName) || HasInvalidCharacter(userNickName))
            {
               

                var defaultUserName = DefaultUserNameFromEmail(email);

                email = email.Replace("_", "");

                string[] sufixEmail = email.Split("@");

                string emailPrefix = sufixEmail[1];

                string[] splitDot = emailPrefix.Split(".");

                string companyPrefix = TitleCaseHelper(splitDot[0]);

                string firstIdentity = TitleCaseHelper(email);

                Random random = new();

                // Genera un número aleatorio del 1 al 20
                int randomNumber = random.Next(1, 21);

                userNickName = string.Concat(firstIdentity, companyPrefix, randomNumber, GeneralData.DEFAULT_SUFIX_USER_NAME);


                if (userNickName.Length > GeneralData.MAX_CHARACTER_USER_NICK_NAME)
                {
                    userNickName = userNickName.Replace(GeneralData.DEFAULT_SUFIX_USER_NAME, string.Empty);
                }

            }

            userNickName = new string(userNickName.Normalize(NormalizationForm.FormD)
                                 .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                                 .ToArray());

            userNickName = RemoveInvalidCharacters(userNickName);

            if (userNickName.Length > GeneralData.MAX_CHARACTER_USER_NICK_NAME)
            {
                userNickName = userNickName.Substring(0, GeneralData.MAX_CHARACTER_USER_NICK_NAME);
            }

            return userNickName;
        }

        private static string TitleCaseHelper(string email)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(DefaultUserNameFromEmail(email).ToLower());
        }

        private static string RemoveInvalidCharacters(string input)
        {
            return Regex.Replace(input, GeneralData.REGEX_USER_IDENTITY.ToString(), string.Empty);
        }

        private static bool HasInvalidCharacter(string text)
        {
            return !GeneralData.REGEX_USER_IDENTITY.IsMatch(text);
        }

        public static string CleanCompanyIdentification(string companyIdentification)
        {
            if (companyIdentification.Contains("."))
            {
                companyIdentification = companyIdentification.Replace(".", string.Empty);
            }

            if (companyIdentification.Contains("-"))
            {
                companyIdentification = companyIdentification.Replace("-", string.Empty);
            }

            if (companyIdentification.Contains("_"))
            {
                companyIdentification = companyIdentification.Replace("_", string.Empty);
            }

            if (companyIdentification.Contains(" "))
            {
                companyIdentification = companyIdentification.Replace(" ", string.Empty);
            }

            return companyIdentification.Trim();
        }

        public static string CleanIdentificationType(string identificationType)
        {
            if (identificationType.Contains("extranje", StringComparison.InvariantCultureIgnoreCase))
            {
                identificationType = GeneralData.TYPE_CEDULA_EXTRANJERIA;
            }
            else if (identificationType.Contains("ciudada", StringComparison.InvariantCultureIgnoreCase))
            {
                identificationType = GeneralData.TYPE_CEDULA_CIUDADANIA;
            }
            else if (identificationType.Contains("pasapor", StringComparison.InvariantCultureIgnoreCase))
            {
                identificationType = GeneralData.TYPE_PASAPORTE;
            }
            else if (identificationType.Contains("nit", StringComparison.InvariantCultureIgnoreCase))
            {
                identificationType = GeneralData.NIT;
            }
            else
            {
                identificationType = GeneralData.TYPE_CEDULA_CIUDADANIA;
            }

            return identificationType;
        }

        public static string DefaultUserNameFromEmail(string email)
        {
            string[] partes = email.Split('@');
            return partes[0];
        }

        public static string DefaultUserRol(string userRol)
        {
            if (userRol.Contains("representante", StringComparison.InvariantCultureIgnoreCase))
            {
                userRol = GeneralData.PERSON_REPRESENTATIVE_ROL;
            }
            else if (string.IsNullOrEmpty(userRol))
            {
                userRol = GeneralData.DEFAULT_USER_ROL;
            }
            else
            {
                userRol = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userRol);
            }

            return userRol;
        }

        public static string CleanStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return GeneralData.BASIC_STATUS;
            }

            if (status.Contains("Pendien", StringComparison.InvariantCultureIgnoreCase))
            {
                status = StatusRegisterEnum.Pending.GetDescription();
            }
            else if (status.Contains("Aproba", StringComparison.InvariantCultureIgnoreCase))
            {
                status = StatusRegisterEnum.Approbed.GetDescription();
            }
            else if (status.Contains("rech", StringComparison.InvariantCultureIgnoreCase))
            {
                status = StatusRegisterEnum.Reject.GetDescription();
            }
            else if (status.Contains("revis", StringComparison.InvariantCultureIgnoreCase))
            {
                status = StatusRegisterEnum.Pending.GetDescription();
            }

            return status;
        }

        public static string CleanPersonTypeFromIdentificationType(string identificationType)
        {
            if (identificationType.Contains(GeneralData.NIT, StringComparison.InvariantCulture))
            {
                return GeneralData.ENTITY_PERSON;
            }
            else
            {
                return GeneralData.NATURAL_PERSON;
            }
        }

        public static string CleanDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return string.Empty;
            }

            DateTime parseTime;

            string stringTime = string.Empty;
            if (DateTime.TryParse(date, out parseTime))
            {
                return parseTime.ToString("yyyy-MM-dd h:m");
            }


            if (date.Contains("p", StringComparison.InvariantCultureIgnoreCase))
            {
                stringTime = date.Replace("P.ÓM.", "p. m.");
            }

            if (date.Contains("a", StringComparison.InvariantCultureIgnoreCase))
            {
                stringTime = date.Replace("A.ÓM.", "a. m.");
            }

            Regex expresion = new("\\d{1,2}\\/\\d{1,2}\\/\\d{4}\\s\\d{1,2}:\\d{2}:\\d{2}");

            bool evaluateFormat = expresion.IsMatch(date);


            if (evaluateFormat && string.IsNullOrEmpty(stringTime))
            {
                stringTime = DateTime.ParseExact(date, "M/d/yyyy H:mm:ss", CultureInfo.InvariantCulture).ToString();
            }

            return Convert.ToDateTime(stringTime).ToString("yyyy-MM-dd h:m");
        }
    }
}
