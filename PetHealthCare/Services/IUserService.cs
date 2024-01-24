using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IUserService
{
    public Task<Users> LoginAsync(LoginDTO loginDTO);
    public Task<ResultResponse<Users>> CreateUserAsync(UserRegistrationDto userRegistrationDto);
    public Task<ResultResponse<Users>> GetUserAsync(Guid userId);
    public List<Users> GetAllUser();
    public Task<ResultResponse<Users>> UpdateUserAsync(Guid userId, UserUpdateDTO userUpdateDTO);
    public Task<PaginatedList<Users>> GetUserPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO);
}