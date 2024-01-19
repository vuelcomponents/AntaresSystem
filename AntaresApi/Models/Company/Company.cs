using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;


namespace AntaresApi.Models;

public class Company
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string? HouseAndLocalNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    
    public List<Realisation>? Realisations { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<_Document>? Documents { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public Status.Status? Status { get; set; }
    public long? StatusId { get; set; }
    public List<Position.Position>? Positions { get; set; }
    public List<House.House>? Houses { get; set; }
    public List<Car.Car>? Cars { get; set; }
    public List<Offer.Offer>? Offers { get; set; }
    public List<Recruitment.Recruitment>? Recruitments { get; set; }
    public List<Plan>? Plans { get; set; }
}