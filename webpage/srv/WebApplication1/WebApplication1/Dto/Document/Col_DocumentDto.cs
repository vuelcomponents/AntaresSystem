using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Document;

public class Col_DocumentDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public _DocumentDto Object { get; set; }
    public string? Identifier { get; set; }
}