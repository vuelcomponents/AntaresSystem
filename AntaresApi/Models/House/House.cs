using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.House;

public class House
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public List<Realisation>? Requirements { get; set; }
    public int? Max { get; set; }
    public int? Min { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<Company>? Companies { get; set; }
    public List<_Document>? Documents { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public string? HouseAndLocalNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public List<Car.Car>? Cars { get; set; }
}