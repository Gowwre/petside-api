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

    public async Task<ResultResponse<OfferResonseDTO>> CreateOfferingsAsync(OfferRequestDTO offeringsDTO, Guid providerId)
    {
        ResultResponse<OfferResonseDTO> result = new ResultResponse<OfferResonseDTO>();
        try
        {
            var offer = offeringsDTO.Adapt<Offerings>();
            var provider = _providersRepository.GetById(providerId); // getdatabase provider
            if (provider != null)
            {
                offer.Providers = provider;
                OfferResonseDTO resultOffer = (await _offeringsRepository.AddAsync(offer)).Adapt<OfferResonseDTO>();
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

    public List<OfferResonseDTO> GetAllOfferings()
    {
        return _offeringsRepository.GetAll().ProjectToType<OfferResonseDTO>().ToList();
    }

    public async Task<ResultResponse<OfferResonseDTO>> GetOfferingsAsync(Guid offeringsId)
    {
        ResultResponse<OfferResonseDTO> result = new ResultResponse<OfferResonseDTO>();
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
            result.Data = offer.Adapt<OfferResonseDTO>();
            result.Success = true;
            result.Messages = "FIND_OFFER_IN_DATABASE";
        }
        return result;
    }

    public async Task<ResultResponse<OfferResonseDTO>> UpdateOfferingsAsync(Guid offeringsId, OfferRequestDTO offeringsDTO)
    {
        ResultResponse<OfferResonseDTO> result = new ResultResponse<OfferResonseDTO>();
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
        result.Data = _offeringsRepository.GetById(offeringsId).Adapt<OfferResonseDTO>();
        result.Messages = "UPDATE_OFFER_SUCCESSFULLY";
        return result;
    }
}
