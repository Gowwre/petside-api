using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("{email}/forgot_password")]
    public async Task<ActionResult<string>> ForGotPassword(string email)
    {
        return Ok(await _userService.ForgotPassword(email));
    }

    [HttpPost("changePassoword")]
    public ActionResult<ResultResponse<UserDTO>> ChangePassword(ChangePassowordDTO passowordDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(_userService.ChangePassword(passowordDTO));
    }

    [HttpPost("{userId}/pro-upgrade/{membership}")]
    public ActionResult<ResultResponse<UserDTO>> UpgradeAccount(Guid userId, Guid membership)
    {
        return Ok(_userService.UpgradeAccountUser(userId, membership));
    }
}