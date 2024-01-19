using AntaresApi.Dto.Action;
using AntaresApi.Models.StatusAction;
using AntaresApi.Services.Interfaces;

namespace AntaresApi.Services;

using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Models;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


public class StatusActionService : IStatusActionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public StatusActionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<StatusActionDto> GetAllStatusAction()
    {
        List<StatusAction> list = _context.StatusActions
            .Include(s=>s.ActionFunction)
            .ThenInclude(s=>s.StoreModel)
            .Include(s=>s.StatusActionTrigger)
            .ToList();
        return list.Select(_mapper.Map<StatusActionDto>).ToList();
    }

    public StatusActionDto GetStatusActionById(long id)
    {
        StatusAction? statusAction = _context.StatusActions!
            .Include(s=>s.ActionFunction)
            .ThenInclude(s=>s.StoreModel)
            .Include(s=>s.StatusActionTrigger)
            .FirstOrDefault(e=> e.Id == id);
        if (statusAction == null)
        {
            throw new BadHttpRequestException($"No statusAction #{id} has been found");
        }
        return _mapper.Map<StatusActionDto>(statusAction);
    }

    public StatusActionDto CreateStatusAction(StatusActionDto statusAction)
    {
        throw new NotImplementedException();
    }

    public StatusActionDto UpdateStatusAction(StatusActionDto statusAction)
    {
        throw new NotImplementedException();
    }

    public long DeleteStatusAction(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleStatusAction(List<IdDto> statusActions)
    {
        throw new NotImplementedException();
    }
    public List<IdDto> ColDeleteMultipleStatusActionFromStatus(List<IdDto> collection, IdDto owner)
    {
        Status? status = _context.Statuses
            .Include(e=>e.StoreModel)
            .Include(e=>e.StatusActions)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (status == null)
        {
            throw new BadHttpRequestException($"Invalid statusAction #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var sta in collection)
            {
                StatusAction? statusAction = _context.StatusActions.FirstOrDefault(c => c.Id == sta.Id);
                if (status != null && status?.StatusActions is {Count:>0})
                {
                    status.StatusActions.Remove(statusAction);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    
    public StatusActionDto ColManAddToStatus(StatusActionDto statusAction, IdDto owner)
    {
        Status? status = _context.Statuses.Include(s=>s.StoreModel)
            .Include(s=>s.StatusActions)!
            .ThenInclude(sa=>sa.StatusActionTrigger)
            .Include(s=>s.StatusActions)!
            .ThenInclude(sa=>sa.ActionFunction)
            .FirstOrDefault(s => s.Id == owner.Id);
        if (status == null)
        {
            throw new BadHttpRequestException("Status has not been specified");
        }
        if (statusAction.ActionFunction == null)
        {
            throw new BadHttpRequestException("Status action function has not been specified");
        }

        ActionFunction? actionFunction =
            _context.ActionFunctions.Include(s=>s.StoreModel)
                .FirstOrDefault(s => s.Id == statusAction.ActionFunction.Id);
        if (actionFunction == null)
        {
            throw new BadHttpRequestException(
                $"Status action function #{statusAction.ActionFunction.Id} has not been found");
        }

        if (statusAction.StatusActionTrigger == null)
        {
            throw new BadHttpRequestException("Status action Trigger has not been specified");
        }
        StatusActionTrigger? statusActionTrigger=
            _context.StatusActionTriggers.FirstOrDefault(s => s.Id == statusAction.StatusActionTrigger.Id);
        if (statusActionTrigger == null)
        {
            throw new BadHttpRequestException(
                $"Status action trigger #{statusAction.StatusActionTrigger.Id} has not been found");
        }

        if (actionFunction.StoreModel.Id != status.StoreModel.Id)
        {
            throw new BadHttpRequestException($"Store model of function is not destined to {status.StoreModel.Code}");
        }

        StatusAction dbStatusAction = new StatusAction();
        dbStatusAction.StatusActionTrigger = statusActionTrigger;
        dbStatusAction.ActionFunction = actionFunction;
        if (status.StatusActions!.Any(s => s.StatusActionTrigger.Id == dbStatusAction.StatusActionTrigger.Id 
                                          && s.ActionFunction.Id == dbStatusAction.ActionFunction.Id))
        {
            throw new BadHttpRequestException("Status has already the same action");
        }
        status.StatusActions.Add(dbStatusAction);
        _context.SaveChanges();
        return _mapper.Map<StatusActionDto>(dbStatusAction);
     

    }
}