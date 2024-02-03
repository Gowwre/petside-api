using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Users>> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            if (ModelState.IsValid)
                return StatusCode(200, await _userService.LoginAsync(loginDTO));
            return StatusCode(400, ModelState);
        }
        catch (Exception ex)
        {
            int statusCode;
            string errorMessage;
            (errorMessage, statusCode) = ex.Message switch
            {
                "ACCOUNT_NOT_FOUND" => (ex.Message, 404),
                "INVALID_PASSWORD" => (ex.Message, 401),
                _ => ("Server error", 500)
            };
            _logger.LogError(statusCode, errorMessage);
            return StatusCode(statusCode, errorMessage);
        }
    }

    [HttpPost("register-member")]
    public async Task<IActionResult> CreateUserAccount(UserRegistrationDto userRegistrationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(await _userService.CreateUserAsync(userRegistrationDto));
    }

    [HttpPut("updateInformation/{id}")]
    public IActionResult UpdateUserAccount(Guid id, UserUpdateDTO userUpdateDTO)
    {
        return Ok(_userService.UpdateUserAsync(id, userUpdateDTO));
    }

    [HttpGet("getInformation/{id}")]
    public IActionResult GetInfomationUser(Guid id)
    {
        return Ok(_userService.GetUserAsync(id));
    }

    //[HttpGet("getAllInformation")]
    //public IActionResult GetAllUser()
    //{
    //    return Ok(_userService.GetAllUser());
    //}
    [HttpGet("getAllInformation")]
    public async Task<IActionResult> GetAllUserWithPagin([FromQuery] GetWithPaginationQueryDTO query)
    {
        return Ok(await _userService.GetUsersPagin(query));
    }
}