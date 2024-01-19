using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models;

public class SystemFunction
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public List<StoreModel.StoreModel> StoreModels { get; set; }
}