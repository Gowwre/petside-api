using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;
using PetHealthCare.Utils;

namespace PetHealthCare.Services.Impl;

public class ProvidersService : IProvidersService
{
    private readonly IProvidersRepository _providersRepository;
    private readonly IOfferingsRepository _offeringsRepository;

    public ProvidersService(IProvidersRepository providersRepository, IOfferingsRepository offeringsRepository)
    {
        _providersRepository = providersRepository;
        _offeringsRepository = offeringsRepository;
    }

    public async Task<ResultResponse<ProviderResponseDTO>> CreateProvidersAsync(ProviderRequestDTO providersDTO)
    {
        var result = new ResultResponse<ProviderResponseDTO>();
        try
        {
            var providerAddDb = providersDTO.Adapt<Providers>();
            providerAddDb.Username = providersDTO.UserNameLogin != null ? providersDTO.UserNameLogin : "ShopPet";
            PasswordHashUtils.CreatePasswordHash(providersDTO.Password != null ? providersDTO.Password : "1", out var passwordHash, out var passwordSalt);
            providerAddDb.PasswordHash = passwordHash;
            providerAddDb.PasswordSalt = passwordSalt;
            var provider = await _providersRepository.AddAsync(providerAddDb);
            result.Data = provider.Adapt<ProviderResponseDTO>();
            result.Success = true;
            result.Messages = "Create Providers Successfully";
            result.Code = 201;
        }
        catch (Exception ex)
        {
            result.Messages = ex.Message;
        }

        return result;
    }

    public bool DeleteProviders(Guid providersId)
    {
        try
        {
            _providersRepository.Delete(providersId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
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

    public List<ProviderResponseDTO> GetProvidersCategory(string? serviceType)
    {
        return _providersRepository.GetAll().Where(_ =>
             (serviceType != null && _.ServiceType != null && _.ServiceType.Contains(serviceType)) || serviceType == null
         ).ProjectToType<ProviderResponseDTO>().ToList();
    }

    public async Task<ResultResponse<ProviderResponseDTO>> loginProvidersAsync(LoginProviderDTO loginProviderDTO)
    {
        var result = new ResultResponse<ProviderResponseDTO>();
        var provider = await _providersRepository.GetAll().Where(p => p.Username == loginProviderDTO.UserName).FirstOrDefaultAsync();
        if (provider == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "INCORRECT_USERNAME_OR_PASSWORD";
        }
        else
        {
            if (!PasswordHashUtils.VerifyPasswordHash(loginProviderDTO.Password, provider.PasswordHash, provider.PasswordSalt))
            {
                result.Code = 300;
                result.Success = false;
                result.Messages = "INCORRECT_USERNAME_OR_PASSWORD";
                return result;
            }
            result.Code = 200;
            result.Data = provider.Adapt<ProviderResponseDTO>();
            result.Success = true;
            result.Messages = "FIND_PROVIDER_IN_DATABASE";
        }

        return result;
    }

    public async Task<ResultResponse<ProviderResponseDTO>> UpdateProvidersAsync(Guid providersId,
        ProviderRequestDTO providersDTO, List<Guid> listOffering)
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

        listOffering.ForEach(_ =>
        {
            var offering = _offeringsRepository.GetById(_);
            if (offering != null && provider.OfferProviders != null
            && !provider.OfferProviders.Any(op => op.Offerings != null && op.Offerings.Id == offering.Id))
            {
                provider.OfferProviders.Add(new OfferProviders
                {
                    Offerings = offering
                });
            }
        });

        _providersRepository.Update(providersDTO.Adapt(provider));
        result.Code = 200;
        result.Success = true;
        result.Data = _providersRepository.GetById(providersId).Adapt<ProviderResponseDTO>();
        result.Messages = "UPDATE_PROVIDER_SUCCESSFULLY";
        return result;
    }
}