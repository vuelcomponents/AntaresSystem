namespace AntaresApi.Dto;

public class EmployeeShortDto
{
    public long? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
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
    public string? Pesel { get; set; }
    public string? Password { get; set; }
}