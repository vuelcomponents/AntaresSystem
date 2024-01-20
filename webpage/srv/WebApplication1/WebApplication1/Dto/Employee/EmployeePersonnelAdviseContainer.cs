namespace WebApplication1.Dto.Employee;

public class EmployeePersonnelAdviseContainer
{
    public EmployeeSuperShortDto Employee { get; set; }
    public CompanyShortDto Company { get; set; }
    public List<EmployeePersonnelAdvise> Advises { get; set; }
}