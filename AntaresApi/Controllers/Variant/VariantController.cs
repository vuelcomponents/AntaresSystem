using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Variant;

[ApiController]
[Route("api/variant")]
public class VariantController : ControllerBase
{
    private readonly IVariantService _variantService;
    
    public VariantController(IVariantService variantService)
    {
        _variantService = variantService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<VariantDto>> GetAllVariant()
    {
        try
        {
            return Ok(_variantService.GetAllVariant());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<VariantDto> GetVariantById(long id)
    {
        try
        {
            return Ok(_variantService.GetVariantById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<VariantDto> CreateVariant(VariantDto variant)
    {
        try
        {
            return Ok(_variantService.CreateVariant(variant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<VariantDto> UpdateVariant(VariantDto variant)
    {
        try
        {
            return Ok(_variantService.UpdateVariant(variant));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<VariantDto> DeleteVariant(long id)
    {
        try
        {
            return Ok(_variantService.DeleteVariant(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleVariant(List<IdDto> variants)
    {
        try
        {
            return Ok(_variantService.DeleteMultipleVariant(variants));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create-colman")]
    public ActionResult<VariantDto> ColManCreateVariant(ColVariantDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "recruitment":
                    return Ok(_variantService.ColManAddToRecruitment(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleDocument(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "recruitment":
                    return Ok(_variantService.ColManDeleteFromRecruitment(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}