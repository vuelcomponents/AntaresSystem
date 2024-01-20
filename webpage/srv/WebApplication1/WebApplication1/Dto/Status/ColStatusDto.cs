using WebApplication1.Dto.Common;
using WebApplication1.Dto.Status;

namespace WebApplication1.Dto;

public class ColStatusDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public StatusDto Object { get; set; }
}