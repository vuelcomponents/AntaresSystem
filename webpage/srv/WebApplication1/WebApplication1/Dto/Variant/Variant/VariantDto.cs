using WebApplication1.Dto.Recruitment;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Variant.VariantRealisation;
using WebApplication1.Dto.Variant.VariantType;

namespace WebApplication1.Dto.Variant.Variant;

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