using AntaresApi.Dto.Common;
using AntaresApi.Dto.Status;

namespace AntaresApi.Services.Interfaces;

public interface IStatusService
{
    IEnumerable<StatusDto> GetAllStatus();
    StatusDto GetStatusById(long id);
    StatusDto CreateStatus(StatusDto status);
    StatusDto UpdateStatus(StatusDto status);
    long DeleteStatus(long id);
    List<IdDto> DeleteMultipleStatuses(List<IdDto> statuses);
    StatusDto ColManAddToStatus(StatusDto transitionStatus, IdDto owner);
}