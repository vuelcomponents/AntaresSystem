using WebApplication1.Dto.Common;
using WebApplication1.Dto.Variant.Variant;
using WebApplication1.Dto.Variant.VariantRealisation;

namespace WebApplication1.Dto.Variant.Realisation;

public class RealisationDto
{
    public long? Id { get; set; }
    public VariantDto? Variant { get; set; }
    public VariantRealisationDto? VariantRealisation { get; set; }
    public double? NumericValue { get; set; }
    public DateTime? DateValue { get; set; }
    public IdDto? Employee { get; set; }
    public IdDto? Company { get; set; }
    public IdCodeDto? Comparator { get; set; }
}