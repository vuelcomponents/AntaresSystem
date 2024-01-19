using System.ComponentModel.DataAnnotations;

namespace AntaresApi.Models.Position;

public class Position
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public PositionUnit PositionUnit { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Realisation>? Requirements { get; set; }
    public int? EmploymentQty { get; set; }
    public Company Company { get; set; }
    public long CompanyId { get; set; }
    public List<Position>? Children { get; set; }
    public Position? Parent { get; set; }
    public long? ParentId { get; set; }
    public double? Demand { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public List<Offer.Offer>? Offers { get; set; }
}