using AntaresApi.Dto.Variant.VariantType;

namespace AntaresApi.Services.Interfaces;

public interface IVariantTypeService
{
    IEnumerable<VariantTypeDto> GetAllVariantTypes();
    VariantTypeDto GetVariantTypeById(long id);
}