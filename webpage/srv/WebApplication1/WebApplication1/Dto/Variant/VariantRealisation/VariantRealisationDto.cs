using WebApplication1.Dto.Variant.Variant;

namespace WebApplication1.Dto.Variant.VariantRealisation;

public class VariantRealisationDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public VariantDto? Variant { get; set; }
}