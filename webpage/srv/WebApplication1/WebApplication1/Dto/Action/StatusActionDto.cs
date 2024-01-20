using WebApplication1.Dto.Status;


namespace WebApplication1.Dto.Action;

public class StatusActionDto
{
    public long? Id { get; set; }
    public List<StatusDto>? Statuses { get;set; }
    public ActionFunctionDto? ActionFunction { get; set; }
    public StatusActionTriggerDto? StatusActionTrigger { get; set; }

}