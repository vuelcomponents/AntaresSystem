

using WebApplication1.Dto.Common;
using WebApplication1.Dto.Document;
using WebApplication1.Dto.Offer;
using WebApplication1.Dto.Status;
using WebApplication1.Dto.Variant.Variant;

namespace WebApplication1.Dto.Recruitment;

public class RecruitmentDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public OfferDto? Offer { get; set; }
    public long? OfferId {get; set; }
    public bool? Open { get; set; }

    public List<EmployeeDto>? Employees { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<VariantDto>? Variants { get; set; }
    public List<_DocumentTypeDto>? DocumentTypes { get; set; }
    public StatusDto? Status { get; set; }
    public long? StatusId { get; set; }
}