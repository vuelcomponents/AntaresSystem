using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models;

public class Comparator
{
    [Key]
    public long? Id { get; set; }
    public string Code { get; set; }
}