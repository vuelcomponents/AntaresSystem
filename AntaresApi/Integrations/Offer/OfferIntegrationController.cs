using AntaresApi.Dto.Offer;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Integrations.Offer;
[ApiController]
[Route("integration/offer")]
public class OfferIntegrationController : ControllerBase
{
    private readonly IOfferIntegrationService _offerIntegrationService;

    public OfferIntegrationController(IOfferIntegrationService offerIntegrationService)
    {
        _offerIntegrationService = offerIntegrationService;
    }
    [HttpGet]
    public async Task<ActionResult<List<OfferShortDto>>> GetAllOffers()
    {
        try
        {
            return Ok(await  _offerIntegrationService.GetAllOffers());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}