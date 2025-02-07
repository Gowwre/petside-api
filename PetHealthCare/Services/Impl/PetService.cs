﻿using Mapster;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
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

    public async Task<ResultResponse<PetResponseDTO>> CreatePetAsync(PetRequestDTO petRequestDTO, Guid userId)
    {
        var result = new ResultResponse<PetResponseDTO>();
        var ownerPet = _userRepository.GetAll().Where(u => u.Id == userId).FirstOrDefault();
        //var appointment = _appointmentRepository.GetById(AppointmentId);
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
            //pet.Appointment = appointment;
            result.Data = (await _petRepository.AddAsync(pet)).Adapt<PetResponseDTO>();
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

    public List<PetResponseDTO> GetAllPet()
    {
        return _petRepository.GetAll().ProjectToType<PetResponseDTO>().ToList();
    }

    public async Task<ResultResponse<PetResponseDTO>> GetPetAsync(Guid petId)
    {
        var result = new ResultResponse<PetResponseDTO>();
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
        result.Data = pet.Adapt<PetResponseDTO>();
        result.Messages = "Get Pet Successfully";
        return result;
    }

    public async Task<ResultResponse<PetResponseDTO>> UpdatePetAsync(Guid petId, PetRequestDTO petRequestDTO)
    {
        var result = new ResultResponse<PetResponseDTO>();
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
        result.Data = _petRepository.GetById(petId).Adapt<PetResponseDTO>();
        result.Messages = "Update Pet Successfully";

        return result;
    }

    public async Task<PaginatedResponse<PetsDTO>> GetPetsPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        string search)
    {
        var pets = await _petRepository.FindPaginAsync<PetsDTO>(
            getWithPaginationQueryDTO.PageNumber,
            getWithPaginationQueryDTO.PageSize,
            _ => (_.Name != null && _.Name.Contains(search) || search == null),
            _ => _.OrderBy(p => p.Name)
        );
        return await pets.ToPaginatedResponseAsync();
    }

    public bool DeletePet(Guid petId)
    {
        try
        {
            _petRepository.Delete(petId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}