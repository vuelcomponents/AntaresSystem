using AntaresApi.Dto;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Models;

namespace AntaresApi.Services.Interfaces;

public interface ICompanyService
{
    IEnumerable<CompanyLiteDto> GetAllCompany();
    CompanyDto GetCompanyById(long id);
    CompanyDto CreateCompany(CompanyDto company);
    CompanyDto UpdateCompany(CompanyDto company);
    long DeleteCompany(long id);
    List<IdDto> DeleteMultipleCompany(List<IdDto> companies);
    List<IdDto> ColDeleteMultipleCompanyFromEmployee(List<IdDto> collection, IdDto owner);
    List<IdDto> ColDeleteMultipleCompanyFromDocument(List<IdDto> collection, IdDto owner);
    CompanyDto ColManAddToEmployee(CompanyDto company, IdDto owner);
    CompanyDto ColManAddToDocument(CompanyDto company, IdDto owner);
    CompanyDto ColManAddToHouse(CompanyDto company, IdDto owner);
    List<IdDto> ColDeleteMultipleCompanyFromHouse(List<IdDto> collection, IdDto owner);
    CompanyDto ColManAddToCar(CompanyDto company, IdDto owner);
    List<IdDto> ColDeleteMultipleCompanyFromCar(List<IdDto> collection, IdDto owner);
    PersonnelAdviseContainerWithColumns Advise(IdCodeDto company);
}