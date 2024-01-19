using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.House;

public class ColHouseDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public HouseDto Object { get; set; }
}