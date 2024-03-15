using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;
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
        [FromQuery] List<Guid> listGuidOffer, Guid providerId)
    {
        return Ok(await _appointmentService.CreateAppointmentAsync(appointmentDTO, userId, listGuidOffer, providerId));
    }

    [HttpPut("updateAppointment/{id}")]
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> UpdateAppointmentInformation(Guid id, AppointmentRequestDTO appointmentDTO)
    {
        return Ok(await _appointmentService.UpdateAppointmentAsync(id, appointmentDTO));
    }

    [HttpPatch("completeAppointment/{id}")]
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> CompleteAppointment(Guid id)
    {
        return Ok(_appointmentService.CompleteAppointment(id));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<ActionResult<ResultResponse<AppointmentResponseDTO>>> GetInformationPet(Guid id)
    {
        return Ok(await _appointmentService.GetAppointmentAsync(id));
    }

    [HttpGet("getAllInformation")]
    public ActionResult<List<AppointmentResponseDTO>> GetAllUser([FromQuery()] string? status)
    {
        return Ok(_appointmentService.GetAllAppointment(status));
    }

    [HttpGet("getByUser/{userId}")]
    public async Task<ActionResult<List<AppointmentResponseDTO>>> GetByUser(Guid userId)
    {
        var data = await _appointmentService.GetByUser(userId);
        return Ok(data);
    }

    [HttpGet("getByProvider/{providerId}")]
    public async Task<ActionResult<List<AppointmentResponseDTO>>> GetByProvider(Guid providerId)
    {
        var data = await _appointmentService.GetByProvider(providerId);
        return Ok(data);
    }
    [HttpDelete("appointment/{id}")]
    public ActionResult DeleteAppointment(Guid id)
    {
        return Ok(_appointmentService.DeleteAppointment(id));
    }
}