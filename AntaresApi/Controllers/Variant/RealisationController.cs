using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Variant;

[ApiController]
[Route("api/realisation")]
public class RealisationController : ControllerBase
{
      private readonly IRealisationService _realisationService;
    
    public RealisationController(IRealisationService variantRealisationService)
    {
        _realisationService = variantRealisationService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<RealisationDto>> GetAllRealisation()
    {
        try
        {
            return Ok(_realisationService.GetAllRealisation());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<RealisationDto> GetRealisationById(long id)
    {
        try
        {
            return Ok(_realisationService.GetRealisationById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<RealisationDto> CreateRealisation(RealisationDto variant)
    {
        try
        {
            return Ok(_realisationService.CreateRealisation(variant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<RealisationDto> UpdateRealisation(RealisationDto variant)
    {
        try
        {
            return Ok(_realisationService.UpdateRealisation(variant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<RealisationDto> DeleteRealisation(long id)
    {
        try
        {
            return Ok(_realisationService.DeleteRealisation(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleRealisation(List<IdDto> realisations)
    {
        try
        {
            return Ok(_realisationService.DeleteMultipleRealisation(realisations));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create-colman")]
    public ActionResult<RealisationDto> ColManCreateCompany(ColRealisationDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "employee":
                    return Ok(_realisationService.CreateRealisationToEmployee(single.Object, single.Owner));
                case "company":
                    return Ok(_realisationService.CreateRealisationToCompany(single.Object, single.Owner));
                case "document":
                    return Ok(_realisationService.CreateRealisationToDocument(single.Object, single.Owner));
                case "actionFunction":
                    return Ok(_realisationService.CreateRealisationToActionFunction(single.Object, single.Owner));
                case "position":
                    return Ok(_realisationService.CreateRequirementToPosition(single.Object, single.Owner));
                case "house":
                    return Ok(_realisationService.CreateRequirementToHouse(single.Object, single.Owner));
                case "car":
                    return Ok(_realisationService.CreateRealisationToCar(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleEmployee(ColIDCollectionDto collection)
    {
            return DeleteMultipleRealisation(collection.Collection);
    }
}