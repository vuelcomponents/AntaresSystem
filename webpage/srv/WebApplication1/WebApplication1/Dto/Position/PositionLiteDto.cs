using WebApplication1.Dto.Common;
using WebApplication1.Dto.Offer;
using WebApplication1.Dto.Variant.Realisation;

namespace WebApplication1.Dto.Position;

public class PositionLiteDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public IdCodeDto? PositionUnit { get; set; }
    public List<EmployeeSuperShortDto>? Employees { get; set; }
    public List<RealisationDto>? Requirements { get; set; }
    public int? EmploymentQty { get; set; }
    public CompanyShortDto? Company { get; set; }
    public long? CompanyId { get; set; }
    public List<PositionLiteDto>? Children { get; set; }
    public PositionLiteDto? Parent { get; set; }
    public long? ParentId { get; set; }
    public double? Demand { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<OfferShortDto>? Offers { get; set; }
}