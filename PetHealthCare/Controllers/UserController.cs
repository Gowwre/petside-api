using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            if (ModelState.IsValid)
            {
                return StatusCode(200, await _userService.LoginAsync(loginDTO));
            }
            else
            {
                return StatusCode(400, ModelState);
            }
        }
        catch (Exception ex)
        {
            int statusCode;
            string errorMessage;
            (errorMessage, statusCode) = ex.Message switch
            {
                "ACCOUNT_NOT_FOUND" => (ex.Message, 404),
                "INVALID PASSWORD" => (ex.Message, 401),
                _ => ("Server error", 500)
            };
            _logger.LogError(statusCode, errorMessage);
            return StatusCode(statusCode, errorMessage);
        }
    }

    [HttpPost("register-member")]
    public async Task<IActionResult> CreateUserAccount(UserRegistrationDto userRegistrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _userService.CreateUserAsync(userRegistrationDto));
    }

    [HttpPut("updateInformation/{id}")]
    public async Task<IActionResult> UpdateUserAccount(Guid id, UserUpdateDTO userUpdateDTO)
    {
        return Ok(_userService.UpdateUserAsync(id, userUpdateDTO));
        //try
        //{
        //    if (await _userService.UpdateUserAsync(id, userUpdateDTO))
        //    {
        //        return StatusCode(200, "Update user Successfull");
        //    }
        //    else
        //    {
        //        return StatusCode(400, "Update user Fail");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    int statusCode;
        //    string errorMessage;
        //    (errorMessage, statusCode) = ex.Message switch
        //    {
        //        "USER_NOT_FOUNF" => (ex.Message, 404),
        //        _ => ("Server error", 500)
        //    };
        //    _logger.LogError(statusCode, errorMessage);
        //    return StatusCode(statusCode, errorMessage);
        //}
    }

    //[HttpGet("getAllUser")]
    //public async Task<IActionResult> GetAllUser(GetWithPaginationQueryDTO getWithPaginationQueryDTO)
    //{
    //    PaginatedList<Users> users = await _userService.GetUserPagin(getWithPaginationQueryDTO);
    //    if (users == null)
    //    {
    //        return StatusCode(200, users);
    //    }
    //    else
    //    {
    //        return StatusCode(400, "List user Is Empty");
    //    }

    //}

    [HttpGet("getInformation/{id}")]
    public IActionResult GetInfomationUser(Guid id)
    {
        //try
        //{
        //    return StatusCode(200, _userService.GetUserAsync(id));
        //}
        //catch (Exception ex)
        //{
        //    int statusCode;
        //    string errorMessage;
        //    (errorMessage, statusCode) = ex.Message switch
        //    {
        //        "USER_NOT_FOUND" => (ex.Message, 404),
        //        _ => ("Server error", 500)
        //    };
        //    _logger.LogError(statusCode, errorMessage);
        //    return StatusCode(statusCode, errorMessage);
        //}
        return Ok(_userService.GetUserAsync(id));
    }

    [HttpGet("getAllInformation")]
    public IActionResult GetAllUser()
    {
        return Ok(_userService.GetAllUser());
    }
}