using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.Mail;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Dto.Action;

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