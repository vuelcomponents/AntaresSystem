using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Mail;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Status;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class _DocumentService : I_DocumentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public _DocumentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<_DocumentDto> GetAll_Document()
    {
        List<_Document> list = _context.Documents
            .Include(c=>c.Companies)!
            .Include(c=>c.Employees)
            .Include(c=>c.Realisations)!
            .ThenInclude(c=>c.Variant)
            .Include(c=>c.Realisations)!
            .ThenInclude(c=>c.VariantRealisation)
            .Include(c=>c.Status)
            .ThenInclude(s=>s.TransitionTo)
            .Include(s=>s.StoreModel)
            .Include(d=>d.DocumentType)
            .Include(d=>d.Cars)
            .Include(d=>d.Houses)
            .ToList();
        return list.Select(_mapper.Map<_DocumentDto>).ToList();
    }

    public _DocumentDto Get_DocumentById(long id)
    {
        _Document? document = _context.Documents.
             Include(c=>c.Companies)!
             .Include(c=>c.Employees)
             .Include(c=>c.Realisations)!
             .ThenInclude(c=>c.Variant)
             .Include(c=>c.Realisations)!
             .ThenInclude(c=>c.VariantRealisation)
             .Include(c=>c.Status)
             .ThenInclude(s=>s.TransitionTo)
             .Include(s=>s.StoreModel)
             .Include(d=>d.DocumentType)
             .Include(d=>d.Cars)
             .Include(d=>d.Houses)
            .FirstOrDefault(c=> c.Id == id);
        if (document == null)
        {
            throw new BadHttpRequestException($"No document #{id} has been found");
        }
        return _mapper.Map<_DocumentDto>(document);
    }

    public _DocumentDto Create_Document(_DocumentDto document)
    {
        if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (document.FileName.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("File name has not been specified");
        }
        if (document.FileData == null)
        {
            throw new BadHttpRequestException("File data has not been specified");
        }

        // Employee? employee = null;
        // if (document.Employee != null && document.Employee.Id != null)
        // {
        //     employee = _context.Employees.FirstOrDefault(e => e.Id == document.Employee.Id);
        //     
        // }
        // Company? company = null;
        // if (document.Company != null && document.Company.Id != null)
        // {
        //     company = _context.Companies.FirstOrDefault(e => e.Id == document.Company.Id);
        //     
        // }
        Status? status = null;
        if (document.Status != null)
        {
            status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            if (status.StoreModel.Code != "Document")
            {
                throw new Exception("Status is not destined to Document");
            }
        }
        _DocumentType? documentType = null;
        if (document.DocumentType != null)
        {
            documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
        }
        try
        {
            _Document db_Document = new _Document
            {
                Code = document.Code!,
                Description = document.Description!,
                FileData =  Convert.FromBase64String(document.FileData),
                FileName = document.FileName!,
                DateFrom   = document.DateFrom ?? DateTime.Now,
                DateTo = document.DateTo ?? DateTime.Now,
                UploadDate = DateTime.Now,
                // Employee = employee,
                // Company = company,
                Status = status,
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                DocumentTypeId = documentType?.Id
            };
            _context.Documents.Add(db_Document);
            _context.SaveChanges();
            return _mapper.Map<_DocumentDto>(db_Document);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public _DocumentDto Update_Document(_DocumentDto document)
    {
        if (document.Id == null)
        {
            throw new BadHttpRequestException($"ID has not been specified");
        }
        if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }
        if (document.FileName.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("File name has not been specified");
        }
        _Document? db_Document = _context.Documents.FirstOrDefault(e => e.Id.Equals(document.Id));
        if (db_Document is null)
        {
            throw new BadHttpRequestException($"There is no company #{document.Id}");
        }

        // Employee? employee = null;
        // if (document.Employee != null && document.Employee.Id != null)
        // {
        //     employee = _context.Employees.FirstOrDefault(e => e.Id == document.Employee.Id);
        //     
        // }
        // Company? company = null;
        // if (document.Company != null && document.Company.Id != null)
        // {
        //     company = _context.Companies.FirstOrDefault(e => e.Id == document.Company.Id);
        //     
        // }
        Status? status = null;
        if (document.Status != null)
        {
            status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
            if (status == null)
            {
                throw new BadHttpRequestException("Status has been specified but not found ");
            }
            if (status.StoreModel.Code != "Document")
            {
                throw new Exception("Status is not destined to Document");
            }
        }
        _DocumentType? documentType = null;
        if (document.DocumentType != null)
        {
            documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
        }
        db_Document.Code = document.Code!;
        db_Document.Description = document.Description!;
        db_Document.FileName = document.FileName!;
        db_Document.DateFrom = document.DateFrom ?? DateTime.Now;
        db_Document.DateTo = document.DateTo ?? DateTime.Now;
        db_Document.UploadDate = DateTime.Now;
        try
        {
            db_Document.FileData = Convert.FromBase64String(document.FileData!);
        }catch{}
        db_Document.DocumentTypeId = documentType?.Id;
        // db_Document.EmployeeId = employee?.Id;
        // db_Document.CompanyId = company?.Id;
        db_Document.StatusId = status?.Id;
        
        _context.SaveChanges();
        return _mapper.Map<_DocumentDto>(db_Document);
    }

    public long Delete_Document(long id)
    {
        throw new NotImplementedException();
    }

    public List<IdDto> DeleteMultiple_Document(List<IdDto> documents)
    {
        if(documents.Count >0)
        {
            foreach (var com in documents)
            {
                _Document? document = _context.Documents
                    .Include(m=>m.Mails)
                    .FirstOrDefault(c => c.Id == com.Id);
                if (document != null)
                {
                    _context.Documents.Remove(document);
                }
            }

            _context.SaveChanges();
        }
        return documents;
    }

   
    public _DocumentDto ColManAddToCompany(_DocumentDto document, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Comany ID has not been specified");
        }
        var dbCompany = _context.Companies.Include(e=>e.Documents).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCompany == null)
        {
            throw new BadHttpRequestException($"Comapny #{owner.Id} doest not exist");
        }

        switch (document.Id)
        {
            case null:
                if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
                }

                if (document.FileName.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("File name has not been specified");
                }

                if (document.FileData == null)
                {
                    throw new BadHttpRequestException("File data has not been specified");
                }
                Status? status = null;
                if (document.Status != null)
                {
                    status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
                    if (status == null)
                    {
                        throw new BadHttpRequestException("Status has been specified but not found ");
                    }
                    if (status.StoreModel.Code != "Document")
                    {
                        throw new Exception("Status is not destined to Document");
                    }
                }
                _DocumentType? documentType = null;
                if (document.DocumentType != null)
                {
                    documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
                }
                _Document db_Document = new _Document
                {
                    Code = document.Code!,
                    Description = document.Description!,
                    FileData = Convert.FromBase64String(document.FileData),
                    FileName = document.FileName!,
                    DateFrom = document.DateFrom ?? DateTime.Now,
                    DateTo = document.DateTo ?? DateTime.Now,
                    UploadDate = DateTime.Now,
                    Status = status,
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                    DocumentTypeId = documentType?.Id
                };

                if (dbCompany.Documents != null && dbCompany.Documents.Any(c => c.Code == db_Document.Code))
                {
                    throw new BadHttpRequestException(
                        $"Company #{dbCompany.Id} already has signed document with code {db_Document.Code}");
                }

                if (dbCompany.Documents == null)
                {
                    dbCompany.Documents = new List<_Document>();
                }

                dbCompany.Documents.Add(db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(db_Document);
            default:
                _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (s_db_Document == null)
                {
                    throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
                }
                if (dbCompany.Documents != null && dbCompany.Documents.Any(c => c.Id == s_db_Document.Id))
                {
                    throw new BadHttpRequestException($"Employee #{dbCompany.Id} already has signed document #{s_db_Document.Code}");
                }

                if (dbCompany.Documents == null)
                {
                    dbCompany.Documents = new List<_Document>();
                }
                dbCompany.Documents.Add(s_db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(s_db_Document);
        }

    }
    public _DocumentDto ColManAddToEmployee(_DocumentDto document, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Employee ID has not been specified");
        }
        var dbEmployee = _context.Employees.Include(e=>e.Documents).FirstOrDefault(e => e.Id == owner.Id);
        if (dbEmployee == null)
        {
            throw new BadHttpRequestException($"Employee #{owner.Id} doest not exist");
        }

        switch (document.Id)
        {
            case null:
                if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("Code, Description has not been specified");
                }
                if (document.FileName.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("File name has not been specified");
                }
                if (document.FileData == null)
                {
                    throw new BadHttpRequestException("File data has not been specified");
                }
                Status? status = null;
                if (document.Status != null)
                {
                    status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
                    if (status == null)
                    {
                        throw new BadHttpRequestException("Status has been specified but not found ");
                    }
                    if (status.StoreModel.Code != "Document")
                    {
                        throw new Exception("Status is not destined to Document");
                    }
                }
                _DocumentType? documentType = null;
                if (document.DocumentType != null)
                {
                    documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
                }
                _Document db_Document = new _Document
                {
                    Code = document.Code!,
                    Description = document.Description!,
                    FileData = Convert.FromBase64String(document.FileData),
                    FileName = document.FileName!,
                    DateFrom = document.DateFrom ?? DateTime.Now,
                    DateTo = document.DateTo ?? DateTime.Now,
                    UploadDate = DateTime.Now,
                    Status = status,
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                    DocumentTypeId = documentType?.Id
                };

                if (dbEmployee.Documents != null && dbEmployee.Documents.Any(c => c.Code == db_Document.Code))
                {
                    throw new BadHttpRequestException($"Employee #{dbEmployee.Id} already has signed document with code {db_Document.Code}");
                }

                if (dbEmployee.Documents == null)
                {
                    dbEmployee.Documents = new List<_Document>();
                }
                dbEmployee.Documents.Add(db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(db_Document);
            default:
                _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (s_db_Document == null)
                {
                    throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
                }
                if (dbEmployee.Documents != null && dbEmployee.Documents.Any(c => c.Id == s_db_Document.Id))
                {
                    throw new BadHttpRequestException($"Employee #{dbEmployee.Id} already has signed document #{s_db_Document.Code}");
                }

                if (dbEmployee.Documents == null)
                {
                    dbEmployee.Documents = new List<_Document>();
                }
                dbEmployee.Documents.Add(s_db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(s_db_Document);
        }
      
    }

    public _DocumentDto ColManAddToHouse(_DocumentDto document, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("House ID has not been specified");
        }

        var dbHouse = _context.Houses.Include(e => e.Documents).FirstOrDefault(e => e.Id == owner.Id);
        if (dbHouse == null)
        {
            throw new BadHttpRequestException($"House #{owner.Id} doest not exist");
        }

        switch (document.Id)
        {
            case null:
                if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("Code, Description has not been specified");
                }

                if (document.FileName.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("File name has not been specified");
                }

                if (document.FileData == null)
                {
                    throw new BadHttpRequestException("File data has not been specified");
                }

                Status? status = null;
                if (document.Status != null)
                {
                    status = _context.Statuses.Include(s => s.StoreModel)
                        .FirstOrDefault(s => s.Id == document.Status.Id);
                    if (status == null)
                    {
                        throw new BadHttpRequestException("Status has been specified but not found ");
                    }

                    if (status.StoreModel.Code != "Document")
                    {
                        throw new Exception("Status is not destined to Document");
                    }
                }

                _DocumentType? documentType = null;
                if (document.DocumentType != null)
                {
                    documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
                }

                _Document db_Document = new _Document
                {
                    Code = document.Code!,
                    Description = document.Description!,
                    FileData = Convert.FromBase64String(document.FileData),
                    FileName = document.FileName!,
                    DateFrom = document.DateFrom ?? DateTime.Now,
                    DateTo = document.DateTo ?? DateTime.Now,
                    UploadDate = DateTime.Now,
                    Status = status,
                    StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Document")!,
                    DocumentTypeId = documentType?.Id
                };

                if (dbHouse.Documents != null && dbHouse.Documents.Any(c => c.Code == db_Document.Code))
                {
                    throw new BadHttpRequestException(
                        $"Employee #{dbHouse.Id} already has signed document with code {db_Document.Code}");
                }

                if (dbHouse.Documents == null)
                {
                    dbHouse.Documents = new List<_Document>();
                }

                dbHouse.Documents.Add(db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(db_Document);
            default:
                _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (s_db_Document == null)
                {
                    throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
                }

                if (dbHouse.Documents != null && dbHouse.Documents.Any(c => c.Id == s_db_Document.Id))
                {
                    throw new BadHttpRequestException(
                        $"Employee #{dbHouse.Id} already has signed document #{s_db_Document.Code}");
                }

                if (dbHouse.Documents == null)
                {
                    dbHouse.Documents = new List<_Document>();
                }

                dbHouse.Documents.Add(s_db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(s_db_Document);
        }
    }

    public _DocumentDto ColManAddToMail(_DocumentDto document, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Mail ID has not been specified");
        }
        var dbMail = _context.Mails.Include(e=>e.Documents).FirstOrDefault(e => e.Id == owner.Id);
        if (dbMail == null)
        {
            throw new BadHttpRequestException($"Mail #{owner.Id} doest not exist");
        }

        switch (document.Id)
        {
            case null:
                if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("Code, Description has not been specified");
                }
                if (document.FileName.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("File name has not been specified");
                }
                if (document.FileData == null)
                {
                    throw new BadHttpRequestException("File data has not been specified");
                }
                Status? status = null;
                if (document.Status != null)
                {
                    status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
                    if (status == null)
                    {
                        throw new BadHttpRequestException("Status has been specified but not found ");
                    }
                    if (status.StoreModel.Code != "Document")
                    {
                        throw new Exception("Status is not destined to Document");
                    }
                }
                _DocumentType? documentType = null;
                if (document.DocumentType != null)
                {
                    documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
                }
                _Document db_Document = new _Document
                {
                    Code = document.Code!,
                    Description = document.Description!,
                    FileData = Convert.FromBase64String(document.FileData),
                    FileName = document.FileName!,
                    DateFrom = document.DateFrom ?? DateTime.Now,
                    DateTo = document.DateTo ?? DateTime.Now,
                    UploadDate = DateTime.Now,
                    Status = status,
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                    DocumentTypeId = documentType?.Id
                };

                if (dbMail.Documents != null && dbMail.Documents.Any(c => c.Code == db_Document.Code))
                {
                    throw new BadHttpRequestException($"Mail #{dbMail.Id} already has signed document with code {db_Document.Code}");
                }

                if (dbMail.Documents == null)
                {
                    dbMail.Documents = new List<_Document>();
                }
                dbMail.Documents.Add(db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(db_Document);
            default:
                _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (s_db_Document == null)
                {
                    throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
                }
                if (dbMail.Documents != null && dbMail.Documents.Any(c => c.Id == s_db_Document.Id))
                {
                    throw new BadHttpRequestException($"Mail #{dbMail.Id} already has signed document #{s_db_Document.Code}");
                }

                if (dbMail.Documents == null)
                {
                    dbMail.Documents = new List<_Document>();
                }
                dbMail.Documents.Add(s_db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(s_db_Document);
        }
      
    }
      public _DocumentDto ColManAddToCar(_DocumentDto document, IdDto owner)
    {
        if (owner.Id == null)
        {
            throw new BadHttpRequestException("Car ID has not been specified");
        }
        var dbCar = _context.Cars.Include(e=>e.Documents).FirstOrDefault(e => e.Id == owner.Id);
        if (dbCar == null)
        {
            throw new BadHttpRequestException($"Car #{owner.Id} doest not exist");
        }

        switch (document.Id)
        {
            case null:
                if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("Code, Description has not been specified");
                }
                if (document.FileName.IsNullOrEmpty())
                {
                    throw new BadHttpRequestException("File name has not been specified");
                }
                if (document.FileData == null)
                {
                    throw new BadHttpRequestException("File data has not been specified");
                }
                Status? status = null;
                if (document.Status != null)
                {
                    status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
                    if (status == null)
                    {
                        throw new BadHttpRequestException("Status has been specified but not found ");
                    }
                    if (status.StoreModel.Code != "Document")
                    {
                        throw new Exception("Status is not destined to Document");
                    }
                }
                _DocumentType? documentType = null;
                if (document.DocumentType != null)
                {
                    documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
                }
                _Document db_Document = new _Document
                {
                    Code = document.Code!,
                    Description = document.Description!,
                    FileData = Convert.FromBase64String(document.FileData),
                    FileName = document.FileName!,
                    DateFrom = document.DateFrom ?? DateTime.Now,
                    DateTo = document.DateTo ?? DateTime.Now,
                    UploadDate = DateTime.Now,
                    Status = status,
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                    DocumentTypeId = documentType?.Id
                };

                if (dbCar.Documents != null && dbCar.Documents.Any(c => c.Code == db_Document.Code))
                {
                    throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed document with code {db_Document.Code}");
                }

                if (dbCar.Documents == null)
                {
                    dbCar.Documents = new List<_Document>();
                }
                dbCar.Documents.Add(db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(db_Document);
            default:
                _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (s_db_Document == null)
                {
                    throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
                }
                if (dbCar.Documents != null && dbCar.Documents.Any(c => c.Id == s_db_Document.Id))
                {
                    throw new BadHttpRequestException($"Car #{dbCar.Id} already has signed document #{s_db_Document.Code}");
                }

                if (dbCar.Documents == null)
                {
                    dbCar.Documents = new List<_Document>();
                }
                dbCar.Documents.Add(s_db_Document);
                _context.SaveChanges();
                return _mapper.Map<_DocumentDto>(s_db_Document);
        }
      
    }
        public _DocumentDto ColManAddToOffer(_DocumentDto document, IdDto owner)
        {
            throw new NotImplementedException();
            // if (owner.Id == null)
            // {
            //     throw new BadHttpRequestException("Offer ID has not been specified");
            // }
            // var dbOffer = _context.Offers.Include(e=>e.Image).FirstOrDefault(e => e.Id == owner.Id);
            // if (dbOffer == null)
            // {
            //     throw new BadHttpRequestException($"Offer #{owner.Id} doest not exist");
            // }
            //
            // switch (document.Id)
            // {
            //     case null:
            //         if (document.Code.IsNullOrEmpty() || document.Description.IsNullOrEmpty())
            //         {
            //             throw new BadHttpRequestException("Code, Description has not been specified");
            //         }
            //         if (document.FileName.IsNullOrEmpty())
            //         {
            //             throw new BadHttpRequestException("File name has not been specified");
            //         }
            //
            //         if (!IsImageExtension(document.FileName!))
            //         {
            //             throw new BadHttpRequestException("File is not an image");
            //         }
            //         if (document.FileData == null)
            //         {
            //             throw new BadHttpRequestException("File data has not been specified");
            //         }
            //         Status? status = null;
            //         if (document.Status != null)
            //         {
            //             status = _context.Statuses.Include(s=>s.StoreModel).FirstOrDefault(s => s.Id == document.Status.Id);
            //             if (status == null)
            //             {
            //                 throw new BadHttpRequestException("Status has been specified but not found ");
            //             }
            //             if (status.StoreModel.Code != "Document")
            //             {
            //                 throw new Exception("Status is not destined to Document");
            //             }
            //         }
            //         _DocumentType? documentType = null;
            //         if (document.DocumentType != null)
            //         {
            //             documentType = _context.DocumentTypes.FirstOrDefault(d => d.Id == document.DocumentType.Id);
            //         }
            //         _Document db_Document = new _Document
            //         {
            //             Code = document.Code!,
            //             Description = document.Description!,
            //             FileData = Convert.FromBase64String(document.FileData),
            //             FileName = document.FileName!,
            //             DateFrom = document.DateFrom ?? DateTime.Now,
            //             DateTo = document.DateTo ?? DateTime.Now,
            //             UploadDate = DateTime.Now,
            //             Status = status,
            //             StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
            //             DocumentTypeId = documentType?.Id
            //         };
            //
            //         if (dbOffer.Images != null && dbOffer.Images.Any(c => c.Code == db_Document.Code))
            //         {
            //             throw new BadHttpRequestException($"Offer #{dbOffer.Id} already has signed document with code {db_Document.Code}");
            //         }
            //         
            //         dbOffer.Image = (db_Document);
            //         _context.SaveChanges();
            //         return _mapper.Map<_DocumentDto>(db_Document);
            //     default:
            //         _Document? s_db_Document = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
            //         if (s_db_Document == null)
            //         {
            //             throw new BadHttpRequestException($"ID was specified, but there is no document #{document.Id}");
            //         }
            //         
            //         if (!IsImageExtension(s_db_Document.FileName!))
            //         {
            //             throw new BadHttpRequestException("File is not an image");
            //         }
            //         dbOffer.Image  = (s_db_Document);
            //         _context.SaveChanges();
            //         return _mapper.Map<_DocumentDto>(s_db_Document);
        // }
      
    }
    public List<IdDto> ColManDeleteFromCompany(List<IdDto> collection, IdDto owner)
    {
        Company? company = _context.Companies
            .Include(e=>e.Documents)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (company == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
                if (document != null && company.Documents is {Count:>0})
                {
                    company.Documents.Remove(document);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteFromCar(List<IdDto> collection, IdDto owner)
    {
        Car? car = _context.Cars
            .Include(e=>e.Documents)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (car == null)
        {
            throw new BadHttpRequestException($"Invalid car #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
                if (document != null && car.Documents is {Count:>0})
                {
                    car.Documents.Remove(document);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteFromOffer(List<IdDto> collection, IdDto owner)
    {
        throw new NotImplementedException();
        // Offer? offer = _context.Offers
        //     .Include(e=>e.Image)
        //     .FirstOrDefault(e => e.Id == owner.Id);
        // if (offer == null)
        // {
        //     throw new BadHttpRequestException($"Invalid offer #{owner.Id}");
        // }
        // if(collection.Count >0)
        // {
        //     foreach (var com in collection)
        //     {
        //         _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
        //         if (document != null && offer.Images is {Count:>0})
        //         {
        //             offer.Images.Remove(document);
        //         }
        //     }
        //
        //     _context.SaveChanges();
        // }
        // return collection;
    }
    public List<IdDto> ColManDeleteFromEmployee(List<IdDto> collection, IdDto owner)
    {
        Employee? employee = _context.Employees
            .Include(e=>e.Documents)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (employee == null)
        {
            throw new BadHttpRequestException($"Invalid employee #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
                if (document != null && employee.Documents is {Count:>0})
                {
                    employee.Documents.Remove(document);
                }
            }

            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteFromMail(List<IdDto> collection, IdDto owner)
    {
        Mail? mail = _context.Mails
            .Include(e=>e.Documents)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (mail == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
                if (document != null && mail.Documents is {Count:>0})
                {
                    mail.Documents.Remove(document);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }
    public List<IdDto> ColManDeleteFromHouse(List<IdDto> collection, IdDto owner)
    {
        House? house = _context.Houses
            .Include(e=>e.Documents)
            .FirstOrDefault(e => e.Id == owner.Id);
        if (house == null)
        {
            throw new BadHttpRequestException($"Invalid company #{owner.Id}");
        }
        
        if(collection.Count >0)
        {
            foreach (var com in collection)
            {
                _Document? document = _context.Documents.FirstOrDefault(c => c.Id == com.Id);
                if (document != null && house.Documents is {Count:>0})
                {
                    house.Documents.Remove(document);
                }
            }
            _context.SaveChanges();
        }
        return collection;
    }

    public async Task<_Document> Get_DocumentModelByIdToDownload(long id)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == id);
        
        if (document == null)
        {
            throw new BadHttpRequestException($"Dokument o identyfikatorze {id} nie został znaleziony.");
        }
        if (document.FileData == null)
        {
            throw new BadHttpRequestException($"Dokument o identyfikatorze {id} doesnt owe FileData.");
        }

        return document;
    }
    
    private bool IsImageExtension(string fileName)
    {
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp"  };
        string extension = Path.GetExtension(fileName)?.ToLower();
        return !string.IsNullOrEmpty(extension) && imageExtensions.Contains(extension);
    }
}