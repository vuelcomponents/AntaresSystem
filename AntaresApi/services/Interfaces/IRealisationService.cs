using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Realisation;

namespace AntaresApi.Services.Interfaces;

public interface IRealisationService
{
    IEnumerable<RealisationDto> GetAllRealisation();

    RealisationDto GetRealisationById(long id);
    RealisationDto CreateRealisation(RealisationDto realisation);
    RealisationDto UpdateRealisation(RealisationDto realisation);
    RealisationDto DeleteRealisation(long id);
    List<IdDto> DeleteMultipleRealisation(List<IdDto> realisation);
    RealisationDto CreateRealisationToCompany(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRealisationToEmployee(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRealisationToDocument(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRealisationToActionFunction(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRequirementToPosition(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRealisationToCar(RealisationDto realisation, IdDto owner);
    RealisationDto CreateRequirementToHouse(RealisationDto realisation, IdDto owner);
}