using AntaresApi.Dto.Offer;

namespace AntaresApi.Integrations.Offer;

public interface IOfferIntegrationService
{
    Task<List<OfferShortDto>> GetAllOffers();
}