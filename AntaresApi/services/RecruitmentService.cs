using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Models;
using AntaresApi.Models.Document;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Position;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class RecruitmentService : IRecruitmentService
{
      private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IEmployeeService _employeeService;

    public RecruitmentService(IMapper mapper, DataContext context, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _context = context;
        _employeeService = employeeService;
    }
    public IEnumerable<RecruitmentDto> GetAllRecruitment()
    {
        List<Recruitment> list = _context.Recruitments
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Company)
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Position)
            .Include(r=>r.Employees)
            .Include(e=>e.Variants)
            .ThenInclude(v=>v.VariantType)
            .Include(v=>v.Variants)
            .ThenInclude(v=>v.StoreModel)
            .Include(r=>r.DocumentTypes)
            .Include(r=>r.Employees)
            .ThenInclude(e=>e.Documents)
            .ThenInclude(d=>d.DocumentType)
            .ToList();
     
        return list.Select(_mapper.Map<RecruitmentDto>).ToList();
    }

    public RecruitmentDto GetRecruitmentById(long id)
    {
        Recruitment? recruitment = _context.Recruitments
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Company)
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Position)
            .Include(r=>r.Employees)
            .Include(e=>e.Variants)
            .ThenInclude(v=>v.VariantType)
            .Include(v=>v.Variants)
            .ThenInclude(v=>v.StoreModel)
            .Include(r=>r.DocumentTypes)
            .Include(r=>r.Employees)
            .ThenInclude(e=>e.Documents)
            .ThenInclude(d=>d.DocumentType)
            .Include(r=>r.Employees)
            .ThenInclude(e=>e.RecruitmentContact)
            .FirstOrDefault(c=> c.Id == id);
        if (recruitment == null)
        {
            throw new BadHttpRequestException($"No recruitment #{id} has been found");
        }
        var dtoRecruitment =  _mapper.Map<RecruitmentDto>(recruitment);
        if(dtoRecruitment.Employees is {Count:>0})
        {
            foreach (var dtoRecruitmentEmployee in dtoRecruitment.Employees)
            {
                dtoRecruitmentEmployee.TempAdvise = _employeeService.AdviseToEmployeeForSpecificPosition(
                    new IdDto
                    {
                        Id = dtoRecruitmentEmployee.Id
                    },
                    new IdDto
                    {
                        Id = recruitment.Offer!.Position!.Id
                    }
                );
            }
        }
        return dtoRecruitment;
    }

    public RecruitmentDto CreateRecruitment(RecruitmentShortDto recruitment)
    {
        if (recruitment.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Code must be fulfilled");
        }
        if (_context.Recruitments.ToList().Any(r =>
                r.Code == recruitment.Code ))
        {
            throw new BadHttpRequestException("This code is being used by another recruitment process");
        }
        if (recruitment.Code.IsNullOrEmpty() || recruitment.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Title and message cannot be empty");
        }
        if (recruitment.Offer == null)
        {
            throw new BadHttpRequestException("Job offer must be set");
        }

    
       
        Offer? offer = _context.Offers
            .Include(o=>o.Company)
            .FirstOrDefault(o => o.Id == recruitment.Offer.Id);
        if (offer == null)
        {
            throw new BadHttpRequestException($"Offer #{recruitment.Offer.Id} has not been found");
        }

        if (_context.Recruitments.Include(r => r.Offer).ToList().Any(r => r.Offer!.Id == offer.Id))
        {
            throw new BadHttpRequestException("This offer is used by another recruitment process");
        }
        
        Recruitment? dbRecruitment = new Recruitment
        {
            Code = recruitment.Code!,
            Description = recruitment.Description!,
            Open = recruitment.Open ?? false,
            Offer = offer,
            StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
        };
        if (recruitment.Status == null)
        {
            throw new BadHttpRequestException("Hire status must be fulfilled");
        }

        Status? status = _context.Statuses.FirstOrDefault(s => s.Id == recruitment.Status.Id);
        if (status == null)
        {
            throw new BadHttpRequestException("Status has not been found");
        }
        if (status.StoreModel.Id != dbRecruitment.StoreModel.Id)
        {
            throw new BadHttpRequestException($"Status only allowed to model {dbRecruitment.StoreModel.Code}");
        }

        if (status.Reserved)
        {
            throw new BadHttpRequestException("None of reserved status can be used");
        }
        dbRecruitment.StatusId = status.Id;

        _context.Recruitments.Add(dbRecruitment);
        _context.SaveChanges();
        return _mapper.Map<RecruitmentDto>(dbRecruitment);
    }

    public RecruitmentDto UpdateRecruitment(RecruitmentShortDto recruitment)
    {
        Recruitment? dbRecruitment = _context.Recruitments
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Company)
            .Include(r=>r.Offer)
            .ThenInclude(r=>r.Position)
            .Include(r=>r.Employees)
            .Include(e=>e.Variants)
            .ThenInclude(v=>v.VariantType)
            .Include(v=>v.Variants)
            .ThenInclude(v=>v.StoreModel)
            .FirstOrDefault(m => m.Id == recruitment.Id);
        if (dbRecruitment == null)
        {
            throw new BadHttpRequestException($"Recruitment {recruitment.Id} has not been found");
        }

        if (_context.Recruitments.ToList().Any(r =>
                r.Code == recruitment.Code  && dbRecruitment.Code != recruitment.Code))
        {
            throw new BadHttpRequestException("This code is being used by another recruitment process");
        }
        if (!dbRecruitment.Code.IsNullOrEmpty())
        {
            dbRecruitment.Code = recruitment.Code!;
        }

        if (!dbRecruitment.Description.IsNullOrEmpty())
        {
            dbRecruitment.Description = recruitment.Description!;
        }
        if (recruitment.Offer == null)
        {
            throw new BadHttpRequestException("Job offer must be set");
        }
        
        Offer? offer = _context.Offers
            .Include(o=>o.Company)
            .FirstOrDefault(o => o.Id == recruitment.Offer.Id);
        if (offer == null)
        {
            throw new BadHttpRequestException($"Offer #{recruitment.Offer.Id} has not been found");
        }

        if (_context.Recruitments.Include(r => r.Offer).ToList().Any(
                r => r.Offer!.Id == recruitment.Offer.Id && r.Offer!.Id != offer.Id)
            )
        {
            throw new BadHttpRequestException("This offer is used by another recruitment process");
        }
        
        dbRecruitment.OfferId = offer.Id;
        dbRecruitment.Open = recruitment.Open ?? false;
        
        _context.SaveChangesAsync();
        return _mapper.Map<RecruitmentDto>(dbRecruitment);
    }

    public List<IdDto> DeleteMultipleRecruitment(List<IdDto> recruitments)
    {
        if(recruitments.Count >0)
        {
            foreach (var recruitment in recruitments)
            {
                Recruitment? dbRecruitment = _context.Recruitments.FirstOrDefault(e => e.Id == recruitment.Id);
                if (dbRecruitment != null)
                {
                    _context.Recruitments.Remove(dbRecruitment);
                }
            }

            _context.SaveChanges();
        }
        return recruitments;
    }

    public RecruitmentDto Advance(EmployeeDto employee)
    {
        throw new NotImplementedException();
    }
}