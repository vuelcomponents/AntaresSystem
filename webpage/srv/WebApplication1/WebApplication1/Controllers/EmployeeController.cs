using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Services;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<EmployeeShortDto>> Login(EmployeeShortDto employee)
    {
        try
        {
            return Ok(await _employeeService.Register(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}