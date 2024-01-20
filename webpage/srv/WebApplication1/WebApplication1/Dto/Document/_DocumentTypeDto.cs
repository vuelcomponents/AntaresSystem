using WebApplication1.Dto.Recruitment;

namespace WebApplication1.Dto.Document;

public class _DocumentTypeDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
    public List<RecruitmentDto>? Recruitments { get; set; }
}