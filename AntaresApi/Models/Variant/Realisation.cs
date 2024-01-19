using System.ComponentModel.DataAnnotations;
using AntaresApi.Dto;
using AntaresApi.Models.Document;

namespace AntaresApi.Models;

public class Realisation
{
    [Key]
    public long Id { get; set; }
    public Variant Variant { get; set; }
    public VariantRealisation? VariantRealisation { get; set; }
    public double? NumericValue { get; set; }
    public DateTime? DateValue { get; set; }
    public bool? MadeByUser { get; set; }
    public IEnumerable<Employee>? Employees { get; set; }
    public IEnumerable<Company>? Companies { get; set; }
    public IEnumerable<_Document>? Documents { get; set; }
    public IEnumerable<Car.Car>? Cars { get; set; }
    public Comparator? Comparator { get; set; }
}