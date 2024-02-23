﻿using System.Formats.Tar;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;
using PetHealthCare.Repository;
using PetHealthCare.Utils;

namespace PetHealthCare.Services.Impl;

public class UserService : IUserService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IPetRepository _petRepository;

    public UserService(IRoleRepository roleRepository, IUserRepository userRepository, IEmailService emailService,
        IMembershipRepository membershipRepository, IPetRepository petRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _emailService = emailService;
        _membershipRepository = membershipRepository;
        _petRepository = petRepository;
    }

    public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
    {
        var user = _userRepository.GetAll().Where(x => x.Email == loginDTO.Email).FirstOrDefault();
        if (user == null) throw new Exception("ACCOUNT_NOT_FOUND");
        if (!PasswordHashUtils.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("INVALID_PASSWORD");
        return user.Adapt<UserDTO>();
    }

    public async Task<ResultResponse<UserDTO>> CreateUserAsync(UserRegistrationDto userRegistrationDto)
    {
        var result = new ResultResponse<UserDTO>();
        var roleOwner = _roleRepository.GetAll().Where(x => x.RoleName == RoleName.OWNER).FirstOrDefault();
        var memberShip = _membershipRepository.GetAll().Where(m => m.Status == MembershipStatus.BASIC).FirstOrDefault();
        PasswordHashUtils.CreatePasswordHash(userRegistrationDto.Password, out var passwordHash, out var passwordSalt);
        try
        {
            result.Data = (await _userRepository.AddAsync(new Users
            {
                FullName = userRegistrationDto.FullName != null
                    ? userRegistrationDto.FullName
                    : userRegistrationDto.Email,
                Email = userRegistrationDto.Email,
                Status = UserStatus.INACTIVE,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                MemberUsers = new List<MemberUser> { new() { Membership = memberShip } },
                UsersRoles = new List<UsersRole> { new() { Role = roleOwner } }
            })).Adapt<UserDTO>();
            result.Success = true;
            result.Messages = "Create User Successfully";
            result.Code = 201;
        }
        catch (DbUpdateException ex)
        {
            result.Messages = "EMAIL_ALREADY_EXISTS";
        }
        catch (Exception ex)
        {
            result.Messages = ex.Message;
        }

        return result;
    }

    public ResultResponse<UserDTO> UpdateUserAsync(Guid userId, UserUpdateDTO userUpdateDTO)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "USER_NOT_FOUND";
            return result;
        }

        user.FullName = userUpdateDTO.Fullname;
        user.Avatar = userUpdateDTO.Avatar;
        user.DateOfBirth = userUpdateDTO.BirthDay;
        user.Address = userUpdateDTO.Address;
        user.PhoneNumber = userUpdateDTO.PhoneNumber;
        _userRepository.Update(user);

        result.Success = true;
        result.Messages = "Update User Successfully";
        result.Code = 200;
        result.Data = _userRepository.GetById(userId).Adapt<UserDTO>();
        return result;
    }

    public ResultResponse<UserDTO> GetUserAsync(Guid userId)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "USER_NOT_FOUND";
        }
        else
        {
            result.Code = 200;
            result.Data = user.Adapt<UserDTO>();
            result.Success = true;
            result.Messages = "Find User In Database";
        }

        return result;
    }

    public async Task<string> ForgotPassword(string email)
    {
        var user = _userRepository.GetAll()?.Where(_ => _.Email == email).FirstOrDefault();
        if (user != null)
        {
            Guid password = Guid.NewGuid();
            PasswordHashUtils.CreatePasswordHash(password.ToString(), out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Update(user);
            await _emailService.SendEmailAsync(email, "Send Temporary Password",
                $"Your temporary password is <b>{password}</b>");
        }

        return user == null ? "Cant Not Find Email User In Data" : "Send Temporary Passwrod In Mail";
    }

    public async Task<PaginatedResponse<UserDTO>> GetUsersPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        string? name)
    {
        PaginatedList<UserDTO> product = await _userRepository.FindPaginAsync<UserDTO>(
            getWithPaginationQueryDTO.PageNumber,
            getWithPaginationQueryDTO.PageSize,
            expression: u => ((name != null && u.FullName.Contains(name)) || name == null) &&
                             (u.UsersRoles == null || !u.UsersRoles.Any(ur =>
                                 ur.Role != null && ur.Role.RoleName == RoleName.ADMIN)),
            orderBy: _ => _.OrderBy(u => u.FullName)
        );
        return await product.ToPaginatedResponseAsync();
    }

    public ResultResponse<UserDTO> ChangePassword(ChangePassowordDTO passowordDTO)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetAll()?.Where(_ => _.Email == passowordDTO.Email).FirstOrDefault();

        if (user != null &&
            PasswordHashUtils.VerifyPasswordHash(passowordDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            PasswordHashUtils.CreatePasswordHash(passowordDTO.NewPassword, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Update(user);
            result.Code = 200;
            result.Data = _userRepository.GetById(user.Id).Adapt<UserDTO>();
            result.Success = true;
            result.Messages = $"Find User Have Email {passowordDTO.Email}";
        }
        else
        {
            result.Code = 300;
            result.Data = null;
            result.Success = false;
            result.Messages = "Email Or Password Is Not Correct ";
        }

        return result;
    }

    public ResultResponse<UserDTO> UpgradeAccountUser(Guid userId, Guid membership)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetById(userId);
        var memberPackage = _membershipRepository.GetById(membership);
        if (user != null && memberPackage != null)
        {
            // thieu payment

            DateTime nowDay = DateTime.Now;
            user.MemberUsers?.Add(new()
            {
                Membership = memberPackage,
                CreateDay = nowDay,
                ExpiredDay = memberPackage.Status switch
                {
                    MembershipStatus.PRO_ONE_MONTH => DateTime.Now.AddMonths(1),
                    MembershipStatus.PRO_THREE_MONTHS => DateTime.Now.AddMonths(3),
                    MembershipStatus.PRO_SIX_MONTHS => DateTime.Now.AddMonths(6),
                    _ => DateTime.Now
                },
                TotalAmount = memberPackage.Amount
            });
            _userRepository.Update(user);
            result.Code = 200;
            result.Data = _userRepository.GetById(user.Id).Adapt<UserDTO>();
            result.Success = true;
            result.Messages = $"Upgrade Account User Success";
        }
        else
        {
            result.Code = 300;
            result.Data = null;
            result.Success = false;
            result.Messages = user == null ? "Can Not Find User In Database" : "Can Not Find Package In Database";
        }

        return result;
    }

    public IEnumerable<UserDTO> SearchUserByName(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return _userRepository.GetAll()
                .Where(_ => _.UsersRoles != null &&
                            !_.UsersRoles.Any(ur => ur.Role != null && ur.Role.RoleName == RoleName.ADMIN))
                .ProjectToType<UserDTO>()
                .ToList();
        }

        return _userRepository.GetAll()
            .Where(_ => _.FullName.Contains(name) &&
                        (_.UsersRoles != null &&
                         !_.UsersRoles.Any(ur => ur.Role != null && ur.Role.RoleName == RoleName.ADMIN)))
            .ProjectToType<UserDTO>()
            .ToList();
    }

    public async Task<PaginatedResponse<PetsDTO>> GetPetsByUserId(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        Guid id,string? search)
    {
        var result = await _petRepository.FindPaginAsync<PetsDTO>(getWithPaginationQueryDTO.PageNumber,
            getWithPaginationQueryDTO.PageSize, expression: _ => _.UsersId == id || (_.UsersId == id && _.Name != null && _.Name.Contains(search)),
            orderBy: _ => _.OrderBy(p => p.Name));

        return await result.ToPaginatedResponseAsync();
    }
}