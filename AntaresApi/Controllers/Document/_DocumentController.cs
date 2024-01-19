using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Models.Document;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Document;
[ApiController]
[Route("api/document")]
public class _DocumentController : ControllerBase
{
        private readonly I_DocumentService _documentService;
    
    public _DocumentController(I_DocumentService documentService)
    {
        _documentService = documentService;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<_DocumentDto>> GetAllDocument()
    {
        try
        {
            return Ok(_documentService.GetAll_Document());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<_DocumentDto> GetDocumentById(long id)
    {
        try
        {
            return Ok(_documentService.Get_DocumentById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("create")]
    public ActionResult<_DocumentDto> CreateDocument(_DocumentDto employeeDocument)
    {
        try
        {
            return Ok( _documentService.Create_Document(employeeDocument));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public  ActionResult<_DocumentDto> UpdateDocument(_DocumentDto employeeDocument)
    {
        try
        {
            return Ok(_documentService.Update_Document(employeeDocument));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult<_DocumentDto> DeleteDocument(long id)
    {
        try
        {
            return Ok(_documentService.Delete_Document(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleDocument(List<IdDto> employeeDocuments)
    {
        try
        {
            return Ok(_documentService.DeleteMultiple_Document(employeeDocuments));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /** FUNKCJE TYLKO DLA MODELI BEDACYCH W CZYJEJS KOLEKCJII **/
    [HttpPost("create-colman")]
    public ActionResult<_DocumentDto> ColManCreateDocument(Col_DocumentDto single)
    {
        try
        {
            switch (single.OwnerName)
            {
                case "employee":
                    return Ok(_documentService.ColManAddToEmployee(single.Object, single.Owner));
                case "company":
                    return Ok(_documentService.ColManAddToCompany(single.Object, single.Owner));
                case "mail":
                    return Ok(_documentService.ColManAddToMail(single.Object, single.Owner));
                case "house":
                    return Ok(_documentService.ColManAddToHouse(single.Object, single.Owner));
                case "car":
                    return Ok(_documentService.ColManAddToCar(single.Object, single.Owner));
                case "offer":
                    return Ok(_documentService.ColManAddToOffer(single.Object, single.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple-colman")]
    public ActionResult<List<IdDto>> ColManDeleteMultipleDocument(ColIDCollectionDto collection)
    {
        try
        {
            switch (collection.OwnerName)
            {
                case "employee":
                    return Ok(_documentService.ColManDeleteFromEmployee(collection.Collection, collection.Owner));
                case "company":
                    return Ok(_documentService.ColManDeleteFromCompany(collection.Collection, collection.Owner));
                case "mail":
                    return Ok(_documentService.ColManDeleteFromMail(collection.Collection, collection.Owner));
                case "house":
                    return Ok(_documentService.ColManDeleteFromHouse(collection.Collection, collection.Owner));
                case "car":
                    return Ok(_documentService.ColManDeleteFromCar(collection.Collection, collection.Owner));
                case "offer":
                    return Ok(_documentService.ColManDeleteFromOffer(collection.Collection, collection.Owner));
            }

            return BadRequest("Not implemented");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("download/{documentId}")]
    public async Task<ActionResult> Download(long documentId)
    {
        try
        {
            _Document document = await _documentService.Get_DocumentModelByIdToDownload(documentId);
            byte[] fileData = document.FileData;
            MemoryStream file =   new MemoryStream(fileData);
            
            return File(file, "application/octet-stream", document.FileName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}