using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
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
    public async Task<IActionResult> ForGotPassword(string email)
    {
        return Ok(await _userService.ForgotPassword(email));
    }
    [HttpPost("changePassoword")]
    public IActionResult ChangePassword(ChangePassowordDTO passowordDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(_userService.ChangePassword(passowordDTO));
    }
    [HttpPost("{userId}/pro-upgrade/{membership}")]
    public IActionResult UpgradeAccount(Guid userId, Guid membership)
    {
        return Ok(_userService.UpgradeAccountUser(userId, membership));
    }
}
