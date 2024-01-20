using WebApplication1.Dto.Common;
using WebApplication1.Dto.Variant.VariantType;

namespace WebApplication1.Dto.Variant.Variant;

public class VariantShortDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public VariantTypeDto? VariantType { get; set; }
    public IdCodeDto? StoreModel { get; set; }
}