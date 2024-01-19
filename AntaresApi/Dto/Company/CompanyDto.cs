using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.House;
using AntaresApi.Dto.Offer;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Dto.Status;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Models;

namespace AntaresApi.Dto;

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