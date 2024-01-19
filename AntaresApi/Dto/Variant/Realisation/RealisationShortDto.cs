using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Dto.Variant.VariantRealisation;

namespace AntaresApi.Dto.Variant.Realisation;

public class RealisationShortDto
{
    public long? Id { get; set; }
    public IdCodeDto? Variant { get; set; }
}