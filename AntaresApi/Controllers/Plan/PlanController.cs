using AntaresApi.Dto.Common;
using AntaresApi.Dto.Plan;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Plan;

[ApiController]
[Route("api/plan")]
public class PlanController : ControllerBase
{
    private readonly IPlanService _planService;
    private readonly IEmployeeService _employeeService;

    public PlanController(IPlanService planService, IEmployeeService employeeService)
    {
        _planService = planService;
        _employeeService = employeeService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<PlanDto>> GetAllPlan()
    {
        try
        {
            return Ok(_planService.GetAllPlan());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<PlanDto> GetPlanById(long id)
    {
        try
        {
            return Ok(_planService.GetPlanById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get-to-employee/{id}")]
    public ActionResult<List<PlanDto>> GetPlansByEmployeeId(long id)
    {
        try
        {
            return Ok(_planService.GetPlansByEmployeeId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<PlanDto> CreatePlan(PlanDto employee)
    {
        try
        {
            return Ok(_planService.CreatePlan(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("update")]
    public async Task<ActionResult<PlanDto>> UpdatePlan(PlanDto company)
    {
        try
        {
            return Ok( await _planService.UpdatePlan(company));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<PlanDto> DeletePlan(long id)
    {
        try
        {
            return Ok(_planService.DeletePlan(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultiplePlan(List<IdDto> plans)
    {
        try
        {
            return Ok(_planService.DeleteMultiplePlan(plans));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}