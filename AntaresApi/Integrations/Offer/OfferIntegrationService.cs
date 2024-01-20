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
        var offers =  _context.Offers.Where(o=>o.Recruitment != null && o.Recruitment.Open).ToList();
        var dtos = offers.Select(_mapper.Map<OfferShortDto>).ToList();
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].Image!.FileData = Convert.ToBase64String(offers[i].Image!.FileData!);
        }

        return dtos;
    }
}

