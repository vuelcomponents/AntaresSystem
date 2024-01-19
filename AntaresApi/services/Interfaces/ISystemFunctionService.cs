using AntaresApi.Dto;

namespace AntaresApi.Services.Interfaces;

public interface ISystemFunctionService
{
    IEnumerable<SystemFunctionDto> GetAllSystemFunctions();
    SystemFunctionDto GetSystemFunctionById(long id);
}