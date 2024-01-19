namespace AntaresApi.Dto.Company;

public class PersonnelAdviseContainer
{
    public EmployeeSuperShortDto Employee { get; set; }
    public CompanyShortDto Company { get; set; }
    public List<PersonnelAdvise> Advises { get; set; }
}