using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IProvidersService
{
    public Task<ResultResponse<ProviderResponseDTO>> CreateProvidersAsync(ProviderRequestDTO providersDTO);
    public Task<ResultResponse<ProviderResponseDTO>> GetProvidersAsync(Guid providersId);
    public List<ProviderResponseDTO> GetAllProviders();
    public Task<ResultResponse<ProviderResponseDTO>> UpdateProvidersAsync(Guid providersId, ProviderRequestDTO providersDTO);
}
