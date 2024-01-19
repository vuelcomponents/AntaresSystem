using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.Position;

public class ColPositionDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public PositionDto Object { get; set; }
}