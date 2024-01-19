using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Services;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Employee;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployee()
    {
        try
        {
            return Ok(_employeeService.GetAllEmployee());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<EmployeeDto> GetEmployeeById(long id)
    {
        try
        {
            return Ok(_employeeService.GetEmployeeById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<EmployeeDto> CreateEmployee(EmployeeDto employee)
    {
        try
        {
            return Ok(_employeeService.CreateEmployee(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("update")]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(EmployeeDto company)
    {
        try
        {
            return Ok( await _employeeService.UpdateEmployee(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<EmployeeDto> DeleteEmployee(long id)
    {
        try
        {
            return Ok(_employeeService.DeleteEmployee(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleEmployee(List<IdDto> employees)
    {
        try
        {
            return Ok(_employeeService.DeleteMultipleEmployee(employees));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleEmployee(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "document":
                    return Ok(_employeeService.ColManDeleteMultipleFromDocument(collection.Collection, collection.Owner));
                case "position":
                    return Ok(_employeeService.ColManDeleteMultipleFromPosition(collection.Collection, collection.Owner));
                case "company":
                    return Ok(_employeeService.ColManDeleteMultipleFromCompany(collection.Collection, collection.Owner));
                case "house":
                    return Ok(_employeeService.ColManDeleteMultipleFromHouse(collection.Collection, collection.Owner));
                case "recruitment":
                    return Ok(_employeeService.ColManDeleteMultipleFromRecruitment(collection.Collection, collection.Owner));
                case "car":
                    switch (collection.Identifier)
                    {
                        case "passengers":
                            return Ok(_employeeService.ColManDeleteMultipleFromPassengerCars(collection.Collection, collection.Owner));
                        case "drivers":
                            return Ok(_employeeService.ColManDeleteMultipleFromDriverCars(collection.Collection, collection.Owner));
                        default:
                            throw new BadHttpRequestException("Identifier(collectionKey) has not been provided");
                    }
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create-colman")]
    public async Task<ActionResult<EmployeeDto>> ColManCreateEmployee(ColEmployeeDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "document":
                    return Ok(_employeeService.ColManAddToDocument(single.Object, single.Owner));
                case "position":
                    return Ok(_employeeService.ColManAddToPosition(single.Object, single.Owner));
                case "company":
                    return Ok(_employeeService.ColManAddToCompany(single.Object, single.Owner));
                case "house":
                    return Ok(_employeeService.ColManAddToHouse(single.Object, single.Owner));
                case "recruitment":
                    return Ok(await _employeeService.ColManAddToRecruitment(single.Object, single.Owner));
                case "car":
                    switch (single.Identifier)
                    {
                        case "passengers":
                            return Ok(_employeeService.ColManAddToPassengerCars(single.Object, single.Owner));
                        case "drivers":
                            return Ok(_employeeService.ColManAddToDriverCars(single.Object, single.Owner));
                        default:
                            throw new BadHttpRequestException("Identifier(collectionKey) has not been provided");
                    }
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("advise")]
    public ActionResult<PersonnelAdviseContainerWithColumns> Advise(IdDto employee)
    {
        try
        {
            return Ok(_employeeService.AdviseToEmployee(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("advise-to-specific-company/{companyId}")]
    public ActionResult<PersonnelAdvise> Advise(IdDto employee, long companyId)
    {
        try
        {
            return Ok(_employeeService.AdviseToEmployeeForSpecificPosition(employee, new IdDto{Id=companyId}));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("hire")]
    public async Task<ActionResult<EmployeeSuperShortDto>> Hire(IdDto employee)
    {
        try
        {
            return Ok(await _employeeService.Hire(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("fire")]
    public async Task<ActionResult<EmployeeSuperShortDto>> Fire(IdDto employee)
    {
        try
        {
            return Ok(await _employeeService.Fire(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}