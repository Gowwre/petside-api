using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/membership")]
[ApiController]
public class MembershipController : ControllerBase
{
    private readonly IMembershipService _membershipService;

    public MembershipController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    [HttpGet]
    public IActionResult getListMember()
    {
        return Ok(_membershipService.GetAllOfferings());
    }
}