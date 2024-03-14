using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
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
    public async Task<ActionResult<Dictionary<int, StatisticsDTO>>> GetStatistics(int year)
    {
        return Ok(await _statisticsService.GetStatistics(year));
    }
    [HttpGet("statisticsUser")]
    public async Task<IActionResult> getStatisticsUser()
    {
        return Ok(await _statisticsService.GetStaticsUser());
    }
    [HttpGet("statisticsProvider")]
    public async Task<IActionResult> getStatisticsProvider()
    {
        return Ok(await _statisticsService.GetStaticsProvider());
    }
    [HttpGet("totalMoney")]
    public IActionResult getAllMoney()
    {
        return Ok(new
        {
            TotalMoney = _statisticsService.GetStaticsMoney()
        });
    }
    [HttpGet("totalUser/{number}")]
    public IActionResult GetLatestProUpgrade(int number)
    {
        return Ok(_statisticsService.GetProUpgradeLatest(number));
    }
}