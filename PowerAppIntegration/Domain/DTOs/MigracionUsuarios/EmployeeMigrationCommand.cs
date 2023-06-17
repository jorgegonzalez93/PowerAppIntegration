using MediatR;

namespace Migration.Domain.Domain.DTOs.MigracionUsuarios
{
    public record EmployeeMigrationCommand(EmployeeMigrationDto employeeMigration) : IRequest<EmployeeDto>;
}
