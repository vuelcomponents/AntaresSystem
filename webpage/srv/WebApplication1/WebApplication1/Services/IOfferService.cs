using WebApplication1.Dto.Offer;

namespace WebApplication1.Services;

public interface IOfferService
{
    public Task<List<OfferDto>> GetAllOffers();
}