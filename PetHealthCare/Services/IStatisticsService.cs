using PetHealthCare.Model.DTO;

namespace PetHealthCare.Services;

public interface IStatisticsService
{
    public Task<Dictionary<int, StatisticsDTO>> GetStatistics(int year);
}
