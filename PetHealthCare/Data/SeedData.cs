using PetHealthCare.Model;
using PetHealthCare.Model.Enums;
using PetHealthCare.Repository;
using PetHealthCare.Utils;
using System.Data;

namespace PetHealthCare.Data;

public class SeedData
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;

    public SeedData(IRoleRepository roleRepository, IMembershipRepository membershipRepository, IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }
    public void Initialize()
    {
        if (!_roleRepository.GetAll().Any())
        {
            _roleRepository.AddAllRole(new List<Role> {
                   new Role {
                               RoleName = RoleName.ADMIN,
                               CreateDateTime = DateTime.Now
                            },
                   new Role {
                               RoleName = RoleName.OWNER,
                               CreateDateTime = DateTime.Now
                            },
                    new Role
                            {
                               RoleName = RoleName.MEMBER,
                               CreateDateTime = DateTime.Now
                            },
                    new Role
                            {
                               RoleName = RoleName.MODERATOR,
                               CreateDateTime = DateTime.Now
                            },
                     new Role
                            {
                               RoleName = RoleName.CARETAKER,
                               CreateDateTime = DateTime.Now
                            }
            });
        }
        if (!_userRepository.GetAll().Any())
        {
            PasswordHashUtils.CreatePasswordHash("SuperAdmin", out var passwordHash, out var passwordSalt);
            Role role = _roleRepository.GetAll().FirstOrDefault(r => r.RoleName == RoleName.ADMIN);
            var user = _userRepository.AddAsync(new Users
            {
                Avatar = "https://indotel.com.vn/wp-content/uploads/2022/07/hinh-anh-du-lich-ha-long8.jpg",
                Email = "Admin@gmail.com",
                DateOfBirth = "09/09/2001",
                FullName = "Admin",
                Address = "Admin",
                Status = UserStatus.INACTIVE,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = "sd",
                UsersRoles = new List<UsersRole> { new() { Role = role } }
            });
        }

        if (!_membershipRepository.GetAll().Any())
        {
            _membershipRepository.AddAllMembership(new List<Membership>() {
                new Membership{
                    Status = MembershipStatus.BASIC,
                    CreateDateTime = DateTime.Now,
                    Amount = 0
                },
                 new Membership{
                    Status = MembershipStatus.PRO_ONE_MONTH,
                    CreateDateTime = DateTime.Now,
                    Amount = 100000
                },
                  new Membership{
                    Status = MembershipStatus.PRO_THREE_MONTHS,
                    CreateDateTime = DateTime.Now,
                    Amount = 280000
                },
                   new Membership{
                    Status = MembershipStatus.PRO_SIX_MONTHS,
                    CreateDateTime = DateTime.Now,
                    Amount = 550000
                }
            });
        }

    }
}
