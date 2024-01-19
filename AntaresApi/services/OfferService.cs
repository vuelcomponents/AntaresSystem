using AntaresApi.Dto.Common;
using AntaresApi.Dto.Offer;
using AntaresApi.Models;
using AntaresApi.Models.Document;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Position;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class OfferService : IOfferService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public OfferService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<OfferShortDto> GetAllOffers()
    {
        List<Offer> list = _context.Offers
             .Include(m=>m.Image)
             .Include(o=>o.Company)
             .Include(o=>o.Position)
            .ToList();
     
        return list.Select(_mapper.Map<OfferShortDto>).ToList();
    }

    public OfferShortDto GetOfferById(long id)
    {
        Offer? offer = _context.Offers
            .Include(m=>m.Image)
            .Include(o=>o.Company)
            .Include(o=>o.Position)
            .FirstOrDefault(c=> c.Id == id);
        if (offer == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<OfferShortDto>(offer);
    }

    public OfferDto CreateOffer(OfferDto offer)
    {
        if (offer.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Code must be fulfilled");
        }
        if (offer.Title.IsNullOrEmpty() || offer.Message.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Title and message cannot be empty");
        }
        if (offer.Company == null)
        {
            throw new BadHttpRequestException("Company has not been specified");
        }

        Company? company = _context.Companies.FirstOrDefault(c => c.Id == offer.Company.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Company #{offer.Company.Id} has not been found");
        }
        if (offer.Position == null)
        {
            throw new BadHttpRequestException("Position must be set");
        }

        Position? position = _context.Positions.FirstOrDefault(p => p.Id == offer.Position.Id);
        if (position == null)
        {
            throw new BadHttpRequestException($"Position #{offer.Position.Id} has not been found");
        }
        if (!(company.Positions !=null && company.Positions.Any(p => p.Id == position.Id)))
        {
            throw new BadHttpRequestException($"Position  #{position.Id} does not belong to company #{company.Id}");
        }
        Offer? dbOffer = new Offer
        {
            Code = offer.Code!,
            Title = offer.Title!,
            Message = offer.Message!,
            Global = offer.Global,
            Company = company,
            Position = position,
        };
        if (offer.Image != null)
        {
            if (!IsImageExtension(offer.Image!.FileName!))
            {
                throw new BadHttpRequestException("This is not an image!");
            }
            _Document? image = _context.Documents.FirstOrDefault(d => d.Id == offer.Image.Id);
            if (image == null)
            {
                throw new BadHttpRequestException("Image has been specified but not found");
            }

            dbOffer.Image = image;
        }
        _context.Offers.Add(dbOffer);
        _context.SaveChanges();
        return _mapper.Map<OfferDto>(dbOffer);
    }

    public OfferDto UpdateOffer(OfferDto offer)
    {
        Offer? dbOffer = _context.Offers.FirstOrDefault(m => m.Id == offer.Id);
        if (dbOffer == null)
        {
            throw new BadHttpRequestException($"Offer {offer.Id} has not been found");
        }

        if (!dbOffer.Title.IsNullOrEmpty())
        {
            dbOffer.Title = offer.Title!;
        }

        if (!dbOffer.Message.IsNullOrEmpty())
        {
            dbOffer.Message = offer.Message!;
        }

        if (!dbOffer.Code.IsNullOrEmpty())
        {
            dbOffer.Code = offer.Code!;
        }

        dbOffer.Global = offer.Global;
        if (offer.Company == null)
        {
            throw new BadHttpRequestException("Company has not been specified");
        }

        Company? company = _context.Companies.FirstOrDefault(c => c.Id == offer.Company.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Company #{offer.Company.Id} has not been found");
        }
        if (offer.Position == null)
        {
            throw new BadHttpRequestException("Position must be set");
        }

        Position? position = _context.Positions.FirstOrDefault(p => p.Id == offer.Position.Id);
        if (position == null)
        {
            throw new BadHttpRequestException($"Position #{offer.Position.Id} has not been found");
        }
        if (!(company.Positions !=null && company.Positions.Any(p => p.Id == position.Id)))
        {
            throw new BadHttpRequestException($"Position  #{position.Id} does not belong to company #{company.Id}");
        }
        if (offer.Image != null)
        {
            if (!IsImageExtension(offer.Image!.FileName!))
            {
                throw new BadHttpRequestException("This is not an image!");
            }
            _Document? image = _context.Documents.FirstOrDefault(d => d.Id == offer.Image.Id);
            if (image == null)
            {
                throw new BadHttpRequestException("Image has been specified but not found");
            }

            dbOffer.Image = image;
        }
        dbOffer.Company = company;
        dbOffer.Position = position;
        _context.SaveChangesAsync();
        return _mapper.Map<OfferDto>(dbOffer);
    }

    public List<IdDto> DeleteMultipleOffer(List<IdDto> offers)
    {
        if(offers.Count >0)
        {
            foreach (var offer in offers)
            {
                Offer? dbOffer = _context.Offers.Include(m=>m.Image).FirstOrDefault(e => e.Id == offer.Id);
                if (dbOffer != null)
                {
                    _context.Offers.Remove(dbOffer);
                }
            }

            _context.SaveChanges();
        }
        return offers;
    }
    
    private bool IsImageExtension(string fileName)
    {
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp"  };
        string extension = Path.GetExtension(fileName)?.ToLower();
        return !string.IsNullOrEmpty(extension) && imageExtensions.Contains(extension);
    }
}