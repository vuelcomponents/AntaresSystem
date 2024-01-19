using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models;

public class VariantRealisation
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public Variant? Variant { get; set; }
}