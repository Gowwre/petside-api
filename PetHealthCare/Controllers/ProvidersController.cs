using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
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
    public async Task<ActionResult<ResultResponse<ProviderResponseDTO>>> UpdateProviderInformation(Guid id, ProviderRequestDTO providersDTO, [FromQuery] List<Guid> listOffering)
    {
        return Ok(await _providersService.UpdateProvidersAsync(id, providersDTO, listOffering));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<ActionResult<ResultResponse<ProviderResponseDTO>>> GetInformation(Guid id)
    {
        return Ok(await _providersService.GetProvidersAsync(id));
    }
    [HttpGet("searchCategory")]
    public ActionResult<List<ProviderResponseDTO>> GetProviderByCategory(string? search)
    {
        return Ok(_providersService.GetProvidersCategory(search));
    }

    [HttpGet("getAllInformation")]
    public ActionResult<List<ProviderResponseDTO>> GetAllUser(string? name)
    {
        return Ok(_providersService.GetAllProviders(name));
    }

    [HttpPost("LoginProvider")]
    public async Task<ActionResult<ResultResponse<ProviderResponseDTO>>> LoginProvider(LoginProviderDTO loginProviderDTO)
    {
        return Ok(await _providersService.loginProvidersAsync(loginProviderDTO));
    }

    [HttpPost("CreateProvider")]
    public async Task<ActionResult<ResultResponse<ProviderResponseDTO>>> CreateOfferInformation(ProviderRequestDTO providersDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _providersService.CreateProvidersAsync(providersDTO));
    }

    [HttpDelete("provider/{id}")]
    public ActionResult DeleteProvider(Guid id)
    {
        return Ok(_providersService.DeleteProviders(id));
    }
}