using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Recruitment;

namespace AntaresApi.Models.Document;

public class _DocumentType
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<_Document>? Documents { get; set; }
    public List<Recruitment.Recruitment>? Recruitments { get; set; }
}