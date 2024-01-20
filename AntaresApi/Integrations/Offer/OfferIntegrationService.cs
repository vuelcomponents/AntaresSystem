using AntaresApi.Dto.Offer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AntaresApi.Integrations.Offer;

public class OfferIntegrationService : IOfferIntegrationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public OfferIntegrationService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<OfferShortDto>> GetAllOffers()
    {
        return _context.Offers.Where(o=>o.Recruitment != null && o.Recruitment.Open).Select(_mapper.Map<OfferShortDto>).ToList();
    }
}

