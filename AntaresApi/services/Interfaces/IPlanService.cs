using AntaresApi.Dto.Common;
using AntaresApi.Dto.Plan;

namespace AntaresApi.Services.Interfaces;

public interface IPlanService
{
    IEnumerable<PlanDto> GetAllPlan();
    PlanDto GetPlanById(long id);
    List<PlanDto> GetPlansByEmployeeId(long id);
    PlanDto CreatePlan(PlanDto plan);
    Task<PlanDto> UpdatePlan(PlanDto plan);
    long DeletePlan(long id);
    List<IdDto> DeleteMultiplePlan(List<IdDto> plans);
}