using WebApplication1.Dto.Car;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Document;
using WebApplication1.Dto.Variant.Realisation;

namespace WebApplication1.Dto.House;

public class HouseDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public List<RealisationDto>? Requirements { get; set; }
    public int? Max { get; set; }
    public int? Min { get; set; }
    public List<EmployeeDto>? Employees { get; set; }
    public List<CompanyDto>? Companies { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public string? HouseAndLocalNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public List<CarDto>? Cars { get; set; }
}