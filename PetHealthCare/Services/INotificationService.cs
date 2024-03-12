using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface INotificationService
{
    public Task<ResultResponse<NotificationDTO>> CreateNotification(NotificationRequest notificationRequest, Guid PetId, Guid UserId);
    public List<NotificationDTO> GetAllNotificationAtDay(DateTime day, Guid UserId);
    public List<NotificationDTO> GetNotificationAroundDay(DateTime day, Guid UserId);
    public ResultResponse<NotificationDTO> GetNotificationById(Guid Id);
    public List<NotificationDTO> GetAllNotificationByUser(Guid userId);
    public List<NotificationDTO> GetAllNotificationByPet(Guid petId);

}
