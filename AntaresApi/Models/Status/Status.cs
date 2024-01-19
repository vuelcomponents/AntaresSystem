using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.Status;

public class Status
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }

    public bool Reserved { get; set; } = false;
    // public string? Description { get; set; }
    public List<Status>? TransitionTo { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public int Priority { get; set; }
    public List<Employee>? Employees { get; set; }
    public List<Employee>? PreviousEmployees { get; set; }
    public List<_Document>? Documents { get; set; }
    public List<Variant>? Variants { get; set; }
    public List<Company>? Companies { get; set; }
    public string? Color { get; set; }
    public List<StatusAction.StatusAction>? StatusActions { get; set; }
    
}