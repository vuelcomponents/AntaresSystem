using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.Document;

public class _Document
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileData { get; set; }
    public DateTime UploadDate { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<Company>? Companies { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<Mail.Mail>? Mails { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public List<Realisation>? Realisations { get; set; }
    public List<Car.Car>? Cars { get; set; }
    public Status.Status? Status { get; set; }
    public long? StatusId { get; set; }
    public _DocumentType? DocumentType { get; set; }
    public long? DocumentTypeId { get; set; }
    public List<House.House>? Houses { get; set; }
    public List<Offer.Offer>? Offers { get; set; }
}