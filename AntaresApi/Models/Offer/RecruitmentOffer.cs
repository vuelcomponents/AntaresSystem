namespace AntaresApi.Models.Offer;

public class RecruitmentOffer
{
    public long Id { get; set; }
    public Offer Offer { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    // public Recruitment Recruitment {get;set;}
    
    
    
}