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
        //try
        //{
        //    if (await _petService.UpdatePetAsync(id, petDTO))
        //    {
        //        return StatusCode(200, "Update Pet Successfull");
        //    }
        //    else
        //    {
        //        return StatusCode(400, "Update pet Fail");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    int statusCode;
        //    string errorMessage;
        //    (errorMessage, statusCode) = ex.Message switch
        //    {
        //        "PET_NOT_FOUND" => (ex.Message, 404),
        //        _ => ("Server error", 500)
        //    };
        //    _logger.LogError(statusCode, errorMessage);
        //    return StatusCode(statusCode, errorMessage);
        //}
        return Ok(await _petService.UpdatePetAsync(id, petRequestDTO));
    }

    [HttpGet("getPetInformation/{id}")]
    public async Task<IActionResult> GetInfomationPet(Guid id)
    {
        //try
        //{
        //    return StatusCode(200, _petService.GetPetAsync(id));
        //}
        //catch (Exception ex)
        //{
        //    int statusCode;
        //    string errorMessage;
        //    (errorMessage, statusCode) = ex.Message switch
        //    {
        //        "PET_NOT_FOUND" => (ex.Message, 404),
        //        _ => ("Server error", 500)
        //    };
        //    _logger.LogError(statusCode, errorMessage);
        //    return StatusCode(statusCode, errorMessage); /{AppointmentId}  , Guid AppointmentId
        //}
        return Ok(await _petService.GetPetAsync(id));
    }

    [HttpPost("CreatePet/{OwnerId}/{AppointmentId}")]
    public async Task<IActionResult> CreatePetInformation(Guid OwnerId, Guid AppointmentId,
        [FromBody] PetRequestDTO petRequestDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _petService.CreatePetAsync(petRequestDTO, OwnerId, AppointmentId));
        //try
        //{
        //    if (ModelState.IsValid)
        //    {

        //        return StatusCode(200, _petService.CreatePetAsync(petDTO, OwnerId));
        //    }
        //    else
        //    {
        //        return StatusCode(400, ModelState);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    int statusCode;
        //    string errorMessage;
        //    (errorMessage, statusCode) = ex.Message switch
        //    {
        //        "PET_ERROR" => (ex.Message, 404),
        //        _ => ("Server error", 500)
        //    };
        //    _logger.LogError(statusCode, errorMessage);
        //    return StatusCode(statusCode, errorMessage);
        //}
    }

    [HttpGet("getAllPetInformation")]
    public IActionResult GetAllUser()
    {
        return Ok(_petService.GetAllPet());
    }
}