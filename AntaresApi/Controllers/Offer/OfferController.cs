using AntaresApi.Dto.Common;
using AntaresApi.Dto.Offer;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Offer;

[ApiController]
[Route("api/offer")]
public class OfferController : ControllerBase
{
    private readonly IOfferService _mailService;

    public OfferController(IOfferService mail)
    {
        _mailService = mail;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<OfferDto>> GetAllOffer()
    {
        try
        {
            return Ok(_mailService.GetAllOffers());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<OfferShortDto> GetOfferById(long id)
    {
        try
        {
            return Ok(_mailService.GetOfferById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    [HttpPost("create")]
    public ActionResult<OfferDto> CreateOffer(OfferDto offer)
    {
        try
        {
            return Ok(_mailService.CreateOffer(offer));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<OfferDto> UpdateOffer(OfferDto offer)
    {
        try
        {
            return Ok(_mailService.UpdateOffer(offer));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleOffer(List<IdDto> offers)
    {
        try
        {
            return Ok(_mailService.DeleteMultipleOffer(offers));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
