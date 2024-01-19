using AntaresApi.Dto.Common;
using AntaresApi.Dto.Plan;
using AntaresApi.Models;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AntaresApi.Services;

public class PlanService : IPlanService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public PlanService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<PlanDto> GetAllPlan()
    {
        List<Plan> list = _context.Plans
            .Include(m=>m.Employees)
            .Include(o=>o.Company)
            .ToList();
     
        return list.Select(_mapper.Map<PlanDto>).ToList();
    }

    public PlanDto CreatePlan(PlanDto plan)
    {
        Company? dbCompany = _context.Companies.FirstOrDefault(c => c.Id == plan.Company.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException("Company has not been found");
        }
        List<Employee> dbEmployees = _context.Employees.AsEnumerable().ToList().Where(e=>plan.Employees.Any(ee=>ee.Id == e.Id)).ToList();
        Plan dbPlan = new Plan
        {
            Title = plan.Title,
            Content = plan.Content,
            Class = plan.Class,
            Employees = dbEmployees,
            Company = dbCompany,
            Start = plan.Start,
            Stop = plan.Stop
        };
        _context.Plans.Add(dbPlan);
        _context.SaveChanges();
        return _mapper.Map<PlanDto>(dbPlan);
    }
    public async  Task<PlanDto> UpdatePlan(PlanDto plan)
    {
        Plan? dbPlan = _context.Plans.FirstOrDefault(p => p.Id == plan.Id);
        if (dbPlan == null)
        {
            throw new BadHttpRequestException("Plan has not been found");
        }
        Company? dbCompany = _context.Companies.FirstOrDefault(c => c.Id == plan.Company.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException("Company has not been found");
        }
        List<Employee> dbEmployees = _context.Employees.Where(e=>plan.Employees.Any(ee=>ee.Id == e.Id)).ToList();
        
        dbPlan.Title = plan.Title;
        dbPlan.Content = plan.Content;
        dbPlan.Class = plan.Class;
        dbPlan.Employees = dbEmployees;
        dbPlan.Company = dbCompany;
        dbPlan.Start = plan.Start;
        dbPlan.Stop = plan.Stop;
        
        _context.Plans.Add(dbPlan);
        await _context.SaveChangesAsync();
        return _mapper.Map<PlanDto>(dbPlan);
    }
    public PlanDto GetPlanById(long id)
    {
        Plan? plan = _context.Plans
            .Include(o=>o.Company)
            .Include(o=>o.Employees)
            .FirstOrDefault(c=> c.Id == id);
        if (plan == null)
        {
            throw new BadHttpRequestException($"No plan #{id} has been found");
        }
        return _mapper.Map<PlanDto>(plan);
    }
    public List<PlanDto> GetPlansByEmployeeId(long id)
    {
        Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            throw new BadHttpRequestException("Employee has not been found");
        }
        List<Plan> plan = _context.Plans
            .Include(o=>o.Company)
            .Include(o=>o.Employees)
            .Where(p=>p.Employees.Any(e=>e.Id == id))
            .ToList();
        if (plan == null)
        {
            throw new BadHttpRequestException($"No plan #{id} has been found");
        }

        return plan.Select(_mapper.Map<PlanDto>).ToList();
    }
    public long DeletePlan(long id)
    {
        DeleteMultiplePlan(new List<IdDto> { new IdDto{Id=id}  } );
        return id;
    }
    public List<IdDto> DeleteMultiplePlan(List<IdDto> plans)
    {
        if(plans.Count >0)
        {
            foreach (var plan in plans)
            {
                Plan? dbPlan = _context.Plans.Include(m=>m.Employees)
                    .Include(p=>p.Company)
                    .FirstOrDefault(e => e.Id == plan.Id);
                if (dbPlan != null)
                {
                    _context.Plans.Remove(dbPlan);
                }
            }

            _context.SaveChanges();
        }
        return plans;
    }
}