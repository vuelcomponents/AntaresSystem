using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto.Offer;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/offer")]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OfferDto>>> GetAllOffers()
    {
        try
        {
            return Ok(await _offerService.GetAllOffers());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}