using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;

namespace AntaresApi.Services.Interfaces;

public interface IDocumentTypeService
{
    IEnumerable<_DocumentTypeDto> GetAll_DocumentType();
    _DocumentTypeDto Get_DocumentTypeById(long id);
    _DocumentTypeDto Create_DocumentType(_DocumentTypeDto documentType);
    _DocumentTypeDto Update_DocumentType(_DocumentTypeDto documentType);
    List<IdDto> DeleteMultiple_DocumentType(List<IdDto> documentTypes);
    _DocumentTypeDto ColManAddToDocument(_DocumentTypeDto documentType, IdDto owner);
    _DocumentTypeDto ColManAddToRecruitment(_DocumentTypeDto documentType, IdDto owner);
    List<IdDto> ColManDeleteFromDocument(List<IdDto> collection,  IdDto owner);
}