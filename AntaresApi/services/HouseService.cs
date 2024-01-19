using AntaresApi.Dto.Common;
using AntaresApi.Dto.House;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class HouseService : IHouseService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public HouseService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<HouseDto> GetAllHouses()
    {
        List<House> list = _context.Houses
            .Include(m=>m.Documents)
            .Include(m=>m.Employees)
            .Include(m=>m.Companies)
            .Include(m=>m.Cars)
            .Include(m=>m.StoreModel)
            .Include(m=>m.Requirements)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantType)
            .Include(m=>m.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .ToList();
     
        return list.Select(_mapper.Map<HouseDto>).ToList();
    }

    public HouseDto GetHouseById(long id)
    {
        House? house = _context.Houses
            .Include(m=>m.Documents)
            .Include(m=>m.Employees)
            .Include(m=>m.Companies)
            .Include(m=>m.Cars)
            .Include(m=>m.StoreModel)
            .Include(m=>m.Requirements)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantType)
            .Include(m=>m.Requirements)!
            .ThenInclude(r=>r.VariantRealisation)
            .FirstOrDefault(c=> c.Id == id);
        if (house == null)
        {
            throw new BadHttpRequestException($"No house #{id} has been found");
        }
        return _mapper.Map<HouseDto>(house);
    }

    public HouseDto CreateHouse(HouseDto house)
    {
        if (house.Code.IsNullOrEmpty()) 
        {
            throw new BadHttpRequestException("Required Fields [Code] has not been specified");
        }
        try
        {
            House dbHouse = new House
            {

                Code = house.Code!,
                HouseAndLocalNumber = house.HouseAndLocalNumber,
                StreetName = house.StreetName,
                PostCode = house.PostCode,
                City = house.City,
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee")!
            };
            _context.Houses.Add(dbHouse);
            _context.SaveChanges();
            return _mapper.Map<HouseDto>(dbHouse);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public HouseDto UpdateHouse(HouseDto house)
    {
        House? dbHouse = _context.Houses.FirstOrDefault(m => m.Id == house.Id);
        if (dbHouse == null)
        {
            throw new BadHttpRequestException($"House {house.Id} has not been found");
        }
        dbHouse.Code = house.Code!;
        dbHouse.HouseAndLocalNumber = house.HouseAndLocalNumber;
        dbHouse.StreetName = house.StreetName;
        dbHouse.PostCode = house.PostCode;
        dbHouse.City = house.City;
        _context.SaveChangesAsync();
        return _mapper.Map<HouseDto>(dbHouse);    
    }

    public List<IdDto> DeleteMultipleHouse(List<IdDto> houses)
    {
        if(houses.Count >0)
            {
                foreach (var house in houses)
                {
                    House? dbHouse = _context.Houses.Include(m=>m.Documents).FirstOrDefault(e => e.Id == house.Id);
                    if (dbHouse != null)
                    {
                        _context.Houses.Remove(dbHouse);
                    }
                }

                _context.SaveChanges();
            }
            return houses;
    }
    public HouseDto ColManAddToCar(HouseDto house, IdDto owner)
    {
        if (house.Id == null)
        {
            throw new BadHttpRequestException("No house ID has been specified");
        }
        House? dbHouse = _context.Houses.FirstOrDefault(c => c.Id == house.Id);
        if (dbHouse is null)
        {
            throw new BadHttpRequestException($"House #{house.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Houses).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        if (dbCar.Houses != null && dbCar.Houses.Any(c => c.Id == dbHouse.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed house #{dbHouse.Id}");
        }

        if (dbCar.Houses == null)
        {
            dbCar.Houses = new List<House>();
        }
        dbCar.Houses.Add(dbHouse);
        _context.SaveChanges();
        return _mapper.Map<HouseDto>(dbHouse);
    }
    public HouseDto ColManAddToDocument(HouseDto house, IdDto owner)
    {
        if (house.Id == null)
        {
            throw new BadHttpRequestException("No house ID has been specified");
        }
        House? dbHouse = _context.Houses.FirstOrDefault(c => c.Id == house.Id);
        if (dbHouse is null)
        {
            throw new BadHttpRequestException($"House #{house.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Document ID has not been specified");
        }
        var dbDocument = _context.Documents.Include(e=>e.Houses).FirstOrDefault(e => e.Id == owner.Id);
        if (dbDocument == null)
        {
            throw new BadHttpRequestException($"Document #{owner.Id} doest not exist");
        }

        if (dbDocument.Houses != null && dbDocument.Houses.Any(c => c.Id == dbHouse.Id))
        {
            throw new BadHttpRequestException($"Document #{dbDocument.Id} already has signed house #{dbHouse.Id}");
        }

        if (dbDocument.Houses == null)
        {
            dbDocument.Houses = new List<House>();
        }
        dbDocument.Houses.Add(dbHouse);
        _context.SaveChanges();
        return _mapper.Map<HouseDto>(dbHouse);
    }
    public HouseDto ColManAddToEmployee(HouseDto house, IdDto owner)
    {
        if (house.Id == null)
        {
            throw new BadHttpRequestException("No house ID has been specified");
        }
        House? dbHouse = _context.Houses.FirstOrDefault(c => c.Id == house.Id);
        if (dbHouse is null)
        {
            throw new BadHttpRequestException($"House #{house.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Employee ID has not been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Houses).FirstOrDefault(e => e.Id == owner.Id);
        if (dbEmployee == null)
        {
            throw new BadHttpRequestException($"Employee #{owner.Id} doest not exist");
        }

        if (dbEmployee.Houses != null && dbEmployee.Houses.Any(c => c.Id == dbHouse.Id))
        {
            throw new BadHttpRequestException($"Employee #{dbEmployee.Id} already has signed house #{dbHouse.Id}");
        }

        if (dbEmployee.Houses == null)
        {
            dbEmployee.Houses = new List<House>();
        }
        dbEmployee.Houses.Add(dbHouse);
        _context.SaveChanges();
        return _mapper.Map<HouseDto>(dbHouse);
    }
    public HouseDto ColManAddToCompany(HouseDto house, IdDto owner)
    {
        if (house.Id == null)
        {
            throw new BadHttpRequestException("No house ID has been specified");
        }
        House? dbHouse = _context.Houses.FirstOrDefault(c => c.Id == house.Id);
        if (dbHouse is null)
        {
            throw new BadHttpRequestException($"House #{house.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Company ID has not been specified");
        }
        var dbCompany = _context.Companies.Include(e=>e.Houses).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException($"Company #{owner.Id} doest not exist");
        }

        if (dbCompany.Houses != null && dbCompany.Houses.Any(c => c.Id == dbHouse.Id))
        {
            throw new BadHttpRequestException($"Company #{dbCompany.Id} already has signed house #{dbHouse.Id}");
        }

        if (dbCompany.Houses == null)
        {
            dbCompany.Houses = new List<House>();
        }
        dbCompany.Houses.Add(dbHouse);
        _context.SaveChanges();
        return _mapper.Map<HouseDto>(dbHouse);
    }
    public List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner)
    {
        Company? company = _context.Companies
            .Include(e=>e.Houses)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                House? house = _context.Houses.FirstOrDefault(c => c.Id == com.Id);
                if (house != null && company.Houses is {Count:>0})
                {
                    company.Houses.Remove(house);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromCar(List<IdDto> collection, IdDto owner)
    {
        Car? car = _context.Cars
            .Include(e=>e.Houses)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                House? house = _context.Houses.FirstOrDefault(c => c.Id == com.Id);
                if (house != null && car.Houses is {Count:>0})
                {
                    car.Houses.Remove(house);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner)
    {
        _Document? document = _context.Documents
            .Include(e=>e.Houses)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (document == null)
        {
            throw new BadHttpRequestException($"Invalid document #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                House? house = _context.Houses.FirstOrDefault(c => c.Id == com.Id);
                if (house != null && document.Houses is {Count:>0})
                {
                    document.Houses.Remove(house);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromEmployee(List<IdDto> collection, IdDto owner)
    {
        Employee? employee = _context.Employees
            .Include(e=>e.Houses)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                House? house = _context.Houses.FirstOrDefault(c => c.Id == com.Id);
                if (house != null && employee.Houses is {Count:>0})
                {
                    employee.Houses.Remove(house);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
}