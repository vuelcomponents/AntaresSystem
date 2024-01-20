using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Position;

public class ColPositionDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public PositionDto Object { get; set; }
}