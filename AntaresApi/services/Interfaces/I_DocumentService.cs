using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Models.Document;

namespace AntaresApi.Services.Interfaces;

public interface I_DocumentService
{
    IEnumerable<_DocumentDto> GetAll_Document();
    _DocumentDto Get_DocumentById(long id);
    _DocumentDto Create_Document(_DocumentDto document);
    _DocumentDto Update_Document(_DocumentDto document);
    long Delete_Document(long id);
    List<IdDto> DeleteMultiple_Document(List<IdDto> documents);
    _DocumentDto ColManAddToCompany(_DocumentDto document, IdDto owner);
    _DocumentDto ColManAddToEmployee(_DocumentDto document, IdDto owner);
    List<IdDto> ColManDeleteFromCompany(List<IdDto> collection,  IdDto owner);
    List<IdDto> ColManDeleteFromEmployee(List<IdDto> collection,  IdDto owner);
    _DocumentDto ColManAddToMail(_DocumentDto document, IdDto owner);
    List<IdDto> ColManDeleteFromMail(List<IdDto> collection,  IdDto owner);
    _DocumentDto ColManAddToHouse(_DocumentDto document, IdDto owner);
    List<IdDto> ColManDeleteFromHouse(List<IdDto> collection,  IdDto owner);
    _DocumentDto ColManAddToCar(_DocumentDto document, IdDto owner);
    List<IdDto> ColManDeleteFromCar(List<IdDto> collection,  IdDto owner);
    _DocumentDto ColManAddToOffer(_DocumentDto document, IdDto owner);
    List<IdDto> ColManDeleteFromOffer(List<IdDto> collection,  IdDto owner);
    Task<_Document> Get_DocumentModelByIdToDownload(long id);
}