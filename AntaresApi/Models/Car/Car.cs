using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.Car;

public class Car
{
    [Key] 
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Vin { get; set; }
    public string? Brand { get; set; }
    public DateTime? Produced { get; set; }
    public DateTime? Bought { get; set; }
    public List<Realisation>? Realisations { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public List<Employee>? Passengers { get; set; }
    public List<Employee>? Drivers { get; set; }
    public List<Company>? Companies { get; set; }
    public List<House.House>? Houses { get; set; }
    public List<_Document>? Documents { get; set; }
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