using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.StatusAction;

public class StatusActionTrigger
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    
}