using AntaresApi.Dto.Common;
using AntaresApi.Dto.Status;
using AntaresApi.Models.StoreModel;

namespace AntaresApi.Dto.Action;

public class StatusActionDto
{
    public long? Id { get; set; }
    public List<StatusDto>? Statuses { get;set; }
    public ActionFunctionDto? ActionFunction { get; set; }
    public StatusActionTriggerDto? StatusActionTrigger { get; set; }

}