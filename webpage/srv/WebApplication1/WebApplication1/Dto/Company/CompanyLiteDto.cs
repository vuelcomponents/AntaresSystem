using WebApplication1.Dto.Common;
using WebApplication1.Dto.Variant.Realisation;

namespace WebApplication1.Dto.Company;

public class CompanyLiteDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? HouseAndLocalNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<RealisationDto>? Realisations { get; set; }
    public List<EmployeeShortDto>? Employees { get; set; }
    public List<IdCodeDto>? Documents { get; set; }
    public IdCodeDto? Status { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<IdCodeDto>? Positions { get; set; }
    public List<IdCodeDto>? Houses { get; set; }
    public List<IdCodeDto>? Cars { get; set; }
    public List<IdCodeDto>? Offers { get; set; }
    public List<IdCodeDto>? Recruitments { get; set; }
}