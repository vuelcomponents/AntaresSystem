using WebApplication1.Dto;

namespace WebApplication1.Services;

public interface IEmployeeService
{
    Task<EmployeeShortDto> Register(EmployeeShortDto employee);
}