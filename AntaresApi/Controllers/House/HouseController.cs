using AntaresApi.Dto.Common;
using AntaresApi.Dto.House;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.House;

[ApiController]
[Route("api/house")]
public class HouseController : ControllerBase
{
    private readonly IHouseService _houseService;

    public HouseController(IHouseService houseService)
    {
        _houseService = houseService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<HouseDto>> GetAllHouse()
    {
        try
        {
            return Ok(_houseService.GetAllHouses());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<HouseDto> GetHouseById(long id)
    {
        try
        {
            return Ok(_houseService.GetHouseById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    [HttpPost("create")]
    public ActionResult<HouseDto> CreateHouse(HouseDto house)
    {
        try
        {
            return Ok(_houseService.CreateHouse(house));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<HouseDto> UpdateHouse(HouseDto house)
    {
        try
        {
            return Ok(_houseService.UpdateHouse(house));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleHouse(List<IdDto> houses)
    {
        try
        {
            return Ok(_houseService.DeleteMultipleHouse(houses));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleHouse(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "car":
                    return Ok(_houseService.ColManDeleteMultipleFromCar(collection.Collection, collection.Owner));
                case "employee":
                    return Ok(_houseService.ColManDeleteMultipleFromEmployee(collection.Collection, collection.Owner));
                case "company":
                    return Ok(_houseService.ColManDeleteMultipleFromCompany(collection.Collection, collection.Owner));
                case "document":
                    return Ok(_houseService.ColManDeleteMultipleFromDocument(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create-colman")]
    public ActionResult<HouseDto> ColManCreateHouse(ColHouseDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "car":
                    return Ok(_houseService.ColManAddToCar(single.Object, single.Owner));
                case "employee":
                    return Ok(_houseService.ColManAddToEmployee(single.Object, single.Owner));
                case "company":
                    return Ok(_houseService.ColManAddToCompany(single.Object, single.Owner));
                case "document":
                    return Ok(_houseService.ColManAddToDocument(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}