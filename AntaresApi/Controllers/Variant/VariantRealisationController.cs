using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Dto.Variant.VariantRealisation;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Variant;

[ApiController]
[Route("api/variant-realisation")]
public class VariantRealisationController : ControllerBase
{
    private readonly IVariantRealisationService _variantRealisationService;
    
    public VariantRealisationController(IVariantRealisationService variantRealisationService)
    {
        _variantRealisationService = variantRealisationService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<VariantRealisationDto>> GetAllVariantRealisation()
    {
        try
        {
            return Ok(_variantRealisationService.GetAllVariantRealisation());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<VariantRealisationDto> GetVariantRealisationById(long id)
    {
        try
        {
            return Ok(_variantRealisationService.GetVariantRealisationById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<VariantRealisationDto> CreateVariantRealisation(VariantRealisationDto variantRealisation)
    {
        try
        {
            return Ok(_variantRealisationService.CreateVariantRealisation(variantRealisation));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<VariantRealisationDto> UpdateVariantRealisation(VariantRealisationDto variant)
    {
        try
        {
            return Ok(_variantRealisationService.UpdateVariantRealisation(variant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<VariantRealisationDto> DeleteVariantRealisation(long id)
    {
        try
        {
            return Ok(_variantRealisationService.DeleteVariantRealisation(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleVariantRealisation(List<IdDto> variantRealisations)
    {
        try
        {
            return Ok(_variantRealisationService.DeleteMultipleVariantRealisation(variantRealisations));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /** FUNKCJE TYLKO DLA MODELI BEDACYCH W CZYJEJS KOLEKCJII **/
    [HttpPost("create-colman")]
    public ActionResult<RealisationDto> ColManCreateVariantRealisation(ColVariantRealisationDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "variant":
                    return Ok(_variantRealisationService.CreateVariantRealisation(single.Object));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleVariantRealisation(ColIDCollectionDto collection)
    {
        return DeleteMultipleVariantRealisation(collection.Collection);
    }
}