using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class CarService : ICarService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CarService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<CarDto> GetAllCars()
    {
        List<Car> list = _context.Cars
            .Include(m=>m.Documents)
            .Include(m=>m.Realisations)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantType)
            .Include(m=>m.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(m=>m.Passengers)
            .Include(m=>m.Drivers)
            .Include(m=>m.Companies)
            .Include(s=>s.StoreModel)
            .Include(m=>m.Houses)
            .ToList();
     
        return list.Select(_mapper.Map<CarDto>).ToList();
    }

    public CarDto GetCarById(long id)
    {
        Car? car = _context.Cars
            .Include(m=>m.Documents)
            .Include(m=>m.Realisations)!
            .ThenInclude(r=>r.Variant)
            .ThenInclude(v=>v.VariantType)
            .Include(m=>m.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(m=>m.Passengers)
            .Include(m=>m.Drivers)
            .Include(m=>m.Companies)
            .Include(s=>s.StoreModel)
            .Include(m=>m.Houses)
            .FirstOrDefault(c=> c.Id == id);
        if (car == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<CarDto>(car);
    }

    public CarDto CreateCar(CarDto car)
    {
        if (car.Code.IsNullOrEmpty()) 
        {
            throw new BadHttpRequestException("Required Fields [Code] has not been specified");
        }
        try
        {
            Car dbCar = new Car
            {

                Code = car.Code!,
                Vin= car.Vin,
                Brand = car.Brand,
                Min = car.Min,
                Max = car.Max,
                Produced = car.Produced,
                Bought = car.Bought,
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Car")!
            };
            _context.Cars.Add(dbCar);
            _context.SaveChanges();
            return _mapper.Map<CarDto>(dbCar);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public CarDto UpdateCar(CarDto car)
    {
        Car? dbCar = _context.Cars.FirstOrDefault(m => m.Id == car.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car {car.Id} has not been found");
        }

        dbCar.Code = car.Code!;
        dbCar.Vin= car.Vin;
        dbCar.Brand = car.Brand;
        dbCar.Min = car.Min;
        dbCar.Max = car.Max;
        dbCar.Produced = car.Produced;
        dbCar.Bought = car.Bought;
        _context.SaveChangesAsync();
        return _mapper.Map<CarDto>(dbCar);    
    }

    public List<IdDto> DeleteMultipleCar(List<IdDto> cars)
    {
        if(cars.Count >0)
        {
            foreach (var car in cars)
            {
                Car? dbCar = _context.Cars.Include(m=>m.Documents).FirstOrDefault(e => e.Id == car.Id);
                if (dbCar != null)
                {
                    _context.Cars.Remove(dbCar);
                }
            }

            _context.SaveChanges();
        }
        return cars;
    }
    public CarDto ColManAddToHouse(CarDto car, IdDto owner)
    {
        if (car.Id == null)
        {
            throw new BadHttpRequestException("No car ID has been specified");
        }
        Car? dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (dbCar is null)
        {
            throw new BadHttpRequestException($"Car #{car.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("House ID has not been specified");
        }
        var dbHouse = _context.Houses.Include(e=>e.Cars).FirstOrDefault(e => e.Id == owner.Id);
        if (dbHouse == null)
        {
            throw new BadHttpRequestException($"House #{owner.Id} doest not exist");
        }

        if (dbHouse.Cars != null && dbHouse.Cars.Any(c => c.Id == dbCar.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed house #{dbHouse.Id}");
        }

        if (dbHouse.Cars == null)
        {
            dbHouse.Cars = new List<Car>();
        }
        dbHouse.Cars.Add(dbCar);
        _context.SaveChanges();
        return _mapper.Map<CarDto>(dbCar);
    }
    public CarDto ColManAddToDocument(CarDto car, IdDto owner)
    {
        if (car.Id == null)
        {
            throw new BadHttpRequestException("No car ID has been specified");
        }
        Car? dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (dbCar is null)
        {
            throw new BadHttpRequestException($"Car #{car.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Document ID has not been specified");
        }
        var dbDocument = _context.Documents.Include(e=>e.Cars).FirstOrDefault(e => e.Id == owner.Id);
        if (dbDocument == null)
        {
            throw new BadHttpRequestException($"Document #{owner.Id} doest not exist");
        }

        if (dbDocument.Cars != null && dbDocument.Cars.Any(c => c.Id == dbCar.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed house #{dbDocument.Id}");
        }

        if (dbDocument.Cars == null)
        {
            dbDocument.Cars = new List<Car>();
        }
        dbDocument.Cars.Add(dbCar);
        _context.SaveChanges();
        return _mapper.Map<CarDto>(dbCar);
    }
    public CarDto ColManAddToCompany(CarDto car, IdDto owner)
    {
        if (car.Id == null)
        {
            throw new BadHttpRequestException("No car ID has been specified");
        }
        Car? dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (dbCar is null)
        {
            throw new BadHttpRequestException($"Car #{car.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Company ID has not been specified");
        }
        Company? dbCompany = _context.Companies.Include(e=>e.Cars).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException($"Company #{owner.Id} doest not exist");
        }

        if (dbCompany.Cars != null && dbCompany.Cars.Any(c => c.Id == dbCar.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed house #{dbCompany.Id}");
        }

        if (dbCompany.Cars == null)
        {
            dbCompany.Cars = new List<Car>();
        }
        dbCompany.Cars.Add(dbCar);
        _context.SaveChanges();
        return _mapper.Map<CarDto>(dbCar);
    }
    public CarDto ColManAddToPassenger(CarDto car, IdDto owner)
    {
        if (car.Id == null)
        {
            throw new BadHttpRequestException("No car ID has been specified");
        }
        Car? dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (dbCar is null)
        {
            throw new BadHttpRequestException($"Car #{car.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Passenger ID has not been specified");
        }
        var dbPassenger = _context.Employees.Include(e=>e.PassengerCars).FirstOrDefault(e => e.Id == owner.Id);
        if (dbPassenger == null)
        {
            throw new BadHttpRequestException($"Passenger #{owner.Id} doest not exist");
        }

        if (dbPassenger.PassengerCars != null && dbPassenger.PassengerCars.Any(c => c.Id == dbCar.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed passenger #{dbPassenger.Id}");
        }

        if (dbPassenger.PassengerCars == null)
        {
            dbPassenger.PassengerCars = new List<Car>();
        }
        dbPassenger.PassengerCars.Add(dbCar);
        _context.SaveChanges();
        return _mapper.Map<CarDto>(dbCar);
    }
    public CarDto ColManAddToDriver(CarDto car, IdDto owner)
    {
        if (car.Id == null)
        {
            throw new BadHttpRequestException("No car ID has been specified");
        }
        Car? dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (dbCar is null)
        {
            throw new BadHttpRequestException($"Car #{car.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Passenger ID has not been specified");
        }
        var dbDriver = _context.Employees.Include(e=>e.DriverCars).FirstOrDefault(e => e.Id == owner.Id);
        if (dbDriver == null)
        {
            throw new BadHttpRequestException($"Driver #{owner.Id} doest not exist");
        }

        if (dbDriver.DriverCars != null && dbDriver.DriverCars.Any(c => c.Id == dbCar.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed driver #{dbDriver.Id}");
        }

        if (dbDriver.DriverCars == null)
        {
            dbDriver.DriverCars = new List<Car>();
        }
        dbDriver.DriverCars.Add(dbCar);
        _context.SaveChanges();
        return _mapper.Map<CarDto>(dbCar);
    }
    public List<IdDto> ColManDeleteMultipleFromPassenger(List<IdDto> collection, IdDto owner)
    {
        Employee? passenger = _context.Employees
            .Include(e=>e.PassengerCars)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (passenger == null)
        {
            throw new BadHttpRequestException($"Invalid passenger #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == com.Id);
                if (car != null && passenger.PassengerCars is {Count:>0})
                {
                    passenger.PassengerCars.Remove(car);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    
    public List<IdDto> ColManDeleteMultipleFromDriver(List<IdDto> collection, IdDto owner)
    {
        Employee? driver = _context.Employees
            .Include(e=>e.DriverCars)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (driver == null)
        {
            throw new BadHttpRequestException($"Invalid driver #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == com.Id);
                if (car != null && driver.DriverCars is {Count:>0})
                {
                    driver.DriverCars.Remove(car);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner)
    {
        Company? company = _context.Companies
            .Include(e=>e.Cars)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == com.Id);
                if (car != null && company.Cars is {Count:>0})
                {
                    company.Cars.Remove(car);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromHouse(List<IdDto> collection, IdDto owner)
    {
        House? house = _context.Houses
            .Include(e=>e.Cars)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (house == null)
        {
            throw new BadHttpRequestException($"Invalid house #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == com.Id);
                if (car != null && house.Cars is {Count:>0})
                {
                    house.Cars.Remove(car);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner)
    {
        _Document? document = _context.Documents
            .Include(e=>e.Cars)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (document == null)
        {
            throw new BadHttpRequestException($"Invalid document #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == com.Id);
                if (car != null && document.Cars is {Count:>0})
                {
                    document.Cars.Remove(car);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
}