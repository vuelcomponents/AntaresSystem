namespace WebApplication1.Dto.Common;

public class ColIDCollectionDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public List<IdDto> Collection { get; set; }
    public string? Identifier { get; set; }
}