using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Recruitment;

namespace AntaresApi.Services.Interfaces;

public interface IRecruitmentService
{
    IEnumerable<RecruitmentDto> GetAllRecruitment();
    RecruitmentDto GetRecruitmentById(long id);
    RecruitmentDto CreateRecruitment(RecruitmentShortDto recruitment);
    RecruitmentDto UpdateRecruitment(RecruitmentShortDto recruitment);
    List<IdDto> DeleteMultipleRecruitment(List<IdDto> recruitment);
    RecruitmentDto Advance(EmployeeDto employee);
}