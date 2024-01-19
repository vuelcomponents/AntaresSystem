using AntaresApi.Dto.Action;
using AntaresApi.Models.StatusAction;
using AntaresApi.Services.Interfaces;
using AutoMapper;

namespace AntaresApi.Services;

public class StatusActionTriggerService : IStatusActionTriggerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public StatusActionTriggerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<StatusActionTriggerDto> GetAllStatusActionTriggers()
    {
        List<StatusActionTrigger> list = _context.StatusActionTriggers.ToList();
        return list.Select(_mapper.Map<StatusActionTriggerDto>).ToList();
    }

    public StatusActionTriggerDto GetStatusActionTriggerById(long id)
    {
        StatusActionTrigger? statusActionTrigger = _context.StatusActionTriggers
            .FirstOrDefault(c=> c.Id == id);
        if (statusActionTrigger == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<StatusActionTriggerDto>(statusActionTrigger);
    }
}