using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.Recruitment;

public class Recruitment
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public Offer.Offer? Offer { get; set; }
    public long? OfferId {get; set; }

    public StoreModel.StoreModel StoreModel { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<Variant>? Variants { get; set; }
    public bool Open { get; set; } = true;
    public List<_DocumentType>? DocumentTypes { get; set; }
    public Status.Status Status { get; set; }
    public long StatusId { get; set; }
}