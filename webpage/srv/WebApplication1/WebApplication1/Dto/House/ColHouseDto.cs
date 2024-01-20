using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.House;

public class ColHouseDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public HouseDto Object { get; set; }
}