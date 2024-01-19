using AntaresApi.Dto.Common;
using AntaresApi.Dto.Status;
using AntaresApi.Models.Status;
using AntaresApi.Models.StoreModel;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class StatusService : IStatusService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public StatusService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<StatusDto> GetAllStatus()
    {
        List<Status> list = _context.Statuses
            .Include(s=>s.StoreModel)!
            .Include(s=>s.TransitionTo)!
            .Include(s=>s.StatusActions)!
            .ThenInclude(s=>s.ActionFunction)!
            .ThenInclude(s=>s.StoreModel)!
            .Include(s=>s.StatusActions)!
            .ThenInclude(s=>s.StatusActionTrigger)!
            .AsSplitQuery()
            .ToList();
        return list.Select(_mapper.Map<StatusDto>).ToList();
    }

    public StatusDto GetStatusById(long id)
    {
        Status? status = _context.Statuses
            .Include(s=>s.StoreModel)
            .Include(s=>s.TransitionTo)
            .Include(s=>s.StatusActions)!
            .ThenInclude(s=>s.ActionFunction)
            .ThenInclude(s=>s.StoreModel)
            .Include(s=>s.StatusActions)!
            .ThenInclude(s=>s.StatusActionTrigger)
            .AsSplitQuery()
            .FirstOrDefault(e=> e.Id == id);
        if (status == null)
        {
            throw new BadHttpRequestException($"No status #{id} has been found");
        }
        return _mapper.Map<StatusDto>(status);
    }

    public StatusDto CreateStatus(StatusDto status)
    {
        if (status.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code] has not been specified");
        }   
        if (status.StoreModel == null)
        {
            throw new BadHttpRequestException("Store model has not been specified");
        }

        StoreModel? storeModel = _context.StoreModels.FirstOrDefault(s => s.Id == status.StoreModel.Id);
        if (storeModel == null)
        {
            throw new BadHttpRequestException($"Store model #{status.StoreModel.Id} has not been found");
        }

        List<Status> transitionTo = new List<Status>();
        if (status.TransitionTo is {Count:>0})
        {
            foreach (var transitionStatus in status.TransitionTo)
            {
               Status? dbTransitionStatus = _context.Statuses.FirstOrDefault(s => s.Id == transitionStatus.Id);
               if (dbTransitionStatus == null)
               {
                   throw new BadHttpRequestException($"Status transition was wrongly specified");
               }
            }
           
        }
        try
        {
            Status dbStatus = new Status
            {
                Code = status.Code!,
                StoreModel = storeModel,
                TransitionTo = transitionTo,
                Priority = status.Priority ?? 0,
                Color = status.Color,
                Description = status.Description
            };
            _context.Statuses.Add(dbStatus);
            _context.SaveChanges();
            return _mapper.Map<StatusDto>(dbStatus);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public StatusDto UpdateStatus(StatusDto status)
    {
        if (status.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (status.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code] has not been specified");
        }   
        Status? dbStatus = _context.Statuses.FirstOrDefault(e => e.Id.Equals(status.Id));
        if (dbStatus is null)
        {
            throw new BadHttpRequestException($"There is no status #{status.Id}");
        }
        if (status.StoreModel == null)
        {
            throw new BadHttpRequestException("Store model has not been specified");
        }
        StoreModel? storeModel = _context.StoreModels.FirstOrDefault(s => s.Id == status.StoreModel.Id);
        if (storeModel == null)
        {
            throw new BadHttpRequestException($"Store model #{status.StoreModel.Id} has not been found");
        }

        List<Status> transitionTo = new List<Status>();
        if (status.TransitionTo is { Count: > 0 })
        {
            foreach (var transitionStatus in status.TransitionTo)
            {
                Status? dbTransitionStatus = _context.Statuses.FirstOrDefault(s => s.Id == transitionStatus.Id);
                if (dbTransitionStatus == null)
                {
                    throw new BadHttpRequestException($"Status transition was wrongly specified");
                }
            }
        }
        if(!dbStatus.Reserved)
        {
            dbStatus.Code = status.Code!;
        }
        dbStatus.Description = status.Description!;
        dbStatus.StoreModel = storeModel;
        dbStatus.TransitionTo = transitionTo;
        dbStatus.Priority = status.Priority ?? 0;
        dbStatus.Color = status.Color;
        _context.SaveChanges();
        return _mapper.Map<StatusDto>(dbStatus);
    }

    public long DeleteStatus(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleStatuses(List<IdDto> statuss)
    {
        if(statuss.Count >0)
        {
            foreach (var stat in statuss)
            {
                Status? status = _context.Statuses
                    .Include(s=>s.Employees).FirstOrDefault(e => e.Id == stat.Id);
                if (status != null)
                {
                    if (status.Reserved)
                    {
                        continue;
                    }
                    if (status.Code == "Unverified" || status.Code == "Registered")
                    {
                        continue;
                    }
                    _context.Statuses.Remove(status);
                }
            }

            _context.SaveChanges();
        }
        return statuss;
    }

    public StatusDto ColManAddToStatus(StatusDto transitionStatus, IdDto owner)
    {
         if (transitionStatus.Id == null)
        {
            throw new BadHttpRequestException("No transition status ID has been specified");
        }
        var dbTransitionStatus = _context.Statuses.FirstOrDefault(c => c.Id == transitionStatus.Id);
        if (dbTransitionStatus is null)
        {
            throw new BadHttpRequestException($"Trans-Status #{transitionStatus.Id} does not exist");
        }

        if (dbTransitionStatus.Reserved)
        {
            throw new BadHttpRequestException($"Status {dbTransitionStatus.Code} is reserved");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Status ID has not been specified");
        }
        var dbStatus = _context.Statuses.Include(e=>e.TransitionTo).FirstOrDefault(e => e.Id == owner.Id);
        if (dbStatus == null)
        {
            throw new BadHttpRequestException($"Status #{owner.Id} doest not exist");
        }

        if (dbStatus.Reserved)
        {
            throw new BadHttpRequestException("Status is reserved");
        }

        if (dbStatus.TransitionTo != null && dbStatus.TransitionTo.Any(c => c.Id == dbTransitionStatus.Id))
        {
            throw new BadHttpRequestException($"Status #{dbStatus.Id} already has signed transstatus #{dbTransitionStatus.Id}");
        }

        if (dbStatus.TransitionTo == null)
        {
            dbStatus.TransitionTo = new List<Status>();
        }
        dbStatus.TransitionTo.Add(dbTransitionStatus);
        _context.SaveChanges();
        return _mapper.Map<StatusDto>(dbTransitionStatus);
    }
}