using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Settings;

public class FirstRun
{
    [Key]
    public int Id { get; set; }
}