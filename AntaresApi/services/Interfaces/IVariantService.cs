using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.Variant;

namespace AntaresApi.Services.Interfaces;

public interface IVariantService
{
    IEnumerable<VariantDto> GetAllVariant();
    VariantDto GetVariantById(long id);
    VariantDto CreateVariant(VariantDto variant);
    VariantDto UpdateVariant(VariantDto variant);
    VariantDto DeleteVariant(long id);
    List<IdDto> DeleteMultipleVariant(List<IdDto> variants);
    VariantDto ColManAddToRecruitment(VariantDto variant, IdDto owner);
    List<IdDto> ColManDeleteFromRecruitment(List<IdDto> collection,  IdDto owner);

}