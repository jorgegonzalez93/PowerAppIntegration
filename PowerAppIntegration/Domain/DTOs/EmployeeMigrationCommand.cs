using MediatR;

namespace Migration.Domain.Domain.DTOs
{
    public record EmployeeMigrationCommand(EmployeeMigrationDto employeeMigration) : IRequest<EmployeeDto>;
}
