using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.Variant.Variant;

public class ColVariantDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public VariantDto Object { get; set; }
    public string? Identifier { get; set; }
}