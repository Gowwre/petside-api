using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/providers")]
[ApiController]
public class ProvidersController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IProvidersService _providersService;

    public ProvidersController(IProvidersService providersService, ILogger<ProvidersController> logger)
    {
        _providersService = providersService;
        _logger = logger;
    }

    [HttpPut("updateProviders/{id}")]
    public async Task<IActionResult> UpdateProviderInfomation(Guid id, ProviderRequestDTO providersDTO)
    {
        return Ok(await _providersService.UpdateProvidersAsync(id, providersDTO));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<IActionResult> GetInfomation(Guid id)
    {
        return Ok(await _providersService.GetProvidersAsync(id));
    }

    [HttpGet("getAllInformation")]
    public IActionResult GetAllUser()
    {
        return Ok(_providersService.GetAllProviders());
    }

    [HttpPost("CreateProvider")]
    public async Task<IActionResult> CreateOfferInformation(ProviderRequestDTO providersDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _providersService.CreateProvidersAsync(providersDTO));
    }
}