using WebApplication1.Dto.Common;

namespace WebApplication1.Dto;

public class SystemFunctionDto
{
    public long? Id { get; set; }   
    public string? Code { get; set; }
    public List<IdCodeDto>? StoreModels { get; set; }
}