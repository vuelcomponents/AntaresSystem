using AntaresApi.Dto.Common;

namespace AntaresApi.Services.Interfaces;

public interface IPositionUnitService
{
    IEnumerable<IdCodeDto> GetAllPositionUnit();
    IdCodeDto GetPositionUnitById(long id);
}