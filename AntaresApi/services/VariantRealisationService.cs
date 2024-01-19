using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.VariantRealisation;
using AntaresApi.Models;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class VariantRealisationService : IVariantRealisationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public VariantRealisationService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<VariantRealisationDto> GetAllVariantRealisation()
    {
        List<VariantRealisation> list = _context.VariantRealisations
            .Include(vr=>vr.Variant)
            .ToList();
        return list.Select(_mapper.Map<VariantRealisationDto>).ToList();
    }

    public IEnumerable<VariantRealisationDto> GetAllVariantRealisationByVariantId(long variantId)
    {
        List<VariantRealisation> list = _context.VariantRealisations
            .Include(vr=>vr.Variant)
            .Where(w=>w.Variant!.Id == variantId)
            .ToList();
        return list.Select(_mapper.Map<VariantRealisationDto>).ToList();
    }
    public VariantRealisationDto GetVariantRealisationById(long id)
    {
        VariantRealisation? variantRealisation = _context.VariantRealisations
            .Include(vr=>vr.Variant)                
            .FirstOrDefault(c=> c.Id == id);
        if (variantRealisation == null)
        {
            throw new BadHttpRequestException($"No variant #{id} has been found");
        }
        return _mapper.Map<VariantRealisationDto>(variantRealisation);
    }
    
    public VariantRealisationDto CreateVariantRealisation(VariantRealisationDto variantRealisation)
    {
        if (variantRealisation.Code.IsNullOrEmpty() || variantRealisation.Description.IsNullOrEmpty() )
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        if (variantRealisation.Variant == null)
        {
            throw new BadHttpRequestException("Variant has not been specified");
        }

        var variant = _context.Variants
            .Include(v=>v.VariantRealisations)    
            .FirstOrDefault(v => v.Id == variantRealisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant {variantRealisation.Variant.Id} does not exist");
        }

        if (variantRealisation.Code == null)
        {
            throw new BadHttpRequestException("No code provided");
        }

        var vrCheck = variant.VariantRealisations!.Any(vr => vr.Code == variantRealisation.Code);
        if (vrCheck)
        {
            throw new BadHttpRequestException($"Variant realisation '{variantRealisation.Code}' already exists in Variant {variant.Code} ");
        }
        try
        {
            VariantRealisation dbVariantRealisation = new VariantRealisation
            {

                Code = variantRealisation.Code!,
                Description = variantRealisation.Description!,
                Variant = variant
            };
            _context.VariantRealisations.Add(dbVariantRealisation);
            _context.SaveChanges();
            return _mapper.Map<VariantRealisationDto>(dbVariantRealisation);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public VariantRealisationDto UpdateVariantRealisation(VariantRealisationDto variantRealisation)
    {
        if (variantRealisation.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (variantRealisation.Code.IsNullOrEmpty() || variantRealisation.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        VariantRealisation? dbVariantRealisation = _context.VariantRealisations.FirstOrDefault(e => e.Id.Equals(variantRealisation.Id));
        if (dbVariantRealisation is null)
        {
            throw new BadHttpRequestException($"There is no employee #{variantRealisation.Id}");
        }

        dbVariantRealisation.Code = variantRealisation.Code!;
        dbVariantRealisation.Description = variantRealisation.Description!;


        _context.SaveChanges();
        return _mapper.Map<VariantRealisationDto>(dbVariantRealisation);
    }

    public VariantRealisationDto DeleteVariantRealisation(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleVariantRealisation(List<IdDto> variantRealisations)
    {
        if(variantRealisations.Count >0)
        {
            foreach (var com in variantRealisations)
            {
                VariantRealisation? variantRealisation = _context.VariantRealisations.FirstOrDefault(c => c.Id == com.Id);
                if (variantRealisation != null)
                {
                    _context.VariantRealisations.Remove(variantRealisation);
                }
            }

            _context.SaveChanges();
        }
        return variantRealisations;
    }   
}