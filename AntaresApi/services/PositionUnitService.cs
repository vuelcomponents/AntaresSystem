using AntaresApi.Dto.Common;
using AntaresApi.Models.Position;
using AntaresApi.Services.Interfaces;
using AutoMapper;

namespace AntaresApi.Services;

public class PositionUnitService : IPositionUnitService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PositionUnitService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<IdCodeDto> GetAllPositionUnit()
    {
        List<PositionUnit> list = _context.PositionUnits.ToList();
        return list.Select(_mapper.Map<IdCodeDto>).ToList();
    }

    public IdCodeDto GetPositionUnitById(long id)
    {
        PositionUnit? positionunit = _context.PositionUnits.FirstOrDefault(c=> c.Id == id);
        if (positionunit == null)
        {
            throw new BadHttpRequestException($"No positionunit #{id} has been found");
        }
        return _mapper.Map<IdCodeDto>(positionunit);
    }
}