using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Models.Document;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class _DocumentTypeService : IDocumentTypeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public _DocumentTypeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<_DocumentTypeDto> GetAll_DocumentType()
    {
        List<_DocumentType> list = _context.DocumentTypes.ToList();
        return list.Select(_mapper.Map<_DocumentTypeDto>).ToList();
    }

    public _DocumentTypeDto Get_DocumentTypeById(long id)
    {
        _DocumentType? documentType = _context.DocumentTypes.FirstOrDefault(c=> c.Id == id);
        if (documentType == null)
        {
            throw new BadHttpRequestException($"No document type #{id} has been found");
        }
        return _mapper.Map<_DocumentTypeDto>(documentType);
    }

    public _DocumentTypeDto Create_DocumentType(_DocumentTypeDto documentType)
    {
        if (documentType.Code.IsNullOrEmpty() || documentType.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (_context.DocumentTypes.ToList().Any(dt => dt.Code == documentType.Code))
        {
            throw new BadHttpRequestException("Document type with that code already exists");
        }

        _DocumentType dbDocumentType = new _DocumentType
            { Code = documentType.Code!, Description = documentType.Description! };
        _context.DocumentTypes.Add(dbDocumentType);
        _context.SaveChanges();
        return _mapper.Map<_DocumentTypeDto>(dbDocumentType);
    }

    public _DocumentTypeDto Update_DocumentType(_DocumentTypeDto documentType)
    {
        if (documentType.Id == null)
        {
            throw new BadHttpRequestException("Document type ID has not been specified");
        }
        if (documentType.Code.IsNullOrEmpty() || documentType.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (_context.DocumentTypes.ToList().Any(dt => dt.Code == documentType.Code && dt.Id != documentType.Id))
        {
            throw new BadHttpRequestException("Document type with that code already exists");
        }

        _DocumentType? dbDocumentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == documentType.Id);
        if (dbDocumentType == null)
        {
            throw new BadHttpRequestException($"Document type #{documentType.Id} does bnot exist");
        }

        dbDocumentType.Code = documentType.Code!;
        dbDocumentType.Description = documentType.Description!;
        _context.SaveChanges();
        return _mapper.Map<_DocumentTypeDto>(dbDocumentType);
    }

    public List<IdDto> DeleteMultiple_DocumentType(List<IdDto> documentTypes)
    {
        throw new NotImplementedException();
    }

    public _DocumentTypeDto ColManAddToDocument(_DocumentTypeDto documentType, IdDto owner)
    {
        throw new NotImplementedException();
    }
    public _DocumentTypeDto ColManAddToRecruitment(_DocumentTypeDto documentType, IdDto owner)
    {
        if (documentType.Id == null)
        {
            throw new BadHttpRequestException("No documentType ID has been specified");
        }
        var dbDocumentType = _context.DocumentTypes.FirstOrDefault(c => c.Id == documentType.Id);
        if (dbDocumentType is null)
        {
            throw new BadHttpRequestException($"DocumentType #{documentType.Id} does not exist");
        }
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Recruitment ID has not been specified");
        }
        var dbRecruitment = _context.Recruitments
            .Include(e=>e.DocumentTypes).FirstOrDefault(e => e.Id == owner.Id);
        if (dbRecruitment == null)
        {
            throw new BadHttpRequestException($"Recruitment #{owner.Id} doest not exist");
        }

        if (dbRecruitment.DocumentTypes != null && dbRecruitment.DocumentTypes.Any(c => c.Id == dbDocumentType.Id))
        {
            throw new BadHttpRequestException($"Recruitment #{dbRecruitment.Id} already has signed documentType #{dbDocumentType.Id}");
        }

        if (dbRecruitment.DocumentTypes == null)
        {
            dbRecruitment.DocumentTypes = new List<_DocumentType>();
        }
        dbRecruitment.DocumentTypes.Add(dbDocumentType);
        _context.SaveChanges();
        return _mapper.Map<_DocumentTypeDto>(dbDocumentType);
    }
    public List<IdDto> ColManDeleteFromDocument(List<IdDto> collection, IdDto owner)
    {
        throw new NotImplementedException();
    }
}