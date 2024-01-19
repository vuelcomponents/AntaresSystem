using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Position;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class CompanyService : ICompanyService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CompanyService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<CompanyLiteDto> GetAllCompany()
    {
        List<Company> list = _context.Companies
            .Include(c=>c.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.Realisations)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantRealisations)
            .Include(c=>c.Employees)
            .Include(c=>c.Documents)
            .Include(c=>c.Status)
            .ThenInclude(s=>s.TransitionTo)
            .Include(s=>s.StoreModel)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Employees)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Children)
            .ThenInclude(c=>c.Employees)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Requirements)
            .ThenInclude(r=>r.Variant)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Requirements)
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.StoreModel)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.StoreModel)
            .Include(c=>c.Houses)
            .Include(c=>c.Cars)
            .AsSplitQuery()
            .ToList();
        return list.Select(_mapper.Map<CompanyLiteDto>).ToList();
    }

    public CompanyDto GetCompanyById(long id)
    {
        Company? company = _context.Companies.
            Include(c=>c.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.Realisations)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantRealisations)
            .Include(c=>c.Employees)!
            .ThenInclude(c=>c.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.Realisations)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantRealisations)
            .Include(e=>e.Documents)!
            .ThenInclude(e=>e.Employees)
            .Include(c=>c.Status)!
            .ThenInclude(s=>s.TransitionTo)
            .Include(s=>s.StoreModel)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Employees)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Children)
            .ThenInclude(c=>c.Employees)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Requirements)!
            .ThenInclude(r=>r.Variant)
            .Include(c=>c.Positions)
            .ThenInclude(p=>p.Requirements)
            .ThenInclude(r=>r.VariantRealisation)
            .Include(c=>c.Positions)!
             .ThenInclude(p=>p.StoreModel)
            .Include(c=>c.Houses)
            .Include(c=>c.Cars)
            .FirstOrDefault(c=> c.Id == id);
        if (company == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<CompanyDto>(company);
    }

    public CompanyDto CreateCompany(CompanyDto company)
    {
        if (company.Code.IsNullOrEmpty() || company.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        Status? status = null;
        if (company.Status != null)
        {
            status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == company.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            if (status.StoreModel.Code != "Company")
            {
                throw new Exception("Status in destined to company");
            }
        }
        try
        {
            Company dbCompany = new Company
            {

                Code = company.Code!,
                Description = company.Description!,
                HouseAndLocalNumber = company.HouseAndLocalNumber,
                StreetName = company.StreetName,
                PostCode = company.PostCode,
                City = company.City,
                Country = company.Country,
                Status = status,
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Company")!
            };
            _context.Companies.Add(dbCompany);
            _context.SaveChanges();
            return _mapper.Map<CompanyDto>(dbCompany);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public CompanyDto UpdateCompany(CompanyDto company)
    {
        if (company.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (company.Code.IsNullOrEmpty() || company.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        Company? dbCompany = _context.Companies.FirstOrDefault(e => e.Id.Equals(company.Id));
        if (dbCompany is null)
        {
            throw new BadHttpRequestException($"There is no employee #{company.Id}");
        }
        Status? status = null;
        if (company.Status != null)
        {
            status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == company.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            if (status.StoreModel.Code != "Company")
            {
                throw new Exception("Status is not destined to company");
            }
        }
        dbCompany.Code = company.Code!;
        dbCompany.Description = company.Description!;
        dbCompany.StreetName = company.StreetName;
        dbCompany.City = company.City;
        dbCompany.PostCode = company.PostCode;
        dbCompany.HouseAndLocalNumber = company.HouseAndLocalNumber;
        dbCompany.Country = company.Country;
        dbCompany.StatusId = status?.Id;
        _context.SaveChanges();
        return _mapper.Map<CompanyDto>(dbCompany);
    }

    public long DeleteCompany(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleCompany(List<IdDto> companies)
    {
        if(companies.Count >0)
        {
            foreach (var com in companies)
            {
                Company? company = _context.Companies
                    .Include(c=>c.Documents)
                    .Include(c=>c.Employees)
                    .Include(c=>c.Realisations)
                    .Include(c=>c.Positions)
                    .ThenInclude(p=>p.Requirements)
                    .Include(c=>c.Positions)
                    .ThenInclude(p=>p.Offers)
                    .FirstOrDefault(c => c.Id == com.Id);
                if (company != null)
                {
                    _context.Companies.Remove(company);
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                throw new BadHttpRequestException("You must remove offers attached to company");
            }
        }
        return companies;
    }

    public List<IdDto> ColDeleteMultipleCompanyFromEmployee(List<IdDto> collection, IdDto owner)
    {
        Employee? employee = _context.Employees
            .Include(e=>e.Companies)!
            .ThenInclude(c=>c.Positions)
            .Include(e=>e.Positions)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Invalid employee #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Company? company = _context.Companies.FirstOrDefault(c => c.Id == com.Id);
                if (company != null && employee.Companies is {Count:>0})
                {
                    if (employee.Positions is { Count: > 0 } && company.Positions is { Count: > 0 })
                    {
                        foreach (var companyPosition in company.Positions)
                        {
                            if (employee.Positions.Any(p => p.Id == companyPosition.Id))
                            {
                                employee.Positions.Remove(companyPosition);
                            }
                        }
                    }
                    employee.Companies.Remove(company);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColDeleteMultipleCompanyFromDocument(List<IdDto> collection, IdDto owner)
    {
        _Document? document = _context.Documents
            .Include(e=>e.Companies)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (document == null)
        {
            throw new BadHttpRequestException($"Invalid document #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Company? company = _context.Companies.FirstOrDefault(c => c.Id == com.Id);
                if (company != null && document.Companies is {Count:>0})
                {
                    document.Companies.Remove(company);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColDeleteMultipleCompanyFromHouse(List<IdDto> collection, IdDto owner)
    {
        House? house = _context.Houses
            .Include(e=>e.Companies)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (house == null)
        {
            throw new BadHttpRequestException($"Invalid house #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Company? company = _context.Companies.FirstOrDefault(c => c.Id == com.Id);
                if (company != null && house.Companies is {Count:>0})
                {
                    house.Companies.Remove(company);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColDeleteMultipleCompanyFromCar(List<IdDto> collection, IdDto owner)
    {
        Car? car = _context.Cars
            .Include(e=>e.Companies)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Company? company = _context.Companies.FirstOrDefault(c => c.Id == com.Id);
                if (company != null && car.Companies is {Count:>0})
                {
                    car.Companies.Remove(company);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public CompanyDto ColManAddToDocument(CompanyDto company, IdDto owner)
    {
        if (company.Id == null)
        {
            throw new BadHttpRequestException("No company ID has been specified");
        }
        var dbCompany = _context.Companies.FirstOrDefault(c => c.Id == company.Id);
        if (dbCompany is null)
        {
            throw new BadHttpRequestException($"Company #{company.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Document ID has not been specified");
        }
        var dbDocument = _context.Documents.Include(e=>e.Companies).FirstOrDefault(e => e.Id == owner.Id);
        if (dbDocument == null)
        {
            throw new BadHttpRequestException($"Employee #{owner.Id} doest not exist");
        }

        if (dbDocument.Companies != null && dbDocument.Companies.Any(c => c.Id == dbCompany.Id))
        {
            throw new BadHttpRequestException($"Employee #{dbDocument.Id} already has signed company #{dbCompany.Id}");
        }

        if (dbDocument.Companies == null)
        {
            dbDocument.Companies = new List<Company>();
        }
        dbDocument.Companies.Add(dbCompany);
        _context.SaveChanges();
        return _mapper.Map<CompanyDto>(dbCompany);
    }
    public CompanyDto ColManAddToHouse(CompanyDto company, IdDto owner)
    {
        if (company.Id == null)
        {
            throw new BadHttpRequestException("No company ID has been specified");
        }
        var dbCompany = _context.Companies.FirstOrDefault(c => c.Id == company.Id);
        if (dbCompany is null)
        {
            throw new BadHttpRequestException($"Company #{company.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("House ID has not been specified");
        }
        House? dbHouse = _context.Houses.Include(e=>e.Companies).FirstOrDefault(e => e.Id == owner.Id);
        if (dbHouse == null)
        {
            throw new BadHttpRequestException($"House #{owner.Id} doest not exist");
        }

        if (dbHouse.Companies != null && dbHouse.Companies.Any(c => c.Id == dbCompany.Id))
        {
            throw new BadHttpRequestException($"House #{dbHouse.Id} already has signed company #{dbCompany.Id}");
        }

        if (dbHouse.Companies == null)
        {
            dbHouse.Companies = new List<Company>();
        }
        dbHouse.Companies.Add(dbCompany);
        _context.SaveChanges();
        return _mapper.Map<CompanyDto>(dbCompany);
    }
    public CompanyDto ColManAddToEmployee(CompanyDto company, IdDto owner)
    {
        if (company.Id == null)
        {
            throw new BadHttpRequestException("No company ID has been specified");
        }
        var dbCompany = _context.Companies.FirstOrDefault(c => c.Id == company.Id);
        if (dbCompany is null)
        {
            throw new BadHttpRequestException($"Company #{company.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Employee ID has not been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Companies).FirstOrDefault(e => e.Id == owner.Id);
        if (dbEmployee == null)
        {
            throw new BadHttpRequestException($"Employee #{owner.Id} doest not exist");
        }

        if (dbEmployee.Companies != null && dbEmployee.Companies.Any(c => c.Id == dbCompany.Id))
        {
            throw new BadHttpRequestException($"Employee #{dbEmployee.Id} already has signed company #{dbCompany.Id}");
        }

        if (dbEmployee.Companies == null)
        {
            dbEmployee.Companies = new List<Company>();
        }
        dbEmployee.Companies.Add(dbCompany);
        _context.SaveChanges();
        return _mapper.Map<CompanyDto>(dbCompany);
    }
    public CompanyDto ColManAddToCar(CompanyDto company, IdDto owner)
    {
        if (company.Id == null)
        {
            throw new BadHttpRequestException("No company ID has been specified");
        }
        var dbCompany = _context.Companies.FirstOrDefault(c => c.Id == company.Id);
        if (dbCompany is null)
        {
            throw new BadHttpRequestException($"Company #{company.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Companies).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        if (dbCar.Companies != null && dbCar.Companies.Any(c => c.Id == dbCompany.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed company #{dbCompany.Id}");
        }

        if (dbCar.Companies == null)
        {
            dbCar.Companies = new List<Company>();
        }
        dbCar.Companies.Add(dbCompany);
        _context.SaveChanges();
        return _mapper.Map<CompanyDto>(dbCompany);
    }

    public PersonnelAdviseContainerWithColumns Advise(IdCodeDto company)
    {
    List<string> columns = new List<string>();        
    List < PersonnelAdviseContainer > personnelAdviseContainers = new List < PersonnelAdviseContainer > ();

    Company ? dbCompany = _context.Companies.Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.VariantRealisation).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Variant).ThenInclude(v => v.VariantType).Include(c => c.Employees).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Comparator).AsSplitQuery()
      .FirstOrDefault(c => c.Id == company.Id);
    if (dbCompany == null) {
      throw new BadHttpRequestException($"Company #{company.Id} does not exist");
    }

    List < Employee > dbEmployees = _context.Employees.Include(e=>e.Positions).Include(r => r.Realisations) !.ThenInclude(r => r.VariantRealisation).Include(r => r.Realisations) !.ThenInclude(r => r.Variant).ThenInclude(v => v.VariantType).Include(e => e.Companies).Include(e => e.Positions).AsSplitQuery()
      .ToList();
    if (dbCompany.Positions is {
        Count: > 0
      }) {

      foreach(var dbEmployee in dbEmployees) {
        PersonnelAdviseContainer adviseContainer = new PersonnelAdviseContainer 
        {
            Company = _mapper.Map < CompanyShortDto > (dbCompany),
            Employee = _mapper.Map < EmployeeSuperShortDto > (dbEmployee),
            Advises = new List < PersonnelAdvise > ()
        };
        adviseContainer.Employee.HandledPositions = dbEmployee.Positions?.Count ?? 0;
        foreach(var companyPosition in dbCompany.Positions) 
        {
          PersonnelAdvise advise = new PersonnelAdvise 
          {
            Position = _mapper.Map < PositionShortDto > (companyPosition)
          };
          int max = companyPosition.Requirements!.Count;
          if (dbEmployee.Realisations is { Count: > 0 } && companyPosition.Requirements is { Count: > 0 }) 
          {
            if (!columns.Contains(companyPosition.Code))
            {
                  columns.Add(companyPosition.Code);
            }  
            foreach(var requirement in companyPosition.Requirements) 
            {
              Realisation ? realisation = dbEmployee.Realisations.FirstOrDefault(r => r.Variant.Id == requirement.Variant!.Id);
              if (realisation == null) {
                advise.Loss++;
                Percents(ref advise, max);
                advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement));
              } 
              else 
              {
                switch (requirement.Variant.VariantType.Code) 
                {
                case "Numeric":
                  switch (requirement.Comparator!.Code) 
                  {
                  case ">":
                    if (realisation.NumericValue > requirement.NumericValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add( _mapper.Map < RealisationShortDto > (requirement));
                    }
                  break;
                  case "<":
                    if (realisation.NumericValue < requirement.NumericValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case ">=":
                    if (realisation.NumericValue >= requirement.NumericValue) 
                    {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } 
                    else 
                    {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case "<=":
                    if (realisation.NumericValue <= requirement.NumericValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } 
                    else 
                    {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case "==":
                    if (realisation.NumericValue == requirement.NumericValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                    break;
                  }
                break;
                case "Date":
                  switch (requirement.Comparator!.Code) {
                  case ">":
                    if (realisation.DateValue > requirement.DateValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case "<":
                    if (realisation.DateValue < requirement.DateValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case ">=":
                    if (realisation.DateValue >= requirement.DateValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case "<=":
                    if (realisation.DateValue <= requirement.DateValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  case "==":
                    if (realisation.DateValue == requirement.DateValue) {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement)
                      );
                    } else {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement)
                      );
                    }
                  break;
                  }
                  break;
                case "Variant Realisation":
                  if (realisation.VariantRealisation!.Id == requirement.VariantRealisation!.Id) {
                    advise.Chance++;
                    Percents(ref advise, max);
                    advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  } else {
                    advise.Loss++;
                    Percents(ref advise, max);
                    advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  }
                  break;
                }
              }
            }
          } else 
          {
            continue;
          }
          adviseContainer.Advises.Add(advise);
        }
        personnelAdviseContainers.Add(adviseContainer);
      }
    }
    return new PersonnelAdviseContainerWithColumns()
    {
        AdviseContainers = personnelAdviseContainers,
        Columns = columns
    };
    }

    private void Percents(ref PersonnelAdvise advise, int max)
    {
        if (advise.Chance - advise.Loss != 0)
        {
            advise.WithLossPercent = (int)Math.Round(((double)(advise.Chance - advise.Loss) / max) * 100);
            // advise.WithLossPercent = (int)Math.Round(advise.WithLossPercent / 10.0) * 10;
        }
        else
        {
            advise.WithLossPercent = 0;
        }

        if (advise.Chance != 0)
        {
            advise.NoLossPercent = (int)Math.Round(((double)advise.Chance / max) * 100);
            // advise.NoLossPercent = (int)Math.Round(advise.NoLossPercent / 10.0) * 10;
        }
        else
        {
            advise.NoLossPercent = 0;
        }

    }
}