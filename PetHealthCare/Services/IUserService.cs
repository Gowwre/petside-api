using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IUserService
{
    public Task<UserDTO> LoginAsync(LoginDTO loginDTO);

    public Task<string> ForgotPassword(string email);
    public Task<ResultResponse<UserDTO>> CreateUserAsync(UserRegistrationDto userRegistrationDto);
    public ResultResponse<UserDTO> GetUserAsync(Guid userId);
    public ResultResponse<UserDTO> UpdateUserAsync(Guid userId, UserUpdateDTO userUpdateDTO);
    public Task<PaginatedResponse<UserDTO>> GetUsersPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO, string? name);
    public ResultResponse<UserDTO> ChangePassword(ChangePassowordDTO passowordDTO);
    public ResultResponse<UserDTO> UpgradeAccountUser(Guid userId, Guid membership);
    public IEnumerable<UserDTO> SearchUserByName(string? name);

    Task<PaginatedResponse<PetsDTO>> GetPetsByUserId(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        Guid id,string? search);
}