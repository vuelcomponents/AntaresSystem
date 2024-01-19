using AntaresApi.Dto.Position;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Dto.Employee;

public class EmployeePersonnelAdvise
{
    public PositionShortDto Position { get; set; }
    public int Chance { get; set; } = 0;
    public int Loss { get; set; } = 0;
    public int NoLossPercent { get; set; } = 0;
    public int WithLossPercent { get; set; } = 0;
    public List<RealisationShortDto>? Matched { get; set; } = new List<RealisationShortDto>();
    public List<RealisationShortDto>? Unmatched { get; set; } = new List<RealisationShortDto>();
}