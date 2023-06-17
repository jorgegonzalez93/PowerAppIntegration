using Migration.Domain.Domain.DTOs.MigracionUsuarios;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Infrastructure.Logs;
using System.Data;

namespace Migration.Domain.Domain.Services
{
    public class ValidationMigrationService
    {
        public string File { get; set; }
        public IEnumerable<Fileld> ValidateFilelds { get; set; }


        public ValidationMigrationService EmployeeRequired()
        {
            List<Fileld> fields = new()
            {
                new Fileld { Required = Enums.Contact.Email.GetDescription()},
                new Fileld { Required = Enums.Contact.CompanyIdentification.GetDescription()},
            };

            return new()
            {
                File = "Employee",
                ValidateFilelds = fields
            };
        }

        public ValidationMigrationService B2CRequired()
        {
            List<Fileld> fields = new()
            {
                new Fileld { Required = Enums.Contact.Email.GetDescription()}
            };

            return new()
            {
                File = "b2c",
                ValidateFilelds = fields
            };
        }

        public bool ProspectiveData(DataRow dataRow, ValidationMigrationService requiredFields)
        {
            bool createRegister = true;

            foreach (Fileld field in requiredFields.ValidateFilelds)
            {
                string? valueField = dataRow[field.Required].ToString();

                if (!string.IsNullOrEmpty(valueField))
                {
                    field.Check = true;
                }
            }

            IEnumerable<Fileld> validateFields = from Fileld field in requiredFields.ValidateFilelds
                                                 where field.Check is false
                                                 select field;

            if (validateFields.Any(validate => validate.Check is false))
            {
                createRegister = false;

                ApplicationLogService.LogReport(dataRow, validateFields, requiredFields.File);
            }

            return createRegister;
        }

    }
}
