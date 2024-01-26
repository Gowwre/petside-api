using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IOfferingsService
{
    public Task<ResultResponse<OfferResonseDTO>> CreateOfferingsAsync(OfferRequestDTO offeringsDTO, Guid providerId);
    public Task<ResultResponse<OfferResonseDTO>> GetOfferingsAsync(Guid offeringsId);
    public List<OfferResonseDTO> GetAllOfferings();
    public Task<ResultResponse<OfferResonseDTO>> UpdateOfferingsAsync(Guid offeringsId, OfferRequestDTO offeringsDTO);
}