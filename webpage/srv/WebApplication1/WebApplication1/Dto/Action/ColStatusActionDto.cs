using WebApplication1.Dto.Action;
using WebApplication1.Dto.Common;

namespace WebApplication1.Dto;

public class ColStatusActionDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public StatusActionDto Object { get; set; }
}