using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.StoreModel;

public class StoreModel
{
    [Key]
    public long Id {get; set; }
    public string Code { get; set; }
    public List<SystemFunction>? SystemFunctions { get; set; }
}