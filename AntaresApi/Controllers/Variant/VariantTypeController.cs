using AntaresApi.Dto.Variant.VariantType;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Variant;

[ApiController]
[Route("api/variant-type")]
public class VariantTypeController : ControllerBase
{
    private readonly IVariantTypeService _variantService;
    
    public VariantTypeController(IVariantTypeService variantService)
    {
        _variantService = variantService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<VariantTypeDto>> GetAllVariant()
    {
        try
        {
            return Ok(_variantService.GetAllVariantTypes());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<VariantTypeDto> GetVariantById(long id)
    {
        try
        {
            return Ok(_variantService.GetVariantTypeById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}