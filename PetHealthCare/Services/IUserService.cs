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
    public Task<PaginatedResponse<UserDTO>> GetUsersPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        string? name, bool CheckIsUpgrade);

    public ResultResponse<UserDTO> ChangePassword(ChangePassowordDTO passowordDTO);
    public Task<ResultResponse<UserDTO>> UpgradeAccountUser(Guid userId);
    public Task<string> UserRegisterUpgrade(Guid userId);
    public Task<string> RemoveRegisterUpgrade(Guid userId);
    public IEnumerable<UserDTO> SearchUserByName(string? name);

    Task<PaginatedResponse<PetsDTO>> GetPetsByUserId(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        Guid id, string? search);
    public ResultResponse<UserDTO> ConfirmEmailOTP(int OTP, string emai);
    public bool DeleteUser(Guid userId);
}
