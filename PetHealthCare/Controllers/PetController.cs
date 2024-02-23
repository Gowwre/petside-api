using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/pets")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IPetService _petService;

    public PetController(IPetService petService, ILogger<PetController> logger)
    {
        _petService = petService;
        _logger = logger;
    }

    [HttpPut("updatePet/{id}")]
    public async Task<IActionResult> UpdatePetInfomation(Guid id, PetRequestDTO petRequestDTO)
    {
        return Ok(await _petService.UpdatePetAsync(id, petRequestDTO));
    }

    [HttpGet("getPetInformation/{id}")]
    public async Task<IActionResult> GetInfomationPet(Guid id)
    {
        return Ok(await _petService.GetPetAsync(id));
    }

    [HttpPost("CreatePet/{OwnerId}")]
    public async Task<IActionResult> CreatePetInformation(Guid OwnerId, [FromBody] PetRequestDTO petRequestDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _petService.CreatePetAsync(petRequestDTO, OwnerId));
    }

    [HttpGet("{search}")]
    public async Task<IActionResult> GetAllUser([FromQuery] GetWithPaginationQueryDTO query, string? search)
    {
        return Ok(await _petService.GetPetsPagin(query, search));
    }
}