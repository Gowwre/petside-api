using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("/users/{userId}/pets/{petId}/notifications")]
    public async Task<ActionResult<ResultResponse<NotificationDTO>>> UpdateAppointmentInformation(Guid userId, NotificationRequest notificationRequest,
       Guid petId)
    {
        return Ok(await _notificationService.CreateNotification(notificationRequest, petId, userId));
    }

    [HttpGet("getNotification/{id}")]
    public ActionResult<ResultResponse<NotificationDTO>> GetNotificationById(Guid id)
    {
        return Ok(_notificationService.GetNotificationById(id));
    }

    [HttpGet("getAllAtDay/{date}")]
    public ActionResult<ResultResponse<NotificationDTO>> GetAllNotificationAtDay(DateTime date, Guid UserId)
    {
        return Ok(_notificationService.GetAllNotificationAtDay(date, UserId));
    }
    [HttpGet("getAllAtSevenDay/{date}")]
    public ActionResult<ResultResponse<NotificationDTO>> GetAllNotificationAtSevenDay(DateTime date, Guid UserId)
    {
        return Ok(_notificationService.GetAllNotificationNext7Days(date, UserId));
    }
    [HttpGet("user/{UserId}")]
    public ActionResult<ResultResponse<NotificationDTO>> GetNotificationByUserId(Guid UserId)
    {
        return Ok(_notificationService.GetAllNotificationByUser(UserId));
    }

    [HttpGet("pet/{PetId}")]
    public ActionResult<ResultResponse<NotificationDTO>> GetNotificationByPetId(Guid PetId)
    {
        return Ok(_notificationService.GetAllNotificationByPet(PetId));
    }
}
