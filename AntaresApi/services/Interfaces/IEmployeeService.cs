using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Dto.House;

namespace AntaresApi.Services.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAllEmployee();
    EmployeeDto GetEmployeeById(long id);
    EmployeeDto CreateEmployee(EmployeeDto employee);
    Task<EmployeeDto> UpdateEmployee(EmployeeDto employee);
    long DeleteEmployee(long id);
    List<IdDto> DeleteMultipleEmployee(List<IdDto> employees);
    EmployeeDto ColManAddToDocument(EmployeeDto employee, IdDto owner);
    EmployeeDto ColManAddToPosition(EmployeeDto employee, IdDto owner);
    EmployeeDto ColManAddToCompany(EmployeeDto employee, IdDto owner);
    EmployeeDto ColManAddToPassengerCars(EmployeeDto employee, IdDto owner);
    EmployeeDto ColManAddToDriverCars(EmployeeDto employee, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromDriverCars(List<IdDto> collection, IdDto owner);
    Task<EmployeeDto> ColManAddToRecruitment(EmployeeDto employee, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromRecruitment(List<IdDto> collection, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromPassengerCars(List<IdDto> collection, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromPosition(List<IdDto> collection, IdDto owner);
    EmployeeDto ColManAddToHouse(EmployeeDto house, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromHouse(List<IdDto> collection, IdDto owner);
    PersonnelAdviseContainerWithColumns AdviseToEmployee(IdDto employee);
    PersonnelAdvise AdviseToEmployeeForSpecificPosition(IdDto employee, IdDto reqPosition);
    Task<EmployeeSuperShortDto> Hire(IdDto employee);
    Task<EmployeeSuperShortDto> Fire(IdDto employee);
}