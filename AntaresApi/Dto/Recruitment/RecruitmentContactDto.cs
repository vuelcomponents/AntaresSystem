namespace AntaresApi.Dto.Recruitment;

public class RecruitmentContactDto
{
    public long? Id { get; set; }
    public bool? Done { get; set; } = false;
    public int? Rating { get; set; } 
    public DateTime? DateTime { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }
    public EmployeeSuperShortDto? Employee { get; set; }
    public long? EmployeeId { get; set; }
}