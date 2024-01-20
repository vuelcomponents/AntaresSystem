using WebApplication1.Dto.Offer;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApplication1.Services;

public class OfferService : IOfferService
{

    private readonly RestClient _client = new RestClient(new RestClientOptions("http://localhost:7057/integration")
    {
        Authenticator = new HttpBasicAuthenticator("username", "password")
    });
    public async Task<List<OfferDto>> GetAllOffers()
    {
        var request = new RestRequest("offer");
        var response = await _client.GetAsync<List<OfferDto>>(request);
        if (response == null)
        {
            throw new BadHttpRequestException("Main Server Response is empty");
        }

        return response;
    }
}