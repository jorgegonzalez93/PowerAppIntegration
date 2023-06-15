using Migration.Domain.Domain.DTOs;

namespace Migration.Domain.Infrastructure.Adapters
{
    public class AgentService
    {
        private const string OCPSUBSCRIPTIONKEY = "4a58f45213a9485d8708506e0a071c7c";
        private const string MIGRATIONEMPLOYEE_PATH = "http://localhost:5000/api/Employees/migrationEmployee";
        private const string REGISTEREDUSERS_PATH = "http://localhost:5000/api/Users/registeredUsers";
        private const string COMPANYDATA_PATH = "http://localhost:5000/api/CompanyData";
        private const string VALIDATEEXISTENCE_PATH = "http://localhost:5000/api/CompanyData/ValidateExistence";
        private const string COMPANY_INFORMATION_BY_IDENTIFICATION = "http://localhost:5000/api/CompanyData/companyInformationByIdentification";

        readonly GenericHttpRequest GenericHttpRequest;
        public AgentService()
        {
            GenericHttpRequest = new();
        }
        public async Task<EmployeeDto> RegisterEmployeeAsync(EmployeeMigrationDto userMigration)
        {
            EmployeeMigrationCommand commandQuery = new(userMigration);

            return await GenericHttpRequest.PostDataAsync<EmployeeDto>($"{MIGRATIONEMPLOYEE_PATH}", commandQuery, OCPSUBSCRIPTIONKEY);
        }

        public async Task<PagedListUsersDto> QueryUser(EmployeeMigrationDto employee)
        {
            UserFilter filter = new()
            {
                PageNumber = -1,
                PageSize = -1,
                FilterTypeDateTime = 0,
                ModifDateTime = null,
                ModifDateTimeEnd = null,
                Search = employee.Email
            };

            return await GenericHttpRequest.PostDataAsync<PagedListUsersDto>($"{REGISTEREDUSERS_PATH}", filter, OCPSUBSCRIPTIONKEY);
        }

        public async Task<CompanyDataDto> CompanyPostAsync(CompanyDataCreateCommand userMigration)
        {
            return await GenericHttpRequest.PostDataAsync<CompanyDataDto>($"{COMPANYDATA_PATH}", userMigration, OCPSUBSCRIPTIONKEY);
        }

        public async Task<CompanyInformationDto> CompanyInformationAsync(string companyIdentification)
        {
            string pathRequest = $"{COMPANY_INFORMATION_BY_IDENTIFICATION}?CompanyIdentification={companyIdentification}";

            return await GenericHttpRequest.GetDataAsync<CompanyInformationDto>($"{pathRequest}", OCPSUBSCRIPTIONKEY);
        }
    }
}
