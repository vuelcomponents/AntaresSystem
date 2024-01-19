using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Models;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.StoreModel;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class VariantService : IVariantService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public VariantService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<VariantDto> GetAllVariant()
    {
        List<Variant> list = _context.Variants    
                            .Include(v=>v.VariantRealisations)
                            .Include(v=>v.VariantType)
                            .Include(v=>v.StoreModel)
                            .AsSplitQuery().ToList();
        return list.Select(_mapper.Map<VariantDto>).ToList();
    }

    public VariantDto GetVariantById(long id)
    {
        Variant? variant = _context.Variants
                            .Include(v=>v.VariantRealisations)
                            .Include(v=>v.VariantType)
                            .Include(v=>v.StoreModel)
                            .AsSplitQuery()
                            .FirstOrDefault(c=> c.Id == id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"No variant #{id} has been found");
        }
        return _mapper.Map<VariantDto>(variant);
    }

    public VariantDto CreateVariant(VariantDto variant)
    {
        if (variant.Code.IsNullOrEmpty() || variant.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (_context.Variants.ToList().Any(v => v.Code == variant.Code))
        {
            throw new BadHttpRequestException("Variant with this Code already exist");
        }
        if (variant.VariantType == null)
        {
            throw new BadHttpRequestException("Variant type must be specified");
        }

        if (variant.StoreModel == null)
        {
            throw new BadHttpRequestException("Destiny model has not been specified");
        }

        StoreModel? storeModel = _context.StoreModels.FirstOrDefault(s => s.Id == variant.StoreModel.Id);
        if (storeModel == null)
        {
            throw new BadHttpRequestException($"Model #{variant.StoreModel.Id} does not exist");
        }

        VariantType? variantType = _context.VariantTypes.FirstOrDefault(vt => vt.Id == variant.VariantType.Id);
        if (variantType == null)
        {
            throw new BadHttpRequestException($"Variant #{variant.VariantType.Id} does not exist");
        }
        try
        {
            Variant dbVariant = new Variant
            {

                Code = variant.Code!,
                Description = variant.Description!,
                VariantType = variantType,
                StoreModel = storeModel,
                Global = variant.Global
                
            };
            _context.Variants.Add(dbVariant);
            _context.SaveChanges();
            return _mapper.Map<VariantDto>(dbVariant);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public VariantDto UpdateVariant(VariantDto variant)
    {
        if (variant.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (variant.Code.IsNullOrEmpty() || variant.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        Variant? dbVariant = _context.Variants.FirstOrDefault(e => e.Id.Equals(variant.Id));
        if (dbVariant is null)
        {
            throw new BadHttpRequestException($"There is no employee #{variant.Id}");
        }
        if (_context.Variants.ToList().Any(v => v.Code == variant.Code && v.Code != dbVariant.Code))
        {
            throw new BadHttpRequestException("Variant with this Code already exist");
        }
        dbVariant.Code = variant.Code!;
        dbVariant.Description = variant.Description!;
        dbVariant.Global = variant.Global;

        _context.SaveChanges();
        return _mapper.Map<VariantDto>(dbVariant);
    }

    public VariantDto DeleteVariant(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleVariant(List<IdDto> variants)
    {
        if(variants.Count >0)
        {
            foreach (var com in variants)
            {
                Variant? variant = _context.Variants.Include(v=>v.VariantRealisations).FirstOrDefault(c => c.Id == com.Id);
                if (variant != null)
                {
                    // if (variant.VariantRealisations is { Count: > 0 })
                    // {
                    //     variant.VariantRealisations.RemoveAll(c => true);
                    // }
                    _context.Variants.Remove(variant);
                }
            }

            _context.SaveChanges();
        }
        return variants;
    }

    public VariantDto ColManAddToRecruitment(VariantDto variant, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Recruitment ID has not been specified");
        }
        var dbRecruitment = _context.Recruitments
            .Include(e=>e.Variants)
            .Include(r=>r.StoreModel)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (dbRecruitment == null)
        {
            throw new BadHttpRequestException($"Recruitment #{owner.Id} doest not exist");
        }

        Variant? dbVariant = _context.Variants.Include(s=>s.StoreModel).FirstOrDefault(v => v.Id == variant.Id);
        if (dbVariant == null)
        {
            throw new BadHttpRequestException("Variant has not been found");
        }

        if (dbVariant.StoreModel!.Id != dbRecruitment.StoreModel.Id)
        {
            throw new BadHttpRequestException(
                $"This variant is not destined to store model {dbRecruitment.StoreModel.Code}");
        }

        if (dbVariant.Global != true)
        {
            throw new BadHttpRequestException("This variant is not global");
        }

        if (dbRecruitment.Variants!.Any(v => v.Id == dbVariant.Id))
        {
            throw new BadHttpRequestException($"Recruitment has already signed variant #{variant.Id}");
        }
        dbRecruitment.Variants!.Add(dbVariant);
        _context.SaveChanges();
        return _mapper.Map<VariantDto>(dbVariant);
    }
    public List<IdDto> ColManDeleteFromRecruitment(List<IdDto> collection, IdDto owner)
    {
        Recruitment? recruitment = _context.Recruitments
            .Include(e=>e.Variants)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (recruitment == null)
        {
            throw new BadHttpRequestException($"Invalid recruitment #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Variant? variant = _context.Variants.FirstOrDefault(c => c.Id == com.Id);
                if (variant != null && recruitment.Variants is {Count:>0})
                {
                    recruitment.Variants.Remove(variant);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    
}