using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.StatusAction;


[ApiController]
[Route("api/action-function")]
public class ActionFunctionController : ControllerBase
{
private readonly IActionFunctionService _actionFunctionService;

public ActionFunctionController(IActionFunctionService actionFunctionService)
{
    _actionFunctionService = actionFunctionService;
}
[HttpGet("get")]
public ActionResult<IEnumerable<ActionFunctionDto>> GetAllActionFunction()
{
    try
    {
        return Ok(_actionFunctionService.GetAllActionFunctions());
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
[HttpGet("get/{id}")]
public ActionResult<ActionFunctionDto> GetActionFunctionById(long id)
{
    try
    {
        return Ok(_actionFunctionService.GetActionFunctionById(id));
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
} 
[HttpPost("create")]
public ActionResult<ActionFunctionDto> CreateCompany(ActionFunctionDto actionFunction)
{
    try
    {
        return Ok(_actionFunctionService.CreateActionFunction(actionFunction));
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
[HttpPatch("update")]
public ActionResult<ActionFunctionDto> UpdateCompany(ActionFunctionDto actionFunction)
{
    try
    {
        return Ok(_actionFunctionService.UpdateActionFunction(actionFunction));
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
[HttpPost("delete-multiple")]
public ActionResult<List<IdDto>> DeleteMultipleCompany(List<IdDto> actionFunctions)
{
    try
    {
        return Ok(_actionFunctionService.DeleteMultipleActionFunction(actionFunctions));
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
}
