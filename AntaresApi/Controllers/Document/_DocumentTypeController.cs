using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/document-type")]
public class _DocumentTypeController : ControllerBase
{
        private readonly IDocumentTypeService _documentTypeService;
    
    public _DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<_DocumentTypeDto>> GetAllDocumentType()
    {
        try
        {
            return Ok(_documentTypeService.GetAll_DocumentType());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<_DocumentTypeDto> GetDocumentTypeById(long id)
    {
        try
        {
            return Ok(_documentTypeService.Get_DocumentTypeById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<_DocumentTypeDto> CreateDocumentType(_DocumentTypeDto employeeDocumentType)
    {
        try
        {
            return Ok( _documentTypeService.Create_DocumentType(employeeDocumentType));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public  ActionResult<_DocumentTypeDto> UpdateDocumentType(_DocumentTypeDto employeeDocumentType)
    {
        try
        {
            return Ok(_documentTypeService.Update_DocumentType(employeeDocumentType));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleDocumentType(List<IdDto> documentTypes)
    {
        try
        {
            return Ok(_documentTypeService.DeleteMultiple_DocumentType(documentTypes));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /** FUNKCJE TYLKO DLA MODELI BEDACYCH W CZYJEJS KOLEKCJII **/
    [HttpPost("create-colman")]
    public ActionResult<_DocumentTypeDto> ColManCreateDocumentType(Col_DocumentTypeDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "document":
                    return Ok(_documentTypeService.ColManAddToDocument(single.Object, single.Owner));
                case "recruitment":
                    return Ok(_documentTypeService.ColManAddToRecruitment(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleDocumentType(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "document":
                    return Ok(_documentTypeService.ColManDeleteFromDocument(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

}

internal class I_DocumentTypeService
{
}