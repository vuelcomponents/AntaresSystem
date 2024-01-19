using AntaresApi.Dto;
using AntaresApi.Models;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AntaresApi.Services;

public class SystemFunctionService : ISystemFunctionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SystemFunctionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<SystemFunctionDto> GetAllSystemFunctions()
    {
        List<SystemFunction> list = _context.SystemFunctions.Include(sf=>sf.StoreModels).ToList();
     
        return list.Select(_mapper.Map<SystemFunctionDto>).ToList();
    }

    public SystemFunctionDto GetSystemFunctionById(long id)
    {
        SystemFunction? systemFunction = _context.SystemFunctions.Include(s=>s.StoreModels).FirstOrDefault(c=> c.Id == id);
        if (systemFunction == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<SystemFunctionDto>(systemFunction);
    }
}