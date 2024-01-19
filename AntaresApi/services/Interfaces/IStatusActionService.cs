using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;

namespace AntaresApi.Services.Interfaces;

public interface IStatusActionService
{
    IEnumerable<StatusActionDto> GetAllStatusAction();
    StatusActionDto GetStatusActionById(long id);
    StatusActionDto CreateStatusAction(StatusActionDto employee);
    StatusActionDto UpdateStatusAction(StatusActionDto employee);
    long DeleteStatusAction(long id);
    List<IdDto> DeleteMultipleStatusAction(List<IdDto> employees);
    List<IdDto> ColDeleteMultipleStatusActionFromStatus(List<IdDto> collection, IdDto owner);
    StatusActionDto ColManAddToStatus(StatusActionDto statusAction, IdDto owner);
}