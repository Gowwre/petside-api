using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/statistics")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("year")]
    public async Task<IActionResult> getStatistics(int year)
    {
        return Ok(await _statisticsService.GetStatistics(year));
    }
}