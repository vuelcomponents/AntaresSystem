using WebApplication1.Dto.Common;

namespace WebApplication1.Dto.Car;

public class ColCarDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public CarDto Object { get; set; }
    public string? Identifier { get; set; }
}