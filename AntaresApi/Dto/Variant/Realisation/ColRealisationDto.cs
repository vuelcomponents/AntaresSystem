using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.Variant.Realisation;

public class ColRealisationDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public RealisationDto Object { get; set; }
}