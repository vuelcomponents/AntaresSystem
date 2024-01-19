using AntaresApi.Dto.Common;
using AntaresApi.Dto.Status;

namespace AntaresApi.Dto;

public class ColStatusDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public StatusDto Object { get; set; }
}