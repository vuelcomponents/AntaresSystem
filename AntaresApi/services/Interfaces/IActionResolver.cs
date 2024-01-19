using AntaresApi.Models;
using AntaresApi.Models.Status;

namespace AntaresApi.Services.Interfaces;

public interface IActionResolver
{
    Task ResolveStatusAction( Status? previousStatus, Status nextStatus, long objectId);
}