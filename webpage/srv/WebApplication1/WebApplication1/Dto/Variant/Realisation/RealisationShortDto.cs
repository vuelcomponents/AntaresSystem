using WebApplication1.Dto.Variant.Variant;
using WebApplication1.Dto.Variant.VariantRealisation;
using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Variant.Realisation;

public class RealisationShortDto
{
    public long? Id { get; set; }
    public IdCodeDto? Variant { get; set; }
}