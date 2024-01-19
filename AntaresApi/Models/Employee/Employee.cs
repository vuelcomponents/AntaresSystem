using System.ComponentModel.DataAnnotations;
using AntaresApi.Controllers.Recruitment;
using AntaresApi.Models.Document;
using AntaresApi.Models.Recruitment;

namespace AntaresApi.Models;

public class Employee
{
    
    [Key]
    public long Id { get; set; }

    public bool? Verified { get; set; } = false;
    public string? VerifyToken { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
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
    public List<Realisation>? Realisations { get; set; }
    public List<Company>? Companies { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public List<_Document>? Documents { get; set; }
    public Status.Status? Status { get; set; }
    public string? Bsn { get; set; }
    public string? Pesel { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public long? StatusId { get; set; }
    public Status.Status? PreviousStatus { get; set; }
    public long? PreviousStatusId { get; set; }
    public List<Position.Position>? Positions { get; set; }
    public List<House.House>? Houses { get; set; }
    public List<Car.Car>? PassengerCars { get; set; }
    public List<Car.Car>? DriverCars { get; set; }
    public Recruitment.Recruitment? Recruitment { get; set; }
    public long? RecruitmentId { get; set; }
    public RecruitmentContact? RecruitmentContact { get; set; }
    public long? RecruitmentContactId { get; set; }
    public List<Plan>? Plans { get; set; }

}