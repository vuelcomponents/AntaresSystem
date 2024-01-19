using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;

namespace AntaresApi.Services.Interfaces;

public interface IActionFunctionService
{
    IEnumerable<ActionFunctionDto> GetAllActionFunctions();
    ActionFunctionDto GetActionFunctionById(long id);
    ActionFunctionDto CreateActionFunction(ActionFunctionDto actionFunction);
    ActionFunctionDto UpdateActionFunction(ActionFunctionDto company);
    List<IdDto> DeleteMultipleActionFunction(List<IdDto> companies);
}