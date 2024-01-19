using AntaresApi.Dto.Common;
using AntaresApi.Dto.Offer;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Dto.Position;

public class PositionDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public IdCodeDto? PositionUnit { get; set; }
    public List<EmployeeDto>? Employees { get; set; }
    public List<RealisationDto>? Requirements { get; set; }
    public int? EmploymentQty { get; set; }
    public CompanyDto? Company { get; set; }
    public long? CompanyId { get; set; }
    public List<PositionDto>? Children { get; set; }
    public PositionDto? Parent { get; set; }
    public long? ParentId { get; set; }
    public double? Demand { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<OfferDto>? Offers { get; set; }
}