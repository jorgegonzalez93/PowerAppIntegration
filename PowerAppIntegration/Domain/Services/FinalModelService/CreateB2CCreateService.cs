using Migration.Domain.Domain.DTOs.MigracionUsuarios;
using System.Data;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class CreateB2CCreateService
    {
        public static B2CDataUser? CreateB2CObject(DataRow contact)
        {
            EmployeeMigrationDto employee = CreateEmployeeService.CreateEmployeeObject(contact, true);

            return new()
            {
                username = employee.UserName!,
                displayName = employee.FullName,
                mail = employee.Email,
                telephone = employee.Telephone
            };
        }
    }
}
