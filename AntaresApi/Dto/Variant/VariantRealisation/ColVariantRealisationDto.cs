using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.Variant.VariantRealisation;

public class ColVariantRealisationDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public VariantRealisationDto Object { get; set; }
}