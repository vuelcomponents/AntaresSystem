using AntaresApi.Dto.Common;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Dto.Variant.VariantRealisation;
using AntaresApi.Dto.Variant.VariantType;
using AntaresApi.Models;
using AntaresApi.Models.StoreModel;

namespace AntaresApi.Dto.Variant.Variant;

public class VariantDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public VariantTypeDto? VariantType { get; set; }
    public IEnumerable<VariantRealisationDto>? VariantRealisations { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public bool? Global { get; set; }
    public List<RecruitmentDto>? Recruitments { get; set; }
}