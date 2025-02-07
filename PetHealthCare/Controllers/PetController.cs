﻿using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
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
    public async Task<ActionResult<ResultResponse<PetResponseDTO>>> UpdatePetInformation(Guid id, PetRequestDTO petRequestDTO)
    {
        return Ok(await _petService.UpdatePetAsync(id, petRequestDTO));
    }

    [HttpGet("getPetInformation/{id}")]
    public async Task<ActionResult<ResultResponse<PetResponseDTO>>> GetInformationPet(Guid id)
    {
        return Ok(await _petService.GetPetAsync(id));
    }

    [HttpPost("CreatePet/{ownerId}")]
    public async Task<ActionResult<ResultResponse<PetResponseDTO>>> CreatePetInformation(Guid ownerId, [FromBody] PetRequestDTO petRequestDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _petService.CreatePetAsync(petRequestDTO, ownerId));
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<PetsDTO>>> GetAllUser([FromQuery] GetWithPaginationQueryDTO query, string? search)
    {
        return Ok(await _petService.GetPetsPagin(query, search));
    }
    [HttpDelete("pet/{id}")]
    public ActionResult DeletePet(Guid id)
    {
        return Ok(_petService.DeletePet(id));
    }
}