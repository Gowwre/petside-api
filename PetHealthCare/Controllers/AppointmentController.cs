using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/appointment")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger _logger;

    public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
    {
        _appointmentService = appointmentService;
        _logger = logger;
    }

    [HttpPost("createAppointment/{userId}")]
    public async Task<IActionResult> UpdateAppointmentInfomation(Guid userId, AppointmentRequestDTO appointmentDTO,
        [FromQuery] List<Guid> listGuidOffer)
    {
        return Ok(await _appointmentService.CreateAppointmentAsync(appointmentDTO, userId, listGuidOffer));
    }

    [HttpPut("updateAppointment/{id}")]
    public async Task<IActionResult> UpdateAppointmentInfomation(Guid id, AppointmentRequestDTO appointmentDTO)
    {
        return Ok(await _appointmentService.UpdateAppointmentAsync(id, appointmentDTO));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<IActionResult> GetInfomationPet(Guid id)
    {
        return Ok(await _appointmentService.GetAppointmentAsync(id));
    }

    [HttpGet("getAllInformation")]
    public IActionResult GetAllUser()
    {
        return Ok(_appointmentService.GetAllAppointment());
    }
}