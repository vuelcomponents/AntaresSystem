using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.Position;

public class PositionUnit
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
}