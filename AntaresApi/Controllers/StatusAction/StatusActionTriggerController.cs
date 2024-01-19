using AntaresApi.Dto.Action;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.StatusAction;

[ApiController]
[Route("api/status-action-trigger")]
public class StatusActionTriggerController : ControllerBase
{
    private readonly IStatusActionTriggerService _statusActionTriggerService;
    
    public StatusActionTriggerController(IStatusActionTriggerService statusActionTriggerService)
    {
        _statusActionTriggerService = statusActionTriggerService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<StatusActionTriggerDto>> GetAllStatusActionTrigger()
    {
        try
        {
            return Ok(_statusActionTriggerService.GetAllStatusActionTriggers());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<StatusActionTriggerDto> GetStatusActionTriggerById(long id)
    {
        try
        {
            return Ok(_statusActionTriggerService.GetStatusActionTriggerById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
}