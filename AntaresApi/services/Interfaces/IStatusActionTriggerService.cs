using AntaresApi.Dto.Action;

namespace AntaresApi.Services.Interfaces;

public interface IStatusActionTriggerService
{
    IEnumerable<StatusActionTriggerDto> GetAllStatusActionTriggers();
    StatusActionTriggerDto GetStatusActionTriggerById(long id);
}