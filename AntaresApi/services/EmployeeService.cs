using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Status;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Position;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IActionResolver _resolver;
    public EmployeeService(DataContext context, IMapper mapper, IActionResolver resolver)
    {
        _context = context;
        _mapper = mapper;
        _resolver = resolver;
    }
    public IEnumerable<EmployeeDto> GetAllEmployee()
    {
        List<Employee> list = _context.Employees
                .Include(e=>e.Realisations)!
                .ThenInclude(r=>r.Variant)!
                .ThenInclude(v=>v.VariantRealisations)
                .Include(e=>e.Realisations)!
                .ThenInclude(r=>r.VariantRealisation)
                .Include(e=>e.Companies)
                .Include(e=>e.Documents)!
                .ThenInclude(d=>d.Companies)
                .Include(e=>e.Status)
                .ThenInclude(s=>s.TransitionTo)
                .Include(s=>s.StoreModel)
                .Include(e=>e.Positions)
                .Include(d=>d.PassengerCars)
                .Include(d=>d.DriverCars)
                .Include(e=>e.Houses)
                .ToList();
        return list.Select(_mapper.Map<EmployeeDto>).ToList();
    }

    public EmployeeDto GetEmployeeById(long id)
    {
        Employee? employee = _context.Employees!
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.Variant)!
            .ThenInclude(v=>v.VariantRealisations)
            .Include(e=>e.Realisations)!
            .ThenInclude(r=>r.VariantRealisation)
            .Include(e=>e.Companies)
            .Include(e=>e.Houses)
            .Include(e=>e.Documents)!
            .ThenInclude(d=>d.Companies)
            .Include(e=>e.Documents)!
            .ThenInclude(d=>d.DocumentType)
            .Include(e=>e.Status)
            .ThenInclude(s=>s.TransitionTo)
            .Include(s=>s.StoreModel)
            .Include(e=>e.Positions)
            .Include(d=>d.PassengerCars)
            .Include(d=>d.DriverCars)
            .Include(e=>e.Documents)!
            .ThenInclude(d=>d.DocumentType)
            .FirstOrDefault(e=> e.Id == id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"No employee #{id} has been found");
        }
        return _mapper.Map<EmployeeDto>(employee);
    }

    public EmployeeDto CreateEmployee(EmployeeDto employee)
    {
        if (employee.FirstName.IsNullOrEmpty() || employee.LastName.IsNullOrEmpty() || employee.Email.IsNullOrEmpty()) 
        {
            throw new BadHttpRequestException("Required Fields [FirsName, LastName, Email] has not been specified");
        }   
        Status? status = null;
        if (employee.Status != null)
        {
            status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == employee.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            
            if (status.StoreModel.Code != "Employee")
            {
                throw new Exception("Status is not destined to Employee");
            }
        }
        try
        {
            Employee dbEmployee = new Employee
            {

                FirstName = employee.FirstName!,
                LastName = employee.LastName!,
                DateOfBirth = employee.DateOfBirth,
                PrivatePhone = employee.PrivatePhone,
                HouseAndLocalNumber = employee.HouseAndLocalNumber,
                StreetName = employee.StreetName,
                PostCode = employee.PostCode,
                City = employee.City,
                SubHouseAndLocalNumber = employee.SubHouseAndLocalNumber,
                SubStreetName = employee.SubStreetName,
                SubPostcode = employee.SubPostcode,
                SubCity = employee.SubCity,
                Email = employee.Email!,
                Bsn = employee.Bsn,
                Pesel = employee.Pesel,
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee")!,
                Verified = true
            };
            if (status != null)
            {
                if (status.Reserved)
                {
                    throw new BadHttpRequestException("Status is reserved");
                }
                dbEmployee.Status = status;
            }
            _context.Employees.Add(dbEmployee);
            _context.SaveChanges();
            return _mapper.Map<EmployeeDto>(dbEmployee);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employee)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (employee.FirstName.IsNullOrEmpty() || employee.LastName.IsNullOrEmpty() || employee.Email.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [FirsName, LastName] has not been specified");
        }   
        Employee? dbEmployee = _context.Employees
                .Include(e=>e.Realisations)
                .Include(e=>e.Companies)
                .Include(e=>e.Status)
                .ThenInclude(s=>s.TransitionTo)
                .Include(s=>s.StoreModel)
                .Include(e=>e.Documents)
                .Include(e=>e.Positions)
                .Include(e=>e.Recruitment)
                .FirstOrDefault(e => e.Id.Equals(employee.Id));
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"There is no employee #{employee.Id}");
        }

        Status? status = null;
        if (employee.Status != null)
        {
            status = _context.Statuses
                .Include(s=>s.StoreModel)
                .Include(s=>s.StatusActions)
                .Include(s=>s.TransitionTo)
                .FirstOrDefault(s => s.Id == employee.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            if (status.StoreModel.Code != "Employee")
            {
                throw new Exception("Status is not destined to Employee");
            }
        }

        if (dbEmployee.Status != status)
        {
            if (dbEmployee.Recruitment != null)
            {
                throw new BadHttpRequestException(
                    $"This employee is already during recruitment process {dbEmployee.Recruitment.Code}! Status change forbidden");
            }
        }
        dbEmployee.StreetName = employee.StreetName;
        dbEmployee.City = employee.City;
        dbEmployee.PostCode = employee.PostCode;
        dbEmployee.FirstName = employee.FirstName!;
        dbEmployee.LastName = employee.LastName!;
        dbEmployee.PrivatePhone = employee.PrivatePhone;
        dbEmployee.DateOfBirth = employee.DateOfBirth;
        dbEmployee.SubStreetName = employee.SubStreetName;
        dbEmployee.HouseAndLocalNumber = employee.HouseAndLocalNumber;
        dbEmployee.SubHouseAndLocalNumber = employee.SubHouseAndLocalNumber;
        dbEmployee.SubCity = employee.SubCity;
        dbEmployee.SubPostcode = employee.SubPostcode;
        dbEmployee.Email = employee.Email!;
        
        if (status != null && status.Id != dbEmployee.Status?.Id)
        {
            Status? previousStatus = null;
            if (dbEmployee.Status != null)
            {
               previousStatus =  _context.Statuses.Include(s=>s.StatusActions).FirstOrDefault(s => s.Id == dbEmployee.Status.Id);
            }

            if (status.Reserved)
            {
                throw new BadHttpRequestException("Status is reserved");
            }
            
            await _resolver.ResolveStatusAction(previousStatus, status!, dbEmployee.Id);
            dbEmployee.PreviousStatusId = dbEmployee.StatusId;
        }
        
        dbEmployee.StatusId = status?.Id;
        
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }

    public long DeleteEmployee(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultipleEmployee(List<IdDto> employees)
    {
        if(employees.Count >0)
        {
            foreach (var emp in employees)
            {
                Employee? employee = _context.Employees
                    .Include(e=>e.Documents)
                    .Include(e=>e.Realisations)
                    .Include(e=>e.Companies)
                    .Include(e=>e.Positions)
                    .FirstOrDefault(e => e.Id == emp.Id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return employees;
    }
    public EmployeeDto ColManAddPassengerToCar(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Passengers).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        if (dbCar.Passengers != null && dbCar.Passengers.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed passenger #{dbEmployee.Id}");
        }

        if (dbCar.Passengers == null)
        {
            dbCar.Passengers = new List<Employee>();
        }
        dbCar.Passengers.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToDocument(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Document ID has not been specified");
        }
        var dbDocument = _context.Documents.Include(e=>e.Employees).FirstOrDefault(e => e.Id == owner.Id);
        if (dbDocument == null)
        {
            throw new BadHttpRequestException($"Document #{owner.Id} doest not exist");
        }

        if (dbDocument.Employees != null && dbDocument.Employees.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Document #{dbDocument.Id} already has signed employee #{dbEmployee.Id}");
        }

        if (dbDocument.Employees == null)
        {
            dbDocument.Employees = new List<Employee>();
        }
        dbDocument.Employees.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public async  Task<EmployeeDto> ColManAddToRecruitment(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Recruitment).FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        
        if (dbEmployee.Recruitment != null)
        {
            throw new BadHttpRequestException(
                $"This employee is already during recruitment process {dbEmployee.Recruitment.Code}");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Document ID has not been specified");
        }
        var dbRecruitment = _context.Recruitments.Include(e=>e.Employees).FirstOrDefault(e => e.Id == owner.Id);
        if (dbRecruitment == null)
        {
            throw new BadHttpRequestException($"Document #{owner.Id} doest not exist");
        }

        if (dbRecruitment.Employees != null && dbRecruitment.Employees.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Document #{dbRecruitment.Id} already has signed employee #{dbEmployee.Id}");
        }

        if (dbRecruitment.Employees == null)
        {
            dbRecruitment.Employees = new List<Employee>();
        }

        dbEmployee.PreviousStatusId = dbEmployee.StatusId;
        dbEmployee.StatusId = _context.Statuses.FirstOrDefault(s => s.Code == "Open Recruitment")!.Id;
        await _resolver.ResolveStatusAction(null, _context.Statuses.Include(s=>s.StatusActions).FirstOrDefault(s => s.Code == "Open Recruitment")!, dbEmployee.Id);
        dbEmployee.RecruitmentContact = new RecruitmentContact();
        dbRecruitment.Employees.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToHouse(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("House ID has not been specified");
        }
        var dbHouse = _context.Houses.Include(e=>e.Employees).FirstOrDefault(e => e.Id == owner.Id);
        if (dbHouse == null)
        {
            throw new BadHttpRequestException($"House #{owner.Id} doest not exist");
        }

        if (dbHouse.Employees != null && dbHouse.Employees.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Document #{dbHouse.Id} already has signed employee #{dbEmployee.Id}");
        }

        if (dbHouse.Employees == null)
        {
            dbHouse.Employees = new List<Employee>();
        }
        dbHouse.Employees.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToCompany(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Companies).FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Company ID has not been specified");
        }
        var dbCompany = _context.Companies.Include(e=>e.Employees).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException($"Company #{owner.Id} doest not exist");
        }

        if (dbCompany.Employees != null && dbCompany.Employees.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Company #{dbCompany.Id} already has signed employee #{dbEmployee.Id}");
        }

        if (dbCompany.Employees == null)
        {
            dbCompany.Employees = new List<Employee>();
        }
        dbCompany.Employees.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToPassengerCars(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No passenger ID has been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Companies).FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Passenger #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Passengers).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        if (dbCar.Passengers != null && dbCar.Passengers.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed passenger #{dbEmployee.Id}");
        }

        if (dbCar.Passengers == null)
        {
            dbCar.Passengers = new List<Employee>();
        }
        dbCar.Passengers.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToDriverCars(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No driver ID has been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.DriverCars).FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Driver #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Drivers).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        if (dbCar.Drivers != null && dbCar.Drivers.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed driver #{dbEmployee.Id}");
        }

        if (dbCar.Drivers == null)
        {
            dbCar.Drivers = new List<Employee>();
        }
        dbCar.Drivers.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    public EmployeeDto ColManAddToPosition(EmployeeDto employee, IdDto owner)
    {
        if (employee.Id == null)
        {
            throw new BadHttpRequestException("No employee ID has been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Companies).FirstOrDefault(c => c.Id == employee.Id);
        if (dbEmployee is null)
        {
            throw new BadHttpRequestException($"Employee #{employee.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Position ID has not been specified");
        }
        var dbPosition = _context.Positions.Include(p=>p.Company).Include(p=>p.PositionUnit).Include(e=>e.Employees).FirstOrDefault(e => e.Id == owner.Id);
        if (dbPosition == null)
        {
            throw new BadHttpRequestException($"Position #{owner.Id} doest not exist");
        }

        if (dbPosition.PositionUnit.Code != "Position")
        {
            throw new BadHttpRequestException("This unit type cannot owe employee");
        }

        if (!(dbEmployee.Companies!.Any(c => c.Id == dbPosition.Company.Id)))
        {
            throw new BadHttpRequestException("Company must hire this employee first");
        }
        
        if (dbPosition.Employees != null && dbPosition.Employees.Any(c => c.Id == dbEmployee.Id))
        {
            throw new BadHttpRequestException($"Position #{dbPosition.Id} already has signed employee #{dbEmployee.Id}");
        }

        if (dbPosition.Employees == null)
        {
            dbPosition.Employees = new List<Employee>();
        }
        dbPosition.Employees.Add(dbEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeDto>(dbEmployee);
    }
    
    public List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner)
    {
        _Document? document = _context.Documents
            .Include(e=>e.Employees)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (document == null)
        {
            throw new BadHttpRequestException($"Invalid document #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? employee = _context.Employees.FirstOrDefault(c => c.Id == com.Id);
                if (employee != null && document.Employees is {Count:>0})
                {
                    document.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromPassengerCars(List<IdDto> collection, IdDto owner)
    {
        Car? car = _context.Cars
            .Include(e=>e.Passengers)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? passenger = _context.Employees.FirstOrDefault(c => c.Id == com.Id);
                if (passenger != null && car.Passengers is {Count:>0})
                {
                    car.Passengers.Remove(passenger);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromDriverCars(List<IdDto> collection, IdDto owner)
    {
        Car? car = _context.Cars
            .Include(e=>e.Drivers)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? driver = _context.Employees.FirstOrDefault(c => c.Id == com.Id);
                if (driver != null && car.Drivers is {Count:>0})
                {
                    car.Drivers.Remove(driver);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromHouse(List<IdDto> collection, IdDto owner)
    {
        House? house = _context.Houses
            .Include(e=>e.Employees)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (house == null)
        {
            throw new BadHttpRequestException($"Invalid document #{owner.Id}");
        }
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? employee = _context.Employees.FirstOrDefault(c => c.Id == com.Id);
                if (employee != null && house.Employees is {Count:>0})
                {
                    house.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromRecruitment(List<IdDto> collection, IdDto owner)
    {
        Recruitment? recruitment = _context.Recruitments
            .Include(e=>e.Employees)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (recruitment == null)
        {
            throw new BadHttpRequestException($"Invalid recruitment #{owner.Id}");
        }
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? employee = _context.Employees
                    .FirstOrDefault(c => c.Id == com.Id);
                if (employee != null && recruitment.Employees is {Count:>0})
                {
                    employee.StatusId = employee.PreviousStatusId;
                    recruitment.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromPosition(List<IdDto> collection, IdDto owner)
    {
        Position? position = _context.Positions
            .Include(e=>e.Employees)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (position == null)
        {
            throw new BadHttpRequestException($"Invalid position #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? employee = _context.Employees.FirstOrDefault(c => c.Id == com.Id);
                if (employee != null && position.Employees is {Count:>0})
                {
                    position.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner)
    {
        Company? company = _context.Companies
            .Include(e=>e.Employees)
            .Include(e=>e.Positions)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                Employee? employee = _context.Employees.Include(e=>e.Positions).FirstOrDefault(c => c.Id == com.Id);
                if (employee == null)
                {
                    throw new BadHttpRequestException("Employee id invalid");
                }
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
                if (company.Employees is {Count:>0})
                {
                    company.Employees.Remove(employee);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    
    public PersonnelAdviseContainerWithColumns AdviseToEmployee(IdDto employee)
    {
    List<string> columns = new List<string>();        
    List < PersonnelAdviseContainer > personnelAdviseContainers = new List < PersonnelAdviseContainer > ();

    List<Company> ? dbCompanies = _context.Companies.Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.VariantRealisation).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Variant).ThenInclude(v => v.VariantType).Include(c => c.Employees).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Comparator).AsSplitQuery()
      .ToList();
    
    Employee? dbEmployee = _context.Employees.Include(e=>e.Positions)!.Include(r => r.Realisations) !.ThenInclude(r => r.VariantRealisation)!.Include(r => r.Realisations) !.ThenInclude(r => r.Variant)!.ThenInclude(v => v.VariantType).Include(e => e.Companies)!.Include(e => e.Positions)!.AsSplitQuery()
      .FirstOrDefault(e=>e.Id == employee.Id);
     if (dbEmployee == null)
     {
         throw new BadHttpRequestException("Employee not found");
     }
     foreach (var dbCompany in dbCompanies)
     {
         if (dbCompany.Positions is { Count: > 0 }) 
         {
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
            Position = _mapper.Map < PositionShortDto > (companyPosition),
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
              if (realisation == null) 
              {
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
                    if (realisation.NumericValue > requirement.NumericValue) 
                    {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                    } 
                    else 
                    {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add( _mapper.Map < RealisationShortDto > (requirement));
                    }
                  break;
                  case "<":
                    if (realisation.NumericValue < requirement.NumericValue) 
                    {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement));
                    } 
                    else 
                    {
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
                    if (realisation.NumericValue <= requirement.NumericValue) 
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
                  case "==":
                    if (realisation.NumericValue == requirement.NumericValue) 
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
                  }
                break;
                case "Date":
                  switch (requirement.Comparator!.Code) 
                  {
                  case ">":
                    if (realisation.DateValue > requirement.DateValue) 
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
                  case "<":
                    if (realisation.DateValue < requirement.DateValue) 
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
                  case ">=":
                    if (realisation.DateValue >= requirement.DateValue) 
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
                    if (realisation.DateValue <= requirement.DateValue) 
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
                  case "==":
                    if (realisation.DateValue == requirement.DateValue) 
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
                  }
                break;
                case "Variant Realisation":
                  if (realisation.VariantRealisation!.Id == requirement.VariantRealisation!.Id) 
                  {
                    advise.Chance++;
                    Percents(ref advise, max);
                    advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  } 
                  else 
                  {
                    advise.Loss++;
                    Percents(ref advise, max);
                    advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  }
                  break;
                }
              }
            }
          } 
          else 
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
 public PersonnelAdvise AdviseToEmployeeForSpecificPosition(IdDto employee, IdDto reqPosition)
    {
    List<string> columns = new List<string>();        
    List < PersonnelAdviseContainer > personnelAdviseContainers = new List < PersonnelAdviseContainer > ();

    List<Company> ? dbCompanies = _context.Companies
        .Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.VariantRealisation).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Variant).ThenInclude(v => v.VariantType).Include(c => c.Employees).Include(c => c.Positions) !.ThenInclude(p => p.Requirements) !.ThenInclude(r => r.Comparator).AsSplitQuery()
        .ToList();
    
    Employee? dbEmployee = _context.Employees.Include(e=>e.Positions)!.Include(r => r.Realisations) !.ThenInclude(r => r.VariantRealisation)!.Include(r => r.Realisations) !.ThenInclude(r => r.Variant)!.ThenInclude(v => v.VariantType).Include(e => e.Companies)!.Include(e => e.Positions)!.AsSplitQuery()
      .FirstOrDefault(e=>e.Id == employee.Id);
     if (dbEmployee == null)
     {
         throw new BadHttpRequestException("Employee not found");
     }
 
         var pos = _context.Positions
             .Include(r=>r.Requirements)
             .ThenInclude(r=>r.Variant)
             .ThenInclude(v=>v.VariantType)
             .Include(r=>r.Requirements)
             .ThenInclude(r=>r.VariantRealisation)
             .FirstOrDefault(p => p.Id == reqPosition.Id);
         if (pos == null)
         {
             throw new BadHttpRequestException("Position was not found");
         }
         
          PersonnelAdvise advise = new PersonnelAdvise 
          {
            Position = _mapper.Map < PositionShortDto > (pos),
          };
          int max = pos.Requirements!.Count;
          if (dbEmployee.Realisations is { Count: > 0 } && pos.Requirements is { Count: > 0 }) 
          {
            foreach(var requirement in pos.Requirements) 
            {
              Realisation ? realisation = dbEmployee.Realisations.FirstOrDefault(r => r.Variant.Id == requirement.Variant!.Id);
              if (realisation == null) 
              {
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
                    if (realisation.NumericValue > requirement.NumericValue) 
                    {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                    } 
                    else 
                    {
                      advise.Loss++;
                      Percents(ref advise, max);
                      advise.Unmatched!.Add( _mapper.Map < RealisationShortDto > (requirement));
                    }
                  break;
                  case "<":
                    if (realisation.NumericValue < requirement.NumericValue) 
                    {
                      advise.Chance++;
                      Percents(ref advise, max);
                      advise.Matched!.Add( _mapper.Map < RealisationShortDto > (requirement));
                    } 
                    else 
                    {
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
                    if (realisation.NumericValue <= requirement.NumericValue) 
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
                  case "==":
                    if (realisation.NumericValue == requirement.NumericValue) 
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
                  }
                break;
                case "Date":
                  switch (requirement.Comparator!.Code) 
                  {
                  case ">":
                    if (realisation.DateValue > requirement.DateValue) 
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
                  case "<":
                    if (realisation.DateValue < requirement.DateValue) 
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
                  case ">=":
                    if (realisation.DateValue >= requirement.DateValue) 
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
                    if (realisation.DateValue <= requirement.DateValue) 
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
                  case "==":
                    if (realisation.DateValue == requirement.DateValue) 
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
                  }
                break;
                case "Variant Realisation":
                  if (realisation.VariantRealisation!.Id == requirement.VariantRealisation!.Id) 
                  {
                    advise.Chance++;
                    Percents(ref advise, max);
                    advise.Matched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  } 
                  else 
                  {
                    advise.Loss++;
                    Percents(ref advise, max);
                    advise.Unmatched!.Add(_mapper.Map < RealisationShortDto > (requirement));
                  }
                  break;
                }
              }
            }
          }

          return advise;
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

    public async Task<EmployeeSuperShortDto> Hire(IdDto employee)
    {
        Employee? dbEmployee = _context.Employees
            .Include(e=>e.Positions)
            .Include(c=>c.Companies)
            .Include(e=>e.Recruitment)
            .ThenInclude(r=>r.Offer)
            .ThenInclude(o=>o.Company)
            .Include(e=>e.Recruitment)
            .ThenInclude(r=>r.Offer)
            .ThenInclude(o=>o.Position)
            .Include(e=>e.Recruitment)
            .ThenInclude(r=>r.Status)
            .FirstOrDefault(e => e.Id == employee.Id);
        if (dbEmployee == null)
        {
            throw new BadHttpRequestException("Employee has not been found");
        }
        if (dbEmployee.Recruitment == null)
        {
            throw new BadHttpRequestException("Employee does not correspond any recruitment process");
        }

        var recruitmentStatus = _context.Statuses.FirstOrDefault(s => s.Id == dbEmployee.Recruitment.Status.Id);
        if (recruitmentStatus == null)
        {
            throw new BadHttpRequestException("Status has not been found in recruitment");
        }
        ColManDeleteMultipleFromRecruitment(new List<IdDto> { new IdDto { Id = dbEmployee.Id } },
            new IdDto { Id = dbEmployee.Recruitment.Id });
        var mappedEmployee = _mapper.Map<EmployeeDto>(dbEmployee);
        mappedEmployee.Status = _mapper.Map<StatusDto>(recruitmentStatus);
        mappedEmployee.Status.Id = recruitmentStatus.Id;
        await UpdateEmployee(mappedEmployee);
        _context.SaveChanges();
        return _mapper.Map<EmployeeSuperShortDto>(dbEmployee);

    }

    public async Task<EmployeeSuperShortDto> Fire(IdDto employee)
    {
        Employee? dbEmployee = _context.Employees
            .Include(e => e.Positions)
            .Include(c => c.Companies)
            .Include(e => e.Recruitment)
            .ThenInclude(r => r.Offer)
            .ThenInclude(o => o.Company)
            .Include(e => e.Recruitment)
            .ThenInclude(r => r.Offer)
            .ThenInclude(o => o.Position)
            .Include(e => e.Recruitment)
            .ThenInclude(r => r.Status)
            .FirstOrDefault(e => e.Id == employee.Id);
        if (dbEmployee == null)
        {
            throw new BadHttpRequestException("Employee has not been found");
        }

        if (dbEmployee.Recruitment == null)
        {
            throw new BadHttpRequestException("Employee does not correspond any recruitment process");
        }
        ColManDeleteMultipleFromRecruitment(new List<IdDto> { new IdDto { Id = dbEmployee.Id } },
            new IdDto { Id = dbEmployee.Recruitment.Id });
        dbEmployee.StatusId = dbEmployee.PreviousStatusId;
        await _context.SaveChangesAsync();
        return _mapper.Map<EmployeeSuperShortDto>(dbEmployee);
    }
}