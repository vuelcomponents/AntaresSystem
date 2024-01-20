using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Recruitment;

namespace AntaresApi.Dto.Offer;

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
    public RecruitmentLiteDto? Recruitment { get; set; }
}