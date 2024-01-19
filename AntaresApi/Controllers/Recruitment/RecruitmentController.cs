using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Recruitment;
[ApiController]
[Route("api/recruitment")]
public class RecruitmentController : ControllerBase
{
    private readonly IRecruitmentService _recruitmentService;

    public RecruitmentController(IRecruitmentService recruitmentService)
    {
        _recruitmentService = recruitmentService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<RecruitmentDto>> GetAllRecruitment()
    {
        try
        {
            return Ok(_recruitmentService.GetAllRecruitment());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<RecruitmentDto> GetRecruitmentById(long id)
    {
        try
        {
            return Ok(_recruitmentService.GetRecruitmentById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    [HttpPost("create")]
    public ActionResult<RecruitmentDto> CreateRecruitment(RecruitmentShortDto recruitment)
    {
        try
        {
            return Ok(_recruitmentService.CreateRecruitment(recruitment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<RecruitmentDto> UpdateRecruitment(RecruitmentShortDto recruitment)
    {
        try
        {
            return Ok(_recruitmentService.UpdateRecruitment(recruitment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleRecruitment(List<IdDto> recruitment)
    {
        try
        {
            return Ok(_recruitmentService.DeleteMultipleRecruitment(recruitment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("advance")]
    public ActionResult<RecruitmentDto> Advance(EmployeeDto employee)
    {
        try
        {
            return Ok(_recruitmentService.Advance(employee));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}