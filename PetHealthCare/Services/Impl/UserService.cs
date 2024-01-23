using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;
using PetHealthCare.Repository;
using PetHealthCare.Utils;

namespace PetHealthCare.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Users> LoginAsync(LoginDTO loginDTO)
    {
        var user = _userRepository.GetAll().Where(x => x.Email == loginDTO.Email).FirstOrDefault();
        if (user == null)
        {
            throw new Exception("ACCOUNT_NOT_FOUND");
        }
        if (!PasswordHashUtils.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("INVALID_PASSWORD");
        }
        return user;
    }
    public async Task<ResultResponse<Users>> CreateUserAsync(UserDTO userDTO)
    {
        ResultResponse<Users> result = new ResultResponse<Users>();
        var roleOwner = _roleRepository.GetAll().Where(x => x.RoleName == RoleName.OWNER).FirstOrDefault();
        PasswordHashUtils.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
        try
        {
            result.Data = await _userRepository.AddAsync(new Users
            {
                FirstName = userDTO.FullName,
                LastName = userDTO.FullName,
                Email = userDTO.Email,
                Avatar = userDTO.Avatar,
                BirthDay = userDTO.DateOfBirth,
                Address = userDTO.Address,
                Status = UserStatus.INACTIVE,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userDTO.PhoneNumber,
                UsersRoles = new List<UsersRole> { new UsersRole { Role = roleOwner } }
            });
            result.Success = true;
            result.Messages = "Create User Successfully";
            result.code = 201;
        }
        catch (Exception ex)
        {
            result.Messages = "CREATE_USER_FAIL";
            //throw new Exception("CREATE_USER_FAIL");
            //return await Result<Users>.FailureAsync("Create User Fail");
        }
        return result;
    }

    public async Task<PaginatedList<Users>> GetUserPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO)
    {
        return await PaginatedList<Users>.CreateAsync(_userRepository.GetAll()
           , getWithPaginationQueryDTO.PageNumber, getWithPaginationQueryDTO.PageSize);
    }

    public async Task<ResultResponse<Users>> UpdateUserAsync(Guid userId, UserUpdateDTO userUpdateDTO)
    {
        ResultResponse<Users> result = new ResultResponse<Users>();
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "USER_NOT_FOUND";
            return result;
        }

        user.FirstName = userUpdateDTO.FirstName;
        user.LastName = userUpdateDTO.LastName;
        user.Avatar = userUpdateDTO.Avatar;
        user.BirthDay = userUpdateDTO.BirthDay;
        user.Address = userUpdateDTO.Address;
        user.PhoneNumber = userUpdateDTO.PhoneNumber;
        _userRepository.Update(user);

        result.Success = true;
        result.Messages = "Update User Successfully";
        result.code = 200;
        result.Data = _userRepository.GetById(userId);
        return result;
    }

    public async Task<ResultResponse<Users>> GetUserAsync(Guid userId)
    {
        ResultResponse<Users> result = new ResultResponse<Users>();
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            result.code = 300;
            result.Success = false;
            result.Messages = "USER_NOT_FOUND";
        }
        else
        {
            result.code = 200;
            result.Data = user;
            result.Success = true;
            result.Messages = "Find User In Database";
        }
        return result;
    }

    public List<Users> GetAllUser()
    {
        return _userRepository.GetAll().ToList();
    }
}
