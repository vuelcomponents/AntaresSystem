using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.Offer;

public class Offer
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public Company Company { get; set; }
    public Position.Position? Position { get; set; }
    public _Document? Image { get; set; }
    public Recruitment.Recruitment? Recruitment { get; set; }
    public bool? Global { get; set; }
}