using WebApplication1.Dto.Document;
using WebApplication1.Dto.Recruitment;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Position;

namespace WebApplication1.Dto.Offer;

public class OfferShortDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool? Global { get; set; }
    public CompanyShortDto? Company { get; set; }
    public IdCodeDto? Image { get; set; }
    public PositionShortDto? Position { get; set; }
}