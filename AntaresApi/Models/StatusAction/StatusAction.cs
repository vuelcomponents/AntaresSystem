using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.StatusAction;

public class StatusAction
{
    [Key]
    public long Id { get; set; }
    public List<Status.Status>? Statuses { get;set; }
    public ActionFunction ActionFunction { get; set; }
    public StatusActionTrigger StatusActionTrigger { get; set; }
}