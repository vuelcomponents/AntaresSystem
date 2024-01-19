using System.ComponentModel.DataAnnotations;
using AntaresApi.Dto.Common;

namespace AntaresApi.Models;

public class Variant
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public VariantType VariantType { get; set; }
    public List<VariantRealisation>? VariantRealisations { get; set; }
    public StoreModel.StoreModel? StoreModel { get; set; }
    public bool? Global { get; set; }
    public List<Recruitment.Recruitment>? Recruitments { get; set; }
}