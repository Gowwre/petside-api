using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IStatisticsService
{
    public Task<Dictionary<int, StatisticsDTO>> GetStatistics(int year);
    public Task<UserStaticsResponse> GetStaticsUser();
    public Task<ProviderStaticResponse> GetStaticsProvider();
    public double GetStaticsMoney();
    public IEnumerable<UserDTO> GetProUpgradeLatest(int number);
}