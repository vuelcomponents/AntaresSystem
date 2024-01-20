using WebApplication1.Dto.Common;

namespace WebApplication1.Dto;

public class ColCompanyDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public CompanyDto Object { get; set; }
}