using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Status;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Status;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<StatusDto>> GetAllStatus()
    {
        try
        {
            return Ok(_statusService.GetAllStatus());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<StatusDto> GetStatusById(long id)
    {
        try
        {
            return Ok(_statusService.GetStatusById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<StatusDto> CreateStatus(StatusDto company)
    {
        try
        {
            return Ok(_statusService.CreateStatus(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<StatusDto> UpdateStatus(StatusDto company)
    {
        try
        {
            return Ok(_statusService.UpdateStatus(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<StatusDto> DeleteStatus(long id)
    {
        try
        {
            return Ok(_statusService.DeleteStatus(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleStatus(List<IdDto> employees)
    {
        try
        {
            return Ok(_statusService.DeleteMultipleStatuses(employees));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create-colman")]
    public ActionResult<StatusDto> ColManCreateEmployee(ColStatusDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "status":
                    return Ok(_statusService.ColManAddToStatus(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}