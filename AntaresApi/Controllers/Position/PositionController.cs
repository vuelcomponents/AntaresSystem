using AntaresApi.Dto.Common;
using AntaresApi.Dto.Position;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Position;

[ApiController]
[Route("api/position")]
public class PositionController : ControllerBase
{
    private readonly IPositionService _positionService;
    
    public PositionController(IPositionService positionService)
    {
        _positionService = positionService;
    }
    
    [HttpGet("get")]
    public ActionResult<IEnumerable<PositionLiteDto>> GetAllPosition()
    {
        try
        {
            return Ok(_positionService.GetAllPosition());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<PositionDto> GetPositionById(long id)
    {
        try
        {
            return Ok(_positionService.GetPositionById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<PositionDto> CreatePosition(PositionDto position)
    {
        try
        {
            return Ok(_positionService.CreatePosition(position));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<PositionDto> UpdatePosition(PositionDto position)
    {
        try
        {
            return Ok(_positionService.UpdatePosition(position));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultiplePosition(List<IdDto> positions)
    {
        try
        {
            return Ok(_positionService.DeleteMultiplePosition(positions));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleEmployee(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "employee":
                    return Ok(_positionService.ColDeleteMultipleFromEmployee(collection.Collection, collection.Owner));
                default:
                    return DeleteMultiplePosition(collection.Collection);
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
      
        
    }
    [HttpPost("create-colman")]
    public ActionResult<PositionDto> ColManCreateCompany(ColPositionDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "employee":
                    return Ok(_positionService.ColManAddToEmployee(single.Object, single.Owner));
                case "company":
                    return Ok(_positionService.ColManAddToCompany(single.Object, single.Owner));
            }

            return BadRequest("The method or operation is not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("move-move")]
    public ActionResult<PositionDto> MovePositionToChildrenOfPosition(PositionMoveDto positionMove)
    {
        try
        {
            return Ok(_positionService.MovePositionToChildrenOfPosition(positionMove.PositionFrom,positionMove.PositionTo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}