

using AntaresApi.Dto;
using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.StatusAction;

[ApiController]
[Route("api/status-action")]
public class StatusActionController : ControllerBase
{
    private readonly IStatusActionService _statusActionService;
    
    public StatusActionController(IStatusActionService statusActionService)
    {
        _statusActionService = statusActionService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<StatusActionDto>> GetAllStatusAction()
    {
        try
        {
            return Ok(_statusActionService.GetAllStatusAction());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<StatusActionDto> GetStatusActionById(long id)
    {
        try
        {
            return Ok(_statusActionService.GetStatusActionById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<StatusActionDto> CreateStatusAction(StatusActionDto statusAction)
    {
        try
        {
            return Ok(_statusActionService.CreateStatusAction(statusAction));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<StatusActionDto> UpdateStatusAction(StatusActionDto statusAction)
    {
        try
        {
            return Ok(_statusActionService.UpdateStatusAction(statusAction));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<StatusActionDto> DeleteStatusAction(long id)
    {
        try
        {
            return Ok(_statusActionService.DeleteStatusAction(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleStatusAction(List<IdDto> companies)
    {
        try
        {
            return Ok(_statusActionService.DeleteMultipleStatusAction(companies));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /** FUNKCJE TYLKO DLA MODELI BEDACYCH W CZYJEJS KOLEKCJII **/
    [HttpPost("create-colman")]
    public ActionResult<StatusActionDto> ColManCreateStatusAction(ColStatusActionDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "status":
                    return Ok(_statusActionService.ColManAddToStatus(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultiple(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "status":
                    return Ok(_statusActionService.ColDeleteMultipleStatusActionFromStatus(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}