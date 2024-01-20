using AntaresApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Integrations.Employee;

public class EmployeeIntegrationService : IEmployeeIntegrationService
{
    public async Task<EmployeeShortDto> Register(EmployeeShortDto employee)
    {
        // registration logic
        return new EmployeeShortDto
        {
            Id = 12,
            FirstName = "MAMRAAD"
        };
    }
}