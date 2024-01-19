using AntaresApi.Dto.Common;
using AntaresApi.Dto.Position;

namespace AntaresApi.Services.Interfaces;

public interface IPositionService
{
    IEnumerable<PositionLiteDto> GetAllPosition();
    PositionDto GetPositionById(long id);     
    PositionDto DeletePosition(long id);
    List<IdDto> DeleteMultiplePosition(List<IdDto> positions);
    PositionDto ColManAddToCompany(PositionDto position, IdDto owner);
    PositionDto ColManAddToEmployee(PositionDto position, IdDto owner);
    PositionDto CreatePosition(PositionDto positions);
    PositionDto UpdatePosition(PositionDto positions);
    List<IdDto> ColDeleteMultipleFromEmployee(List<IdDto> collection, IdDto owner);
    PositionDto MovePositionToChildrenOfPosition(PositionDto positionFrom, PositionDto positionTo);
}