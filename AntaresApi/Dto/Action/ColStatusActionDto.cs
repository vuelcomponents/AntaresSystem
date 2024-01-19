using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;

namespace AntaresApi.Dto;

public class ColStatusActionDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public StatusActionDto Object { get; set; }
}