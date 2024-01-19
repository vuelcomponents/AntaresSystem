using AntaresApi.Dto.Common;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Position;

[ApiController]
[Route("api/position-unit")]
public class PositionUnitController : ControllerBase
{
    private readonly IPositionUnitService _positionUnitService;
    
    public PositionUnitController(IPositionUnitService positionUnitService)
    {
        _positionUnitService = positionUnitService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<IdCodeDto>> GetAllPositionUnit()
    {
        try
        {
            return Ok(_positionUnitService.GetAllPositionUnit());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<IdCodeDto> GetPositionUnitById(long id)
    {
        try
        {
            return Ok(_positionUnitService.GetPositionUnitById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}