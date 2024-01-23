using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class PetService : IPetService
{
    private readonly IUserRepository _userRepository;
    private readonly IPetRepository _petRepository;

    public PetService(IUserRepository userRepository, IPetRepository petRepository)
    {
        _userRepository = userRepository;
        _petRepository = petRepository;
    }

    public async Task<ResultResponse<Pets>> CreatePetAsync(PetDTO petDTO, Guid userId)
    {
        ResultResponse<Pets> result = new ResultResponse<Pets>();
        var ownerPet = _userRepository.GetAll().Where(u => u.Id == userId).FirstOrDefault();
        if (ownerPet == null)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "OWNER_NOT_FOUND";
            return result;
        }
        try
        {
            result.Data = await _petRepository.AddAsync(new Pets
            {
                Name = petDTO.Name,
                Species = petDTO.Species,
                Breed = petDTO.Breed,
                BirthDate = petDTO.BirthDate,
                Age = petDTO.Age,
                Gender = petDTO.Gender,
                Weight = petDTO.Weight,
                Users = ownerPet
            });
            result.code = 201;
            result.Success = true;
            result.Messages = "Create Pet Successfully";
        }
        catch (Exception ex)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "CREATE_PET_FAIL";

        }
        return result;
    }

    public List<Pets> GetAllPet()
    {
        return _petRepository.GetAll().ToList();
    }

    public async Task<ResultResponse<Pets>> GetPetAsync(Guid petId)
    {
        ResultResponse<Pets> result = new ResultResponse<Pets>();
        var pet = _petRepository.GetById(petId);
        if (pet == null)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "PET_NOT_FOUND";
            return result;
        }
        result.code = 200;
        result.Success = true;
        result.Data = pet;
        result.Messages = "Get Pet Successfully";
        return result;
    }

    public async Task<ResultResponse<Pets>> UpdatePetAsync(Guid petId, PetDTO userUpdateDTO)
    {
        ResultResponse<Pets> result = new ResultResponse<Pets>();
        var pet = _petRepository.GetById(petId);
        if (pet == null)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "PET_NOT_FOUND";
            return result;
        }
        pet.Name = userUpdateDTO.Name;
        pet.Species = userUpdateDTO.Species;
        pet.Breed = userUpdateDTO.Breed;
        pet.BirthDate = userUpdateDTO.BirthDate;
        pet.Age = userUpdateDTO.Age;
        pet.Gender = userUpdateDTO.Gender;
        pet.Weight = userUpdateDTO.Weight;
        _petRepository.Update(pet);

        result.code = 200;
        result.Success = true;
        result.Data = _petRepository.GetById(petId);
        result.Messages = "Update Pet Successfully";

        return result;
    }
}
