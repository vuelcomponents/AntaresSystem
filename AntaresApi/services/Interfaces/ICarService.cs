using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;

namespace AntaresApi.Services.Interfaces;

public interface ICarService
{
    IEnumerable<CarDto> GetAllCars();
    CarDto GetCarById(long id);
    CarDto CreateCar(CarDto car);
    CarDto UpdateCar(CarDto car);
    List<IdDto> DeleteMultipleCar(List<IdDto> cars);
    CarDto ColManAddToHouse(CarDto car, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromHouse(List<IdDto> collection, IdDto owner);
    CarDto ColManAddToPassenger(CarDto employee, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromPassenger(List<IdDto> collection, IdDto owner);
    CarDto ColManAddToDriver(CarDto employee, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromDriver(List<IdDto> collection, IdDto owner);
    CarDto ColManAddToDocument(CarDto car, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner);
    CarDto ColManAddToCompany(CarDto car, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner);
}