using WebApplication1.Dto.Car;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.House;
using WebApplication1.Dto.Offer;
using WebApplication1.Dto.Status;
using WebApplication1.Dto.Variant.Realisation;

namespace WebApplication1.Dto.Document;

public class _DocumentDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? FileName { get; set; }
    public string? FileData { get; set; }
    public DateTime UploadDate { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<IdCodeDto>? Companies { get; set; }
    public List<EmployeeShortDto>? Employees { get; set; }
    public List<RealisationDto>? Realisations { get; set; }
    public StatusDto? Status { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public _DocumentTypeDto? DocumentType { get; set; }
    public List<HouseDto>? Houses { get; set; }
    public List<CarDto>? Cars { get; set; }
    public List<OfferDto>? Offers { get; set; }
}