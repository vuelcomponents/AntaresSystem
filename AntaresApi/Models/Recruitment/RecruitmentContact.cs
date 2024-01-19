using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.Recruitment;

public class RecruitmentContact
{
    [Key]
    public long Id { get; set; }
    public bool Done { get; set; } = false;
    public int? Rating { get; set; } 
    public DateTime? DateTime { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }

}