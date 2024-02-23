using Mapster;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class OfferingsService : IOfferingsService
{
    private readonly IOfferingsRepository _offeringsRepository;
    private readonly IProvidersRepository _providersRepository;

    public OfferingsService(IOfferingsRepository offeringsRepository, IProvidersRepository providersRepository)
    {
        _offeringsRepository = offeringsRepository;
        _providersRepository = providersRepository;
    }

    public async Task<ResultResponse<OfferResponseDTO>> CreateOfferingsAsync(OfferRequestDTO offeringsDTO,
        Guid providerId)
    {
        var result = new ResultResponse<OfferResponseDTO>();
        try
        {
            var offer = offeringsDTO.Adapt<Offerings>();
            var provider = _providersRepository.GetById(providerId); // getdatabase provider
            if (provider != null)
            {
                offer.Providers = provider;
                var resultOffer = (await _offeringsRepository.AddAsync(offer)).Adapt<OfferResponseDTO>();
                resultOffer.ProviderResponse = provider.Adapt<ProviderResponseDTO>();
                result.Data = resultOffer;
                result.Success = true;
                result.Messages = "Create Offer Successfully";
                result.Code = 201;
            }
            else
            {
                result.Success = false;
                result.Code = 300;
                result.Messages = "PROVIDER_NOT_FOUND";
            }
        }
        catch (Exception ex)
        {
            result.Messages = ex.Message;
        }

        return result;
    }

    public List<OfferResponseDTO> GetAllOfferings()
    {
        return _offeringsRepository.GetAll().ProjectToType<OfferResponseDTO>().ToList();
    }

    public async Task<ResultResponse<OfferResponseDTO>> GetOfferingsAsync(Guid offeringsId)
    {
        var result = new ResultResponse<OfferResponseDTO>();
        var offer = _offeringsRepository.GetById(offeringsId);
        if (offer == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "OFFER_NOT_FOUND";
        }
        else
        {
            result.Code = 200;
            result.Data = offer.Adapt<OfferResponseDTO>();
            result.Success = true;
            result.Messages = "FIND_OFFER_IN_DATABASE";
        }

        return result;
    }

    public async Task<ResultResponse<OfferResponseDTO>> UpdateOfferingsAsync(Guid offeringsId,
        OfferRequestDTO offeringsDTO)
    {
        var result = new ResultResponse<OfferResponseDTO>();
        var offer = _offeringsRepository.GetById(offeringsId);
        if (offer == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "OFFER_NOT_FOUND";
            return result;
        }

        offer.Description = offeringsDTO.Description;
        offer.ServiceName = offeringsDTO.ServiceName;
        offer.Price = offeringsDTO.Price;
        _offeringsRepository.Update(offer);

        result.Code = 200;
        result.Success = true;
        result.Data = _offeringsRepository.GetById(offeringsId).Adapt<OfferResponseDTO>();
        result.Messages = "UPDATE_OFFER_SUCCESSFULLY";
        return result;
    }
}