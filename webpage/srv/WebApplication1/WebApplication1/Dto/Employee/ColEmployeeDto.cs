using WebApplication1.Dto.Common;

namespace WebApplication1.Dto;

public class ColEmployeeDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public EmployeeDto Object { get; set; }
    public string? Identifier { get; set; }
}