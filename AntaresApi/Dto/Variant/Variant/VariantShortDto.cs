using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant.VariantType;

namespace AntaresApi.Dto.Variant.Variant;

public class VariantShortDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public VariantTypeDto? VariantType { get; set; }
    public IdCodeDto? StoreModel { get; set; }
}