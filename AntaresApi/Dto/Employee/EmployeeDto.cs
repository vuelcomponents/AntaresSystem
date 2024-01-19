using System.ComponentModel.DataAnnotations;
using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.House;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Dto.Status;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Dto;

public class EmployeeDto
{
    public long? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PrivatePhone { get; set; }
    public string? HouseAndLocalNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? SubHouseAndLocalNumber { get; set; }
    public string? SubStreetName { get; set; }
    public string? SubPostcode { get; set; }
    public string? SubCity { get; set; }
    public IEnumerable<RealisationDto>? Realisations { get; set; }
    public List<CompanyDto>? Companies { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
    public bool? Verified { get; set; } = false;
    public StatusDto? Status { get; set; }
    public long? StatusId { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<PositionDto>? Positions { get; set; }
    public List<HouseDto>? Houses { get; set; }
    public List<CarDto>? PassengerCars { get; set; }
    public List<CarDto>? DriverCars { get; set; }
    public string? Bsn { get; set; }
    public string? Password { get; set; }
    public string? Pesel { get; set; }
    public RecruitmentDto? Recruitment { get; set; }
    public long? RecruitmentId { get; set; }
    public PersonnelAdvise? TempAdvise { get; set; }
    public RecruitmentContactDto? RecruitmentContact { get; set; }
}