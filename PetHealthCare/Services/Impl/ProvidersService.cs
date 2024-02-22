using Mapster;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class ProvidersService : IProvidersService
{
    private readonly IProvidersRepository _providersRepository;

    public ProvidersService(IProvidersRepository providersRepository)
    {
        _providersRepository = providersRepository;
    }

    public async Task<ResultResponse<ProviderResponseDTO>> CreateProvidersAsync(ProviderRequestDTO providersDTO)
    {
        var result = new ResultResponse<ProviderResponseDTO>();
        try
        {
            var provider = await _providersRepository.AddAsync(providersDTO.Adapt<Providers>());
            result.Data = provider.Adapt<ProviderResponseDTO>();
            result.Success = true;
            result.Messages = "Create Providers Successfully";
            result.Code = 201;
        }
        catch (Exception ex)
        {
            //result.Messages = "CREATE_PROVIDER_FAIL";
            result.Messages = ex.Message;
        }

        return result;
    }

    public List<ProviderResponseDTO> GetAllProviders(string? name)
    {
        return _providersRepository.GetAll().Where(_ =>
            (name != null && _.ProviderName != null && _.ProviderName.Contains(name)) || name == null
        ).ProjectToType<ProviderResponseDTO>().ToList();
    }

    public async Task<ResultResponse<ProviderResponseDTO>> GetProvidersAsync(Guid providersId)
    {
        var result = new ResultResponse<ProviderResponseDTO>();
        var provider = _providersRepository.GetById(providersId);
        if (provider == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "PROVIDER_NOT_FOUND";
        }
        else
        {
            result.Code = 200;
            result.Data = provider.Adapt<ProviderResponseDTO>();
            result.Success = true;
            result.Messages = "FIND_PROVIDER_IN_DATABASE";
        }

        return result;
    }

    public async Task<ResultResponse<ProviderResponseDTO>> UpdateProvidersAsync(Guid providersId,
        ProviderRequestDTO providersDTO)
    {
        var result = new ResultResponse<ProviderResponseDTO>();
        var provider = _providersRepository.GetById(providersId);
        if (provider == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "PROVIDER_NOT_FOUND";
            return result;
        }

        //providersDTO.ProviderId = provider.Id;
        _providersRepository.Update(providersDTO.Adapt(provider));
        result.Code = 200;
        result.Success = true;
        result.Data = _providersRepository.GetById(providersId).Adapt<ProviderResponseDTO>();
        result.Messages = "UPDATE_PROVIDER_SUCCESSFULLY";
        return result;
    }
}