using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IPetService
{
    public Task<ResultResponse<Pets>> CreatePetAsync(PetDTO petDTO, Guid userId);
    public Task<ResultResponse<Pets>> GetPetAsync(Guid petId);
    public List<Pets> GetAllPet();
    public Task<ResultResponse<Pets>> UpdatePetAsync(Guid petId, PetDTO userUpdateDTO);
}
