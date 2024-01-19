using AntaresApi.Dto.Common;
using AntaresApi.Dto.Position;
using AntaresApi.Models;
using AntaresApi.Models.Position;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class PositionService : IPositionService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PositionService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<PositionLiteDto> GetAllPosition()
    {
        List<Position> list = _context.Positions
            .Include(p=>p.Children)
            .Include(p=>p.Company)
            .Include(p=>p.Employees)
            .Include(p=>p.PositionUnit)
            .Include(p=>p.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(p=>p.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(p=>p.StoreModel)
            .ToList();
        return list.Select(_mapper.Map<PositionLiteDto>).ToList();
    }

    public PositionDto GetPositionById(long id)
    {
        Position? position = _context.Positions
            .Include(p=>p.Children)
            .Include(p=>p.Company)
            .Include(p=>p.Employees)
            .Include(p=>p.PositionUnit)
            .Include(p=>p.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(p=>p.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(p=>p.StoreModel)
            .FirstOrDefault(c=> c.Id == id);
        if (position == null)
        {
            throw new BadHttpRequestException($"No position #{id} has been found");
        }
        return _mapper.Map<PositionDto>(position);
    }
 
    public PositionDto CreatePosition(PositionDto positions)
    {
        throw new NotImplementedException("Not implemented || Position must have an owner");
    }

    public PositionDto UpdatePosition(PositionDto position)
    {
        Position? dbPosition = _context.Positions.FirstOrDefault(p => p.Id == position.Id);
        if (dbPosition == null)
        {
            throw new BadHttpRequestException($"Position #{position.Id} does not exist");
        }

        if (position.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Code cannot be empty");
        }
        dbPosition.Demand = position.Demand;
        dbPosition.Description = position.Description ?? "";
        dbPosition.Code = position.Code!;
        _context.SaveChanges();
        return _mapper.Map<PositionDto>(dbPosition);
    }

    public PositionDto DeletePosition(long id)
    {
        throw new NotImplementedException();
    }
    

    public List<IdDto> DeleteMultiplePosition(List<IdDto> positions)
    {
        if(positions.Count >0)
        {
            foreach (var rl in positions)
            {
                if (rl.Id == null)
                {
                    throw new BadHttpRequestException("Id of listed delete object has not been specified");
                }
                Position? position = _context.Positions.Include(p=>p.Children).Include(p=>p.Company).Include(p=>p.Employees).Include(p=>p.PositionUnit).FirstOrDefault(c => c.Id == rl.Id);

                if (position == null)
                {
                    throw new BadHttpRequestException($"Position #{rl.Id} has not been found");
                }
                _context.Positions.Remove(position);
            }

            _context.SaveChanges();
        }
        return positions;
    }

 public PositionDto ColManAddToCompany(PositionDto position, IdDto owner)
    {
        Company? company = _context.Companies
            .Include(d=>d.Positions)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Company #{owner!.Id} does not exist");
        }
        
        if (position.Code == null)
        {
            throw new BadHttpRequestException("No Code provided");
        }

        if (_context.Positions.FirstOrDefault(p => p.Code == position.Code) != null)
        {
            throw new BadHttpRequestException($"Position '{position.Code}' already exist");
        }

        if (position.Description == null)
        {
            throw new BadHttpRequestException("No Description provided");
        }
        if (position.PositionUnit == null)
        {
            throw new BadHttpRequestException("No unit type provided");
        }

        PositionUnit? positionUnit = _context.PositionUnits.FirstOrDefault(p => p.Id == position.PositionUnit.Id);
        if (positionUnit == null)
        {
            throw new BadHttpRequestException($"Position unit #{position.PositionUnit.Id} does not exist");
        }
        Position dbPosition = new Position
        {
            Code = position.Code,
            Description = position.Description,
            PositionUnit = positionUnit,
            Demand = position.Demand,
            StoreModel = _context.StoreModels.FirstOrDefault(sm=>sm.Code=="Employee")!
        };
        if (company.Positions == null)
        {
            company.Positions = new List<Position>();
        }
        company.Positions.Add(dbPosition);
        _context.SaveChanges();
        return _mapper.Map<PositionDto>(dbPosition);
    }

    public PositionDto ColManAddToEmployee(PositionDto position, IdDto owner)
    {
        Employee? employee = _context.Employees
            .Include(d=>d.Positions)
            .Include(e=>e.Companies)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Company #{owner!.Id} does not exist");
        }
        if (position.Id == null)
        {
            throw new BadHttpRequestException("No Id provided");
        }

        Position? dbPosition = _context.Positions.Include(p=>p.PositionUnit).FirstOrDefault(p => p.Id == position.Id);
        if (dbPosition == null)
        {
            throw new BadHttpRequestException($"Position #{position.Id} does not exist");
        }

        if (dbPosition.PositionUnit.Code != "Position")
        {
            throw new BadHttpRequestException("Position must be the type of position");
        }
        if (employee.Positions!.Any(p => p.Id == dbPosition.Id))
        {
            throw new BadHttpRequestException($"Employee already contains position #{dbPosition.Id}");
        }
        if (employee.Companies == null)
        {
            throw new BadHttpRequestException("Employee must have a proper company to join a position");
        }
        if (!(employee.Companies.Any(c => c.Id == dbPosition.CompanyId)))
        {
            throw new BadHttpRequestException("Employee companies does not contain chosen position");
        }
        if (employee.Positions == null)
        {
            employee.Positions = new List<Position>();
        }
        employee.Positions.Add(dbPosition);
        _context.SaveChanges();
        return _mapper.Map<PositionDto>(dbPosition);
    }
    
    public List<IdDto> ColDeleteMultipleFromEmployee(List<IdDto> collection, IdDto owner)
    {
        Employee? employee = _context.Employees
            .Include(e=>e.Positions)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Invalid employee #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var pos in collection)
            {
                Position? position = _context.Positions.Include(p=>p.Company).Include(p=>p.Employees).Include(p=>p.PositionUnit).FirstOrDefault(c => c.Id == pos.Id);
                if (position != null && employee.Positions is {Count:>0})
                {
                    employee.Positions.Remove(position);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }

    public PositionDto MovePositionToChildrenOfPosition(PositionDto positionFrom, PositionDto positionTo)
    {
        Position? dbPositionFrom = _context.Positions.Include(p=>p.PositionUnit).Include(p=>p.Children).FirstOrDefault(p => p.Id == positionFrom.Id);
        if (dbPositionFrom == null)
        {
            throw new BadHttpRequestException($"Position(#{positionFrom.Id}) /from/ has not been found ");
        }   
        Position? dbPositionTo = _context.Positions.Include(p=>p.PositionUnit).Include(p=>p.Children).FirstOrDefault(p => p.Id == positionTo.Id);
        if (dbPositionTo == null)
        {
            throw new BadHttpRequestException($"Position(#{positionTo.Id}) /to/ has not been found ");
        }

        if (dbPositionTo.Children!.Any(c => c.Id == dbPositionFrom.Id))
        {
            throw new BadHttpRequestException($"Position(#{dbPositionTo.Id}) already contains position #{dbPositionFrom.Id} ");
        }
        
        dbPositionFrom.ParentId = dbPositionTo.Id;
        _context.SaveChanges();
        return _mapper.Map<PositionDto>(dbPositionTo);
    }
    
}