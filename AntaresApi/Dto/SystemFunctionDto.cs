using AntaresApi.Dto.Common;

namespace AntaresApi.Dto;

public class SystemFunctionDto
{
    public long? Id { get; set; }   
    public string? Code { get; set; }
    public List<IdCodeDto>? StoreModels { get; set; }
}