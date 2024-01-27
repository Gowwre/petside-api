﻿using Mapster;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class PetService : IPetService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPetRepository _petRepository;
    private readonly IUserRepository _userRepository;

    public PetService(IUserRepository userRepository, IPetRepository petRepository,
        IAppointmentRepository appointmentRepository)
    {
        _userRepository = userRepository;
        _petRepository = petRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<ResultResponse<PetResponserDTO>> CreatePetAsync(PetRequestDTO petRequestDTO, Guid userId)
    {
        var result = new ResultResponse<PetResponserDTO>();
        var ownerPet = _userRepository.GetAll().Where(u => u.Id == userId).FirstOrDefault();
        if (ownerPet == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "OWNER_NOT_FOUND";
            return result;
        }

        try
        {
            var pet = new Pets();
            petRequestDTO.Adapt(pet);
            pet.Users = ownerPet;
            result.Data = (await _petRepository.AddAsync(pet)).Adapt<PetResponserDTO>();
            result.Code = 201;
            result.Success = true;
            result.Messages = "Create Pet Successfully";
        }
        catch (Exception ex)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = ex.Message;
        }

        return result;
    }

    public List<PetResponserDTO> GetAllPet()
    {
        // _offeringsRepository.GetAll().ProjectToType<OfferResonseDTO>().ToList();
        return _petRepository.GetAll().ProjectToType<PetResponserDTO>().ToList();
    }

    public async Task<ResultResponse<PetResponserDTO>> GetPetAsync(Guid petId)
    {
        var result = new ResultResponse<PetResponserDTO>();
        var pet = _petRepository.GetById(petId);
        if (pet == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "PET_NOT_FOUND";
            return result;
        }

        result.Code = 200;
        result.Success = true;
        result.Data = pet.Adapt<PetResponserDTO>();
        result.Messages = "Get Pet Successfully";
        return result;
    }

    public async Task<ResultResponse<PetResponserDTO>> UpdatePetAsync(Guid petId, PetRequestDTO petRequestDTO)
    {
        var result = new ResultResponse<PetResponserDTO>();
        var pet = _petRepository.GetById(petId);
        if (pet == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "PET_NOT_FOUND";
            return result;
        }

        _petRepository.Update(petRequestDTO.Adapt(pet));

        result.Code = 200;
        result.Success = true;
        result.Data = _petRepository.GetById(petId).Adapt<PetResponserDTO>();
        result.Messages = "Update Pet Successfully";

        return result;
    }
}