using AntaresApi.Dto.Variant.VariantType;
using AntaresApi.Models;
using AntaresApi.Services.Interfaces;
using AutoMapper;

namespace AntaresApi.Services;

public class VariantTypeService : IVariantTypeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public VariantTypeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<VariantTypeDto> GetAllVariantTypes()
    {
        List<VariantType> list = _context.VariantTypes   
            .ToList();
        return list.Select(_mapper.Map<VariantTypeDto>).ToList();
    }

    public VariantTypeDto GetVariantTypeById(long id)
    {
        VariantType? variantType = _context.VariantTypes
            .FirstOrDefault(c=> c.Id == id);
        if (variantType == null)
        {
            throw new BadHttpRequestException($"No variant type #{id} has been found");
        }
        return _mapper.Map<VariantTypeDto>(variantType);
    }
}