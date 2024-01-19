using AntaresApi.Dto;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.SystemFunction;


[ApiController]
[Route("api/system-function")]
public class SystemFunctionController : ControllerBase
{
    private readonly ISystemFunctionService _systemFunctionService;

    public SystemFunctionController(ISystemFunctionService systemFunctionService)
    {
        _systemFunctionService = systemFunctionService;
    }

    [HttpGet("get")]
    public ActionResult<IEnumerable<SystemFunctionDto>> GetAllSystemFunction()
    {
        try
        {
            return Ok(_systemFunctionService.GetAllSystemFunctions());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get/{id}")]
    public ActionResult<SystemFunctionDto> GetSystemFunctionById(long id)
    {
        try
        {
            return Ok(_systemFunctionService.GetSystemFunctionById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}