using AntaresApi.Dto.Common;
using AntaresApi.Dto.Offer;

namespace AntaresApi.Services.Interfaces;

public interface IOfferService
{
    IEnumerable<OfferShortDto> GetAllOffers();
    OfferShortDto GetOfferById(long id);
    OfferDto CreateOffer(OfferDto offer);
    OfferDto UpdateOffer(OfferDto offer);
    List<IdDto> DeleteMultipleOffer(List<IdDto> offers);
}