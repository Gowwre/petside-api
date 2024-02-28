using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPetRepository _petRepository;

    public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository, IPetRepository petRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _petRepository = petRepository;
    }

    public async Task<ResultResponse<NotificationDTO>> CreateNotification(NotificationRequest notificationRequest, Guid PetId, Guid UserId)
    {

        var result = new ResultResponse<NotificationDTO>();
        var ownerPet = await _userRepository.GetAll().Where(u => u.Id == UserId).FirstOrDefaultAsync();
        var pet = await _petRepository.GetAll().Where(u => u.Id == PetId).FirstOrDefaultAsync();
        if (ownerPet == null || pet == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = ownerPet != null ? "OWNER_NOT_FOUND" : "PET_NOT_FOUND";
            return result;
        }

        try
        {
            var notification = new Notifications();
            notificationRequest.Adapt(notification);
            notification.Users = ownerPet;
            notification.Pets = pet;

            result.Data = (await _notificationRepository.AddAsync(notification)).Adapt<NotificationDTO>();
            result.Code = 201;
            result.Success = true;
            result.Messages = "Create Notificaton Successfully";
        }
        catch (Exception ex)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = ex.Message;
        }

        return result;

    }

    public List<NotificationDTO> GetAllNotificationAtDay(DateTime day)
    {
        return _notificationRepository.GetAll().Where(_ => _.DateRemind.Day.Equals(day.Day) && _.DateRemind.Month.Equals(day.Month))
            .ProjectToType<NotificationDTO>().ToList();
    }

    public ResultResponse<NotificationDTO> GetNotificationById(Guid Id)
    {
        var result = new ResultResponse<NotificationDTO>();
        var notification = _notificationRepository.GetById(Id);
        if (notification == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "NOTIFICATION_NOT_FOUND";
        }
        else
        {
            result.Code = 200;
            result.Data = notification.Adapt<NotificationDTO>();
            result.Success = true;
            result.Messages = "NOTIFICATION_IN_DATABASE";
        }
        return result;
    }

    public List<NotificationDTO> GetAllNotificationNext7Days(DateTime day)
    {

        DateTime endDate = day.AddDays(7);

        return _notificationRepository
            .GetAll()
            .Where(notification => notification.DateRemind >= day && notification.DateRemind <= endDate)
            .ProjectToType<NotificationDTO>()
            .ToList();
    }
}
