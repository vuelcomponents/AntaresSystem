using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Car;

[ApiController]
[Route("api/car")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService mail)
    {
        _carService = mail;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<CarDto>> GetAllCar()
    {
        try
        {
            return Ok(_carService.GetAllCars());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<CarDto> GetCarById(long id)
    {
        try
        {
            return Ok(_carService.GetCarById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    [HttpPost("create")]
    public ActionResult<CarDto> CreateCar(CarDto car)
    {
        try
        {
            return Ok(_carService.CreateCar(car));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<CarDto> UpdateCar(CarDto car)
    {
        try
        {
            return Ok(_carService.UpdateCar(car));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleCar(List<IdDto> cars)
    {
        try
        {
            return Ok(_carService.DeleteMultipleCar(cars));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleCar(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "document":
                    return Ok(_carService.ColManDeleteMultipleFromDocument(collection.Collection, collection.Owner));
                case "house":
                    return Ok(_carService.ColManDeleteMultipleFromHouse(collection.Collection, collection.Owner));
                case "company":
                    return Ok(_carService.ColManDeleteMultipleFromCompany(collection.Collection, collection.Owner));
                    
                case "employee":
                    switch (collection.Identifier)
                    {
                        case "passengerCars":
                            return Ok(_carService.ColManDeleteMultipleFromPassenger(collection.Collection, collection.Owner));
                        case "driverCars":
                            return Ok(_carService.ColManDeleteMultipleFromDriver(collection.Collection, collection.Owner));
                        default:
                            throw new BadHttpRequestException("Identifier has not been specified");
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
    public ActionResult<CarDto> ColManCreateHouse(ColCarDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "house":
                    return Ok(_carService.ColManAddToHouse(single.Object, single.Owner));
                case "document":
                    return Ok(_carService.ColManAddToDocument(single.Object, single.Owner));
                case "company":
                    return Ok(_carService.ColManAddToCompany(single.Object, single.Owner));
                case "employee":
                    switch (single.Identifier)
                    {
                        case "passengerCars":
                            return Ok(_carService.ColManAddToPassenger(single.Object, single.Owner));
                        case "driverCars":
                            return Ok(_carService.ColManAddToDriver(single.Object, single.Owner));
                        default:
                            throw new BadHttpRequestException("Identifier has not been specified");
                    }
                    
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}