﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
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
        List<Guid> listProvider)
    {
        var result = new ResultResponse<OfferResponseDTO>();
        try
        {
            var offer = offeringsDTO.Adapt<Offerings>();
            offer.OfferProviders = new List<OfferProviders>();
            listProvider.ForEach(_ =>
            {
                var provider = _providersRepository.GetById(_);
                if (provider != null && offer.OfferProviders != null
                && !offer.OfferProviders.Any(op => op.Providers != null && op.Providers.Id == provider.Id))
                {
                    offer.OfferProviders.Add(new OfferProviders
                    {
                        Providers = provider
                    });
                }
            });
            var resultOffer = (await _offeringsRepository.AddAsync(offer)).Adapt<OfferResponseDTO>();
            result.Data = resultOffer;
            result.Success = true;
            result.Messages = "Create Offer Successfully";
            result.Code = 201;

        }
        catch (Exception ex)
        {
            result.Messages = ex.Message;
        }

        return result;
    }

    public bool DeleteOfferings(Guid offeringsId)
    {
        try
        {
            _offeringsRepository.Delete(offeringsId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<List<OfferResponseDTO>> GetByCriteria(OfferingsQueryDto query)
    {
        
        if(!string.IsNullOrEmpty(query.Name) && !string.IsNullOrEmpty(query.Category))
        {
            var data = _offeringsRepository.GetByCriteria(x=> x.ServiceName.Contains(query.Name) && x.Category.Contains(query.Category));
            return data;
        }
        
        if (!string.IsNullOrEmpty(query.Name))
        {
            var data = _offeringsRepository.GetByCriteria(x=> x.ServiceName.Contains(query.Name));
            return data;
        }

        if (!string.IsNullOrEmpty(query.Category))
        {
            var data = _offeringsRepository.GetByCriteria(x => x.Category.Contains(query.Category));
            return data;
        }
        
        

        return Task.FromResult(GetAllOfferings());

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
        OfferRequestDTO offeringsDTO, List<Guid> listProvider)
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
        listProvider.ForEach(_ =>
        {
            var provider = _providersRepository.GetById(_);
            if (provider != null && offer.OfferProviders != null
            && !offer.OfferProviders.Any(op => op.Providers != null && op.Providers.Id == provider.Id))
            {
                offer.OfferProviders.Add(new OfferProviders
                {
                    Providers = provider
                });
            }
        });

        offer.Description = offeringsDTO.Description;
        offer.ServiceName = offeringsDTO.ServiceName;
        offer.Price = offeringsDTO.Price;
        offer.Image = offeringsDTO.Image;
        offer.Category = offeringsDTO.Category;
        _offeringsRepository.Update(offer);
        result.Code = 200;
        result.Success = true;
        result.Data = _offeringsRepository.GetById(offeringsId).Adapt<OfferResponseDTO>();
        result.Messages = "UPDATE_OFFER_SUCCESSFULLY";
        return result;
    }
}