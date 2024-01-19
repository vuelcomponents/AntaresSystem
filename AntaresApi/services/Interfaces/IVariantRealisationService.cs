using AntaresApi.Dto.Common;
using AntaresApi.Dto.Variant;
using AntaresApi.Dto.Variant.VariantRealisation;

namespace AntaresApi.Services.Interfaces;

public interface IVariantRealisationService
{
    IEnumerable<VariantRealisationDto> GetAllVariantRealisation();
    IEnumerable<VariantRealisationDto> GetAllVariantRealisationByVariantId(long variantId);
    VariantRealisationDto GetVariantRealisationById(long id);
    VariantRealisationDto CreateVariantRealisation(VariantRealisationDto variantRealisations);
    VariantRealisationDto UpdateVariantRealisation(VariantRealisationDto variantRealisations);
    VariantRealisationDto DeleteVariantRealisation(long id);
    List<IdDto> DeleteMultipleVariantRealisation(List<IdDto> variantRealisations);
}