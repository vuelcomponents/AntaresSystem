using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.House;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Dto.Car;

public class CarDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Vin { get; set; }
    public string? Brand { get; set; }
    public DateTime? Produced { get; set; }
    public DateTime? Bought { get; set; }
    public List<RealisationDto>? Realisations { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<EmployeeShortDto>? Passengers { get; set; }
    public List<EmployeeShortDto>? Drivers { get; set; }
    public List<CompanyShortDto>? Companies { get; set; }
    public List<IdCodeDto>? Houses { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
    public string? TextData1 { get; set; }
    public string? TextData2 { get; set; }
    public string? TextData3 { get; set; }
    public string? TextData4 { get; set; }
    public int? Max { get; set; }
    public int? Min { get; set; }
    public int? IntData1 { get; set; }
    public int? IntData2 { get; set; }
    public double? DoubleData1 { get; set; }
    public double? DoubleData2 { get; set; }
    public DateTime? DateData1 { get; set; }
    public DateTime? DateData2 { get; set; }
}