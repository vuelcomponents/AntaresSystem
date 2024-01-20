using AntaresApi.Dto;

namespace AntaresApi.Integrations.Employee;

public interface IEmployeeIntegrationService
{
    Task<EmployeeShortDto> Register(EmployeeShortDto employee);
}