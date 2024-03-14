using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class StatisticsService : IStatisticsService
{
    private readonly IMemberUserRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProvidersRepository _providersRepository;
    private readonly Dictionary<int, IList<MemberUser>> _listMember = new();
    private readonly Dictionary<int, StatisticsDTO> _listStatistics = new();
    private readonly Dictionary<int, IList<Users>> _listUser = new();

    public StatisticsService(IUserRepository userRepository, IMemberUserRepository memberRepository, IProvidersRepository providersRepository)
    {
        _providersRepository = providersRepository;
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        for (var i = 1; i <= 12; i++)
        {
            _listMember.Add(i, new List<MemberUser>());
            _listStatistics.Add(i, new StatisticsDTO());
            _listUser.Add(i, new List<Users>());
        }
    }

    public double GetStaticsMoney()
    {
        var TotalMoney = 0.0;
        var members = _memberRepository.GetAll().Where(_ => _.TotalAmount != null).ToList();

        foreach (var member in members)
        {
            TotalMoney += (double)(member.TotalAmount ?? 0.0);
        }
        // chỉ lấy được total gói Pro nhưng chưa lấy được tiền Appointments
        return TotalMoney;
    }

    public async Task<ProviderStaticResponse> GetStaticsProvider()
    {
        return new ProviderStaticResponse()
        {
            TotalProvider = await _providersRepository.GetAll().CountAsync(),
            ProviderInMonth = await _providersRepository.GetAll().CountAsync(p => p.CreateDateTime.Month == DateTime.UtcNow.Month)
        };
    }

    public async Task<UserStaticsResponse> GetStaticsUser()
    {
        return new UserStaticsResponse()
        {
            UserInMonth = await _userRepository.GetAll().CountAsync(u => u.CreateDateTime.Month == DateTime.UtcNow.Month),
            TotalUser = await _userRepository.GetAll().CountAsync(u => u.UsersRoles == null || !u.UsersRoles.Any(ur => ur.Role != null && ur.Role.RoleName == RoleName.ADMIN))
        };
    }
    public async Task<Dictionary<int, StatisticsDTO>> GetStatistics(int year)
    {
        var users = await _userRepository.GetAll().Where(u => u.CreateDateTime.Year == year).ToListAsync();
        var memberUser = await _memberRepository.GetAll().Where(m => m.CreateDay.Year == year).ToListAsync();
        memberUser.ForEach(m => { _listMember[m.CreateDay.Month].Add(m); });
        users.ForEach(m => { _listUser[m.CreateDateTime.Month].Add(m); });
        for (var i = 1; i <= 12; i++)
        {
            double totalInMonth = 0;
            foreach (var item in _listMember[i])
                if (item.TotalAmount != null)
                    totalInMonth += (double)item.TotalAmount;
            _listStatistics[i].RevenueInMonth = (decimal)totalInMonth;
            // vì gói pro có số tiền khác null 
            _listStatistics[i].ProUpgradesInMonth =
                _listMember[i].Where(_ => _.TotalAmount != null).GroupBy(m => m.UsersId).Count();
            // Thống kê tất cả use đã tạo trong 1 tháng
            _listStatistics[i].UserRegisterInMonth = _listUser[i].Count();
        }

        return _listStatistics;
    }

    public IEnumerable<UserDTO> GetProUpgradeLatest(int number)
    {
        var userMember = _memberRepository.GetAll()
                .Where(_ => _.UsersId != null)
                .OrderByDescending(p => p.CreateDay)
                .GroupBy(p => p.UsersId)
                .Select(g => g.FirstOrDefault())
                .ToList();
        var listUser = new List<UserDTO>();
        userMember.ForEach(p =>
        {
            listUser.Add(_userRepository.GetAll()
                .Where(_ => _.Id == p.UsersId || p.UsersId == null)
                .FirstOrDefault().Adapt<UserDTO>());
        });
        var users = listUser.Take(number).ToList();
        return users;
    }
}