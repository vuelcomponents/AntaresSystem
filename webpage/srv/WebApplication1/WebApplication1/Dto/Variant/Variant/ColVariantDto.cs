using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Variant.Variant;

public class ColVariantDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public VariantDto Object { get; set; }
    public string? Identifier { get; set; }
}