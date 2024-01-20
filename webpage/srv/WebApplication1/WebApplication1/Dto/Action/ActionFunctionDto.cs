using WebApplication1.Dto.Document;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Mail;
using WebApplication1.Dto.Position;
using WebApplication1.Dto.Variant.Realisation;

namespace WebApplication1.Dto.Action;

public class ActionFunctionDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public SystemFunctionDto? SystemFunction { get; set; }
    public string? Description { get; set; }
    public List<RealisationDto>? Requirements { get; set; }
    public CompanyDto? Company { get; set; }
    public MailDto? Mail { get; set; }
    public PositionDto? Position { get; set; }
}