using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Models;
using AntaresApi.Models.House;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AntaresApi.Services;

public class RealisationService : IRealisationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public RealisationService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<RealisationDto> GetAllRealisation()
    {
        List<Realisation> list = _context.Realisations
            .Include(r=>r.Variant)
            .Include(r=>r.Comparator)
            .Include(r=>r.VariantRealisation).ToList();
        return list.Select(_mapper.Map<RealisationDto>).ToList();
    }
    
    public RealisationDto GetRealisationById(long id)
    {
        Realisation? realisation = _context.Realisations
            .Include(r=>r.Variant)
            .Include(r=>r.Comparator)
            .Include(r=>r.VariantRealisation)
                            .FirstOrDefault(c=> c.Id == id);
        if (realisation == null)
        {
            throw new BadHttpRequestException($"No variant #{id} has been found");
        }
        return _mapper.Map<RealisationDto>(realisation);
    }
    
    public RealisationDto CreateRealisation(RealisationDto realisation)
    {
        throw new NotImplementedException("Not implemented || Realisation must have an owner");
    }

    public RealisationDto UpdateRealisation(RealisationDto realisation)
    {
       throw new NotImplementedException();
    }

    public RealisationDto DeleteRealisation(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleRealisation(List<IdDto> realisations)
    {
        
        if(realisations.Count >0)
        {
            foreach (var rl in realisations)
            {
                if (rl.Id == null)
                {
                    throw new BadHttpRequestException("Id of listed delete object has not been specified");
                }
                Realisation? realisation = _context.Realisations.FirstOrDefault(c => c.Id == rl.Id);

                if (realisation == null)
                {
                    throw new BadHttpRequestException($"Realisation #{rl.Id} has not been found");
                }
                _context.Realisations.Remove(realisation);
            }

            _context.SaveChanges();
        }
        return realisations;
    }
    
    public RealisationDto CreateRealisationToEmployee(RealisationDto realisation, IdDto owner)
    {
        var employee = _context.Employees
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Employee #{owner!.Id} does not exist");
        }
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }
  
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }

        if (variant.StoreModel!.Code != "Employee")
        {
            throw new Exception("This variant is not destined to employee");
        }
        if (variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }

        Realisation dbRealisation = PrepareRealisation(realisation, variant);
      
        if (employee.Realisations == null)
        {
            employee.Realisations = new List<Realisation>();
        }
        if (employee.Realisations.Any(r =>
              dbRealisation.VariantRealisation!=null && r.Variant.Id == variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} has the same realisation in database");
        }

        if (employee.Realisations.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"[DATE]Employee #{employee.Id} has the same realisation in database (date value)");
        }
        if (employee.Realisations.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"[NUM]Employee #{employee.Id} has the same realisation in database (date value)");
        }
        employee.Realisations.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
    public RealisationDto CreateRealisationToCompany(RealisationDto realisation, IdDto owner)
    {
        var company = _context.Companies
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Company #{owner.Id} does not exist");
        }
        
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }
        
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Code != "Company")
        {
            throw new Exception("This variant is not destined to company");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
      
        if (company.Realisations == null)
        {
            company.Realisations = new List<Realisation>();
        }
        if (company.Realisations!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Company #{company.Id} has the same realisation in database");
        }

        if (company.Realisations!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"Company #{company.Id} has the same realisation in database (date value)");
        }
        if (company.Realisations!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"Company #{company.Id} has the same realisation in database (date value)");
        }
        company.Realisations.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
       public RealisationDto CreateRealisationToCar(RealisationDto realisation, IdDto owner)
       {
        var car = _context.Cars
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(d=>d.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Car #{owner!.Id} does not exist");
        }
        
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }
        
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Code != "Car")
        {
            throw new Exception("This variant is not destined to document");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
      
        if (car.Realisations == null)
        {
            car.Realisations = new List<Realisation>();
        }
        if (car.Realisations!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Car #{car.Id} has the same realisation in database");
        }

        if (car.Realisations!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"Car #{car.Id} has the same realisation in database (date value)");
        }
        if (car.Realisations!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"Car #{car.Id} has the same realisation in database (date value)");
        }
        car.Realisations.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
    public RealisationDto CreateRealisationToDocument(RealisationDto realisation, IdDto owner)
    {
        var document = _context.Documents
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(d=>d.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (document == null)
        {
            throw new BadHttpRequestException($"Document #{owner!.Id} does not exist");
        }
        
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }
        
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Code != "Document")
        {
            throw new Exception("This variant is not destined to document");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
      
        if (document.Realisations == null)
        {
            document.Realisations = new List<Realisation>();
        }
        if (document.Realisations!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Document #{document.Id} has the same realisation in database");
        }

        if (document.Realisations!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"Document #{document.Id} has the same realisation in database (date value)");
        }
        if (document.Realisations!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"Document #{document.Id} has the same realisation in database (date value)");
        }
        document.Realisations.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
     public RealisationDto CreateRealisationToActionFunction(RealisationDto realisation, IdDto owner)
    {
        var actionFunction  = _context.ActionFunctions
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(d=>d.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (actionFunction  == null)
        {
            throw new BadHttpRequestException($"Action function #{owner!.Id} does not exist");
        }

     
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }

        Comparator? comparator = null;
        if (realisation.Variant.VariantType.Id != 3 && realisation.Comparator == null)
        {
            throw new BadHttpRequestException("Requirement numeric/date needs an comparator");
        }

        if (realisation.Comparator != null)
        {
            comparator = _context.Comparators.FirstOrDefault(c => c.Id == realisation.Comparator.Id);
        }
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Id != actionFunction.StoreModel.Id)
        {
            throw new Exception($"This variant is not destined to {actionFunction.StoreModel.Code}");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
        dbRealisation.Comparator = comparator;
        if (actionFunction.Requirements == null)
        {
            actionFunction.Requirements = new List<Realisation>();
        }
        if (actionFunction.Requirements!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Action function #{actionFunction.Id} has the same requirement in database");
        }

        if (actionFunction.Requirements!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"Action function #{actionFunction.Id} has the same requirement in database (date value)");
        }
        if (actionFunction.Requirements!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"Action function #{actionFunction.Id} has the same requirement in database (date value)");
        }
        actionFunction.Requirements.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
        public RealisationDto CreateRequirementToPosition(RealisationDto realisation, IdDto owner)
    {
        var position  = _context.Positions
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(d=>d.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (position  == null)
        {
            throw new BadHttpRequestException($"Action function #{owner!.Id} does not exist");
        }
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }

        Comparator? comparator = null;
        if (realisation.Variant.VariantType.Id != 3 && realisation.Comparator == null)
        {
            throw new BadHttpRequestException("Requirement numeric/date needs an comparator");
        }

        if (realisation.Comparator != null)
        {
            comparator = _context.Comparators.FirstOrDefault(c => c.Id == realisation.Comparator.Id);
        }
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Id != position.StoreModel.Id)
        {
            throw new Exception($"This variant is not destined to {position.StoreModel.Code}");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
        dbRealisation.Comparator = comparator;
        if (position.Requirements == null)
        {
            position.Requirements = new List<Realisation>();
        }
        if (position.Requirements!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"Position #{position.Id} has the same requirement in database");
        }

        if (position.Requirements!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"Position #{position.Id} has the same requirement in database (date value)");
        }
        if (position.Requirements!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"Position #{position.Id} has the same requirement in database (date value)");
        }
        position.Requirements.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
    public RealisationDto CreateRequirementToHouse(RealisationDto realisation, IdDto owner)
    {
        House? house  = _context.Houses
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(e=>e.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(d=>d.StoreModel)
            .FirstOrDefault(e => e.Id == owner!.Id);
        if (house  == null)
        {
            throw new BadHttpRequestException($"House #{owner!.Id} does not exist");
        }
        if (realisation.Variant == null)
        {
            throw new BadHttpRequestException("No variant provided");
        }

        if (realisation.Variant.VariantType == null)
        {
            throw new Exception("Variant Type has not been provided by an object");
        }

        Comparator? comparator = null;
        if (realisation.Variant.VariantType.Id != 3 && realisation.Comparator == null)
        {
            throw new BadHttpRequestException("Requirement numeric/date needs an comparator");
        }

        if (realisation.Comparator != null)
        {
            comparator = _context.Comparators.FirstOrDefault(c => c.Id == realisation.Comparator.Id);
        }
        var variant = _context.Variants
            .Include(v=>v.VariantType)
            .Include(v=>v.StoreModel)
            .FirstOrDefault(v => v.Id == realisation.Variant.Id);
        if (variant == null)
        {
            throw new BadHttpRequestException($"Variant #{realisation.Variant.Id} does not exist");
        }
        if (variant.StoreModel!.Id != house.StoreModel.Id)
        {
            throw new Exception($"This variant is not destined to {house.StoreModel.Code}");
        }
        Realisation dbRealisation = PrepareRealisation(realisation, variant);
        dbRealisation.Comparator = comparator;
        if (house.Requirements == null)
        {
            house.Requirements = new List<Realisation>();
        }
        if (house.Requirements!.Any(r =>
                dbRealisation.VariantRealisation != null && r.Variant.Id == dbRealisation.Variant.Id && r.VariantRealisation!.Id == dbRealisation.VariantRealisation.Id))
        {
            throw new BadHttpRequestException($"House #{house.Id} has the same requirement in database");
        }

        if (house.Requirements!.Any(r => dbRealisation.DateValue != null && r.Variant.Id == variant.Id && r.DateValue == dbRealisation.DateValue))
        {
            throw new BadHttpRequestException($"House #{house.Id} has the same requirement in database (date value)");
        }
        if (house.Requirements!.Any(r => dbRealisation.NumericValue != null && r.NumericValue != null &&r.Variant.Id == variant.Id && r.NumericValue == dbRealisation.NumericValue))
        {
            throw new BadHttpRequestException($"House #{house.Id} has the same requirement in database (date value)");
        }
        house.Requirements.Add(dbRealisation);
        _context.SaveChanges();
        return _mapper.Map<RealisationDto>(dbRealisation);
    }
    private Realisation PrepareRealisation(RealisationDto realisation, Variant variant)
    {
        switch (variant.VariantType.Id)
        {
            case 1:
                if (realisation.NumericValue == null)
                {
                    throw new BadHttpRequestException("Numeric requirement must have a value");
                }
                return new Realisation
                {
                    Variant = variant,
                    NumericValue = realisation.NumericValue
                };
            case 2:
                if (realisation.DateValue == null)
                {
                    throw new BadHttpRequestException("Date requirement must have a specified date");
                }
                return new Realisation
                {
                    Variant = variant,
                    DateValue = realisation.DateValue
                };
            case 3:
                if (realisation.VariantRealisation == null)
                {
                    throw new BadHttpRequestException("No variant realisation provided");
                }

                var variantRealisation = _context.VariantRealisations .Include(v=>v.Variant).FirstOrDefault(vr =>vr.Id == realisation.VariantRealisation.Id);
                if (variantRealisation == null)
                {
                    throw new BadHttpRequestException($"Variant realisation #{realisation.VariantRealisation.Id} does not exist");
                }

                if (variantRealisation.Variant?.Id != variant.Id)
                {
                    throw new BadHttpRequestException($"Variant #{variant.Id} does not owe variant realisation #{variantRealisation.Id}");
                }
                return new Realisation
                {
                    Variant = variant,
                    VariantRealisation = variantRealisation
                };
        }

        throw new NotImplementedException();
    }
}