using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IOfferingsService
{
    public Task<ResultResponse<OfferResponseDTO>> CreateOfferingsAsync(OfferRequestDTO offeringsDTO, Guid providerId);
    public Task<ResultResponse<OfferResponseDTO>> GetOfferingsAsync(Guid offeringsId);
    public List<OfferResponseDTO> GetAllOfferings();
    public Task<ResultResponse<OfferResponseDTO>> UpdateOfferingsAsync(Guid offeringsId, OfferRequestDTO offeringsDTO);
}