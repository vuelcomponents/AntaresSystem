namespace WebApplication1.Dto.Plan;

public class PlanDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Class { get; set; }
    public required DateTimeOffset Start { get; set; }
    public required DateTimeOffset Stop { get; set; }
    public List<EmployeeSuperShortDto> Employees { get; set; } = new List<EmployeeSuperShortDto>();
    public required CompanyShortDto Company { get; set; }
    public long? EmployeeId { get; set; }
    public long? CompanyId { get; set; }
}