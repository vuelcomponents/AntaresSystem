using AntaresApi.Dto.Common;
using AntaresApi.Dto.House;

namespace AntaresApi.Services.Interfaces;

public interface IHouseService
{
    IEnumerable<HouseDto> GetAllHouses();
    HouseDto GetHouseById(long id);
    HouseDto CreateHouse(HouseDto house);
    HouseDto UpdateHouse(HouseDto house);
    List<IdDto> DeleteMultipleHouse(List<IdDto> houses);
    HouseDto ColManAddToCar(HouseDto house, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromCar(List<IdDto> collection, IdDto owner);
    HouseDto ColManAddToEmployee(HouseDto house, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromEmployee(List<IdDto> collection, IdDto owner);
    HouseDto ColManAddToCompany(HouseDto house, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromCompany(List<IdDto> collection, IdDto owner);
    HouseDto ColManAddToDocument(HouseDto house, IdDto owner);
    List<IdDto> ColManDeleteMultipleFromDocument(List<IdDto> collection, IdDto owner);
}