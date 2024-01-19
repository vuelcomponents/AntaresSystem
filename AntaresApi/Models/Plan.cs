using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace AntaresApi.Models;

public class Plan
{
    [Key]
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Class { get; set; }
    public required DateTimeOffset Start { get; set; }
    public required DateTimeOffset Stop { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
    public required Company Company { get; set; }
    public long? EmployeeId { get; set; }
    public long? CompanyId { get; set; }
}