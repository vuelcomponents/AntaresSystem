using AntaresApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Integrations.Employee;
[ApiController]
[Route("integration/employee")]
public class EmployeeIntegrationController : ControllerBase
{
    private readonly IEmployeeIntegrationService _employeeIntegrationService;

    public EmployeeIntegrationController(IEmployeeIntegrationService employeeIntegrationService)
    {
        _employeeIntegrationService = employeeIntegrationService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<EmployeeShortDto>> Register(EmployeeShortDto employee)
    {
        try
        {
            return Ok(await _employeeIntegrationService.Register(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
}