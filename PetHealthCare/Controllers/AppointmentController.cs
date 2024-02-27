using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
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
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> UpdateAppointmentInformation(Guid userId, AppointmentRequestDTO appointmentDTO,
        [FromQuery] List<Guid> listGuidOffer)
    {
        return Ok(await _appointmentService.CreateAppointmentAsync(appointmentDTO, userId, listGuidOffer));
    }

    [HttpPut("updateAppointment/{id}")]
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> UpdateAppointmentInformation(Guid id, AppointmentRequestDTO appointmentDTO)
    {
        return Ok(await _appointmentService.UpdateAppointmentAsync(id, appointmentDTO));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> GetInformationPet(Guid id)
    {
        return Ok(await _appointmentService.GetAppointmentAsync(id));
    }

    [HttpGet("getAllInformation")]
    public ActionResult<List<AppointmentRequestDTO>> GetAllUser()
    {
        return Ok(_appointmentService.GetAllAppointment());
    }
    [HttpDelete("appointment/{id}")]
    public ActionResult DeleteAppointment(Guid id)
    {
        return Ok(_appointmentService.DeleteAppointment(id));
    }
}