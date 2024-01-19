using AntaresApi.Dto.Common;

namespace AntaresApi.Dto;

public class ColCompanyDto
{
    public string OwnerName { get; set; }
    public IdDto Owner { get; set; }
    public CompanyDto Object { get; set; }
}