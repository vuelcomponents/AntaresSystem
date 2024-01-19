using System.Collections;
using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Services;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Company;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    
    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<CompanyLiteDto>> GetAllCompany()
    {
        try
        {
            return Ok(_companyService.GetAllCompany());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<CompanyDto> GetCompanyById(long id)
    {
        try
        {
            return Ok(_companyService.GetCompanyById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<CompanyDto> CreateCompany(CompanyDto company)
    {
        try
        {
            return Ok(_companyService.CreateCompany(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<CompanyDto> UpdateCompany(CompanyDto company)
    {
        try
        {
            return Ok(_companyService.UpdateCompany(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<CompanyDto> DeleteCompany(long id)
    {
        try
        {
            return Ok(_companyService.DeleteCompany(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleCompany(List<IdDto> companies)
    {
        try
        {
            return Ok(_companyService.DeleteMultipleCompany(companies));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /** FUNKCJE TYLKO DLA MODELI BEDACYCH W CZYJEJS KOLEKCJII **/
    [HttpPost("create-colman")]
    public ActionResult<CompanyDto> ColManCreateCompany(ColCompanyDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "employee":
                    return Ok(_companyService.ColManAddToEmployee(single.Object, single.Owner));
                case "document":
                    return Ok(_companyService.ColManAddToDocument(single.Object, single.Owner));
                case "house":
                    return Ok(_companyService.ColManAddToHouse(single.Object, single.Owner));
                case "car":
                    return Ok(_companyService.ColManAddToCar(single.Object, single.Owner));
                
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleCompanyFromEmployee(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "employee":
                    return Ok(_companyService.ColDeleteMultipleCompanyFromEmployee(collection.Collection, collection.Owner));
                case "document":
                    return Ok(_companyService.ColDeleteMultipleCompanyFromDocument(collection.Collection,
                        collection.Owner));
                case "house":
                    return Ok(_companyService.ColDeleteMultipleCompanyFromHouse(collection.Collection, collection.Owner));
                case "car":
                    return Ok(_companyService.ColDeleteMultipleCompanyFromCar(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("advise")]
    public ActionResult<PersonnelAdviseContainerWithColumns> Advise(IdCodeDto company)
    {
        try
        {
            return Ok(_companyService.Advise(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}