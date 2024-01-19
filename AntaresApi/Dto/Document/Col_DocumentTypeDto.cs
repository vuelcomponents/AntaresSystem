using AntaresApi.Dto.Common;

namespace AntaresApi.Dto.Document;

public class Col_DocumentTypeDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public _DocumentTypeDto Object { get; set; }
}