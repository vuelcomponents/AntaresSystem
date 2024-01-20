using WebApplication1.Dto.Recruitment;
using WebApplication1.Dto.Car;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Document;
using WebApplication1.Dto.House;
using WebApplication1.Dto.Offer;
using WebApplication1.Dto.Position;
using WebApplication1.Dto.Status;
using WebApplication1.Dto.Variant.Realisation;


namespace WebApplication1.Dto;

public class CompanyDto
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
    public List<EmployeeDto>? Employees { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
    public StatusDto? Status { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<PositionDto>? Positions { get; set; }
    public List<HouseDto>? Houses { get; set; }
    public List<CarDto>? Cars { get; set; }
    public List<OfferDto>? Offers { get; set; }
    public List<RecruitmentDto>? Recruitments { get; set; }
}