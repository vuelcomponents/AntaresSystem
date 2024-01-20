using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Variant.Realisation;

public class ColRealisationDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public RealisationDto Object { get; set; }
}