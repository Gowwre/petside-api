using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IProvidersService
{
    public Task<ResultResponse<ProviderResponseDTO>> CreateProvidersAsync(ProviderRequestDTO providersDTO);
    public Task<string> CreateUserNamePasswordProvider(Guid ProviderId, string UserName, string Password);
    public Task<ResultResponse<ProviderResponseDTO>> GetProvidersAsync(Guid providersId);
    public Task<ResultResponse<ProviderResponseDTO>> loginProvidersAsync(LoginProviderDTO loginProviderDTO);
    public List<ProviderResponseDTO> GetProvidersCategory(string? search);
    public List<ProviderResponseDTO> GetAllProviders(string? name);
    public Task<ResultResponse<ProviderResponseDTO>> UpdateProvidersAsync(Guid providersId,
        ProviderRequestDTO providersDTO, List<Guid> listOffering);
    public bool DeleteProviders(Guid providersId);
}