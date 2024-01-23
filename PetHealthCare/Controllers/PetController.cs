using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;
[Route("api/pets")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly ILogger _logger;

    public PetController(IPetService petService, ILogger<PetController> logger)
    {
        _petService = petService;
        _logger = logger;
    }

    [HttpPost("updatePet/{id}")]
    public async Task<IActionResult> UpdatePetInfomation(Guid id, PetDTO petDTO)
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
        return Ok(_petService.UpdatePetAsync(id, petDTO));
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
        //    return StatusCode(statusCode, errorMessage);
        //}
        return Ok(_petService.GetPetAsync(id));
    }

    [HttpPost("CreatePet/{OwnerId}")]
    public async Task<IActionResult> CreatePetInformation(Guid OwnerId, PetDTO petDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(_petService.CreatePetAsync(petDTO, OwnerId));
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
