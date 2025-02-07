﻿using Mapster;
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
    private readonly IEmailService _emailService;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IPetRepository _petRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemberUserRepository _memberRepository;

    public UserService(IRoleRepository roleRepository, IMemberUserRepository memberRepository, IUserRepository userRepository, IEmailService emailService,
        IMembershipRepository membershipRepository, IPetRepository petRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _memberRepository = memberRepository;
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
        //var memberShip = _membershipRepository.GetAll().Where(m => m.Status == MembershipStatus.BASIC).FirstOrDefault();
        PasswordHashUtils.CreatePasswordHash(userRegistrationDto.Password, out var passwordHash, out var passwordSalt);
        try
        {
            result.Data = (await _userRepository.AddAsync(new Users
            {

                FullName = userRegistrationDto.FullName != null
                    ? userRegistrationDto.FullName
                    : userRegistrationDto.Email,
                Email = userRegistrationDto.Email,
                Avatar = "https://firebasestorage.googleapis.com/v0/b/swd-longchim.appspot.com/o/images%2F143086968_2856368904622192_1959732218791162458_n.png?alt=media&token=009e2c55-5541-48bf-9442-c410dfb5738a",
                DateOfBirth = "01/01/2000",
                Address = "tp Hồ Chí Minh",
                IsUpgrade = false,
                //UpgradeDate = DateTime.Now,
                Status = UserStatus.INACTIVE,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                //MemberUsers = new List<MemberUser> { new() { Membership = memberShip } },
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
            Random random = new Random();
            int randomNumber = random.Next(0, 9999);
            string formattedNumber = randomNumber.ToString("D4");
            await _emailService.SendEmailAsync(email, "Send Temporary Password", $"Your temporary password is <b>{formattedNumber}</b>");
            user.OtpEmail = Convert.ToInt32(formattedNumber);
            _userRepository.Update(user);
        }
        return user == null ? "Cant Not Find Email User In Data" : "Send Temporary Passwrod In Mail";
    }

    public async Task<PaginatedResponse<UserDTO>> GetUsersPagin(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        string? name, bool CheckIsUpgrade)
    {
        if (CheckIsUpgrade)
        {
            var product = await _userRepository.FindPaginAsync<UserDTO>(
                        getWithPaginationQueryDTO.PageNumber,
                        getWithPaginationQueryDTO.PageSize,
                        u => ((name != null && u.FullName.Contains(name)) || name == null) &&
                             (u.UsersRoles == null || !u.UsersRoles.Any(ur =>
                                 ur.Role != null && ur.Role.RoleName == RoleName.ADMIN)) && u.IsUpgrade,
                        _ => _.OrderByDescending(u => u.UpgradeDate)
                    );
            return await product.ToPaginatedResponseAsync();
        }
        else
        {
            var product = await _userRepository.FindPaginAsync<UserDTO>(
                getWithPaginationQueryDTO.PageNumber,
                getWithPaginationQueryDTO.PageSize,
                u => ((name != null && u.FullName.Contains(name)) || name == null) &&
                     (u.UsersRoles == null || !u.UsersRoles.Any(ur =>
                         ur.Role != null && ur.Role.RoleName == RoleName.ADMIN)),
                _ => _.OrderBy(u => u.FullName)
            );
            return await product.ToPaginatedResponseAsync();
        }

    }

    public ResultResponse<UserDTO> ChangePassword(ChangePassowordDTO passowordDTO)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetAll()?.Where(_ => _.Email == passowordDTO.Email).FirstOrDefault();

        if (user != null)
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

    public async Task<ResultResponse<UserDTO>> UpgradeAccountUser(Guid userId)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetById(userId);
        user.Upgraded = true; // admin đã đồng ý xác nhận 
        user.ExpirationDate = DateTime.Now.AddMonths(3);
        user.IsUpgrade = false;
        // sửa lại gói 3 tháng thành 109 
        // Sending email
        var emailSubject = "Congratulations on Your Account Upgrade";
        var emailBody = @"<!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Congratulations on Your Account Upgrade</title>
                    </head>
                    <body>
                        <div style='font-family: Arial, sans-serif;'>
                            <h2>Congratulations!</h2>
                            <p>Your account has been upgraded to the Pro service package.</p>
                            <p>The duration of this service package is 3 months, starting from today.</p>
                            <p>Thank you for using our services.</p>
                            <img src='https://support.content.office.net/en-us/media/7dbd87dd-c244-4d78-8fda-4408a08582cc.jpg' alt='Congratulations Image' style='max-width: 100%;'>
                        </div>
                    </body>
                    </html>";

        await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);
        var memberPackage = _membershipRepository.GetAll().Where(m => m.Status == MembershipStatus.PRO_THREE_MONTHS).FirstOrDefault();
        if (user != null && memberPackage != null)
        {
            // thieu payment
            user.UpgradeDate = DateTime.Now;
            var nowDay = DateTime.Now;
            user.MemberUsers?.Add(new MemberUser
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
            result.Messages = "Upgrade Account User Success";
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
            return _userRepository.GetAll()
                .Where(_ => _.UsersRoles != null &&
                            !_.UsersRoles.Any(ur => ur.Role != null && ur.Role.RoleName == RoleName.ADMIN))
                .ProjectToType<UserDTO>()
                .ToList();

        return _userRepository.GetAll()
            .Where(_ => _.FullName.Contains(name) &&
                        _.UsersRoles != null &&
                        !_.UsersRoles.Any(ur => ur.Role != null && ur.Role.RoleName == RoleName.ADMIN))
            .ProjectToType<UserDTO>()
            .ToList();
    }

    public async Task<PaginatedResponse<PetsDTO>> GetPetsByUserId(GetWithPaginationQueryDTO getWithPaginationQueryDTO,
        Guid id, string? search)
    {
        var result = await _petRepository.FindPaginAsync<PetsDTO>(getWithPaginationQueryDTO.PageNumber,
            getWithPaginationQueryDTO.PageSize,
            _ => _.UsersId == id || (_.UsersId == id && _.Name != null && _.Name.Contains(search)),
            _ => _.OrderBy(p => p.Name));

        return await result.ToPaginatedResponseAsync();
    }
    public ResultResponse<UserDTO> ConfirmEmailOTP(int OTP, string emai)
    {
        var result = new ResultResponse<UserDTO>();
        var user = _userRepository.GetAll().Where(_ => _.Email == emai && _.OtpEmail == OTP).FirstOrDefault();
        if (user != null)
        {
            result.Code = 200;
            result.Data = user.Adapt<UserDTO>();
            result.Success = true;
            result.Messages = $"OTP IS TRUE";
        }
        else
        {
            result.Code = 300;
            result.Data = null;
            result.Success = false;
            result.Messages = $"OTP IS FALSE";
        }
        return result;
    }

    public bool DeleteUser(Guid userId)
    {
        try
        {
            _userRepository.Delete(userId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<string> UserRegisterUpgrade(Guid userId)
    {
        // thiếu giửi mail cho admin 
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return "User Is Not Found";
        }
        var emailSubject = "Your Account Upgrade Request is Under Review";
        var emailBody = @"<!DOCTYPE html>
             <html lang='en'>
             <head>
                 <meta charset='UTF-8'>
                 <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                 <title>Your Account Upgrade Request is Under Review</title>
             </head>
             <body>
                 <div style='font-family: Arial, sans-serif;'>
                     <h2>Notification: Your Account Upgrade Request is Under Review</h2>
                     <p>Wellcome To Petside</p>
                     <p>This is to inform you that your request to upgrade your account is currently under review by our admin team.</p>
                     <p>Please allow 1-2 days for us to process your request. We appreciate your patience.</p>
                     <p>Thank you for choosing our services.</p>
                     <img src='https://timo.vn/wp-content/uploads/tiny-people-with-laptop-financial-digital-transformation-open-banking-platform-online-banking-system-finance-digital-transformation-concept-illustration_335657-2529.jpg' alt='Congratulations Image' style='max-width: 100%;'>
                 </div>
             </body>
             </html>";

        await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);
        user.IsUpgrade = true;

        _userRepository.Update(user);
        return "Rigister Upgrade Account To Pro Successfully";
    }

    public async Task<string> RemoveRegisterUpgrade(Guid userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return "User Is Not Found";
        }
        var emailSubject = "Cancellation of Pro Account Upgrade";

        var emailBody = @"<!DOCTYPE html>

             <html lang='en'>
             <head>
                 <meta charset='UTF-8'>
                 <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                 <title>Cancellation of Pro Account Upgrade</title>
             </head>
             <body>
                 <div style='font-family: Arial, sans-serif;'>
                     <h2>Cancellation of Pro Account Upgrade</h2>
                     <p>We regret to inform you that your recent upgrade to the Pro service package has been cancelled.</p>
                     <p>Upon reviewing your account preferences, it seems there might have been a misunderstanding or a change in your requirements. As such, we have reverted your account back to its previous subscription status.</p>
                     <p>If you have any questions or concerns regarding this change, please do not hesitate to reach out to our customer support team. We are here to assist you in any way we can.</p>
                     <p>Thank you for considering our Pro service package, and we apologize for any inconvenience this may have caused. We value your continued support and look forward to serving you in the future.</p>
                     <img src='https://olivetech.com/wp-content/uploads/2022/07/blog-digi-trans-fin.png' alt='Cancellation Image' style='max-width: 100%;'>
                 </div>
             </body>
             </html>";

        await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);
        user.IsUpgrade = false;

        _userRepository.Update(user);
        return "Remove Upgrade Account To Pro Successfully";
    }
}