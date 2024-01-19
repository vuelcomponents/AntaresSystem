using AntaresApi.Dto.Common;
using AntaresApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Variant;
[ApiController]
[Route("api/comparator")]
public class ComparatorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ComparatorController(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<IdCodeDto>> GetAllComparators()
    {
        List<Comparator> list = _context.Comparators.ToList();
        return Ok(list.Select(_mapper.Map<IdCodeDto>).ToList());
    }
    [HttpGet("get/{id}")]
    public ActionResult<IdCodeDto> GetRealisationById(long id)
    {
        Comparator? comparator = _context.Comparators.FirstOrDefault(c=> c.Id == id);
        if (comparator == null)
        {
            return BadRequest($"No comparator #{id} has been found");
        }
        return _mapper.Map<IdCodeDto>(comparator);
    }
}