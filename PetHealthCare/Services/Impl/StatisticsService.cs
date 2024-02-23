﻿using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class StatisticsService : IStatisticsService
{
    private readonly IMemberUserRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    private readonly Dictionary<int, IList<MemberUser>> _listMember = new();
    private readonly Dictionary<int, StatisticsDTO> _listStatistics = new();
    private readonly Dictionary<int, IList<Users>> _listUser = new();

    public StatisticsService(IUserRepository userRepository, IMemberUserRepository memberRepository)
    {
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        for (var i = 1; i <= 12; i++)
        {
            _listMember.Add(i, new List<MemberUser>());
            _listStatistics.Add(i, new StatisticsDTO());
            _listUser.Add(i, new List<Users>());
        }
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
}