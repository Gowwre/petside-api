using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IPetService
{
    public Task<ResultResponse<PetResponserDTO>> CreatePetAsync(PetRequestDTO petRequestDTO, Guid userId);
    public Task<ResultResponse<PetResponserDTO>> GetPetAsync(Guid petId);

    public Task<PaginatedResponse<PetsDTO>> GetPetsPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        string search);

    public List<PetResponserDTO> GetAllPet();
    public Task<ResultResponse<PetResponserDTO>> UpdatePetAsync(Guid petId, PetRequestDTO petRequestDTO);
}