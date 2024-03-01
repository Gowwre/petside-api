using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IPetService
{
    public Task<ResultResponse<PetResponseDTO>> CreatePetAsync(PetRequestDTO petRequestDTO, Guid userId);
    public Task<ResultResponse<PetResponseDTO>> GetPetAsync(Guid petId);

    public Task<PaginatedResponse<PetsDTO>> GetPetsPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
         string search);

    public List<PetResponseDTO> GetAllPet();
    public Task<ResultResponse<PetResponseDTO>> UpdatePetAsync(Guid petId, PetRequestDTO petRequestDTO);
    public bool DeletePet(Guid petId);
}