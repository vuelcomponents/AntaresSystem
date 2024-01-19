using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Models;
using AntaresApi.Models.Document;
using AntaresApi.Models.StoreModel;

namespace AntaresApi.Dto.Status;

public class StatusDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public bool? Reserved { get; set; }
    public string? Description { get; set; }
    public List<StatusDto>? TransitionTo { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public int? Priority { get; set; }
    public EmployeeDto? Employee { get; set; }
    public _DocumentDto? Document { get; set; }
    public VariantDto? Variant { get; set; }
    public CompanyDto? Company { get; set; }
    public string? Color { get; set; }
    public List<StatusActionDto>? StatusActions { get; set; }
}