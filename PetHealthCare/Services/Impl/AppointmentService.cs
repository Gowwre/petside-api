using Mapster;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;
using PetHealthCare.Repository;
using PetHealthCare.Repository.Impl;

namespace PetHealthCare.Services.Impl;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IOfferingsRepository _offeringsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProvidersRepository _providersRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository,
        IOfferingsRepository offeringsRepository,
        IUserRepository userRepository,
        IProvidersRepository providersRepository)
    {
        _appointmentRepository = appointmentRepository;
        _offeringsRepository = offeringsRepository;
        _userRepository = userRepository;
        _providersRepository = providersRepository;
    }

    public async Task<ResultResponse<AppointmentResponseDTO>> CreateAppointmentAsync(
        AppointmentRequestDTO appointmentDTO, Guid userId, List<Guid> OfferId, Guid providerId)
    {
        var result = new ResultResponse<AppointmentResponseDTO>();
        try
        {
            var user = _userRepository.GetById(userId);
            var appointment = new Appointment();

            appointmentDTO.AppointmentStatus = AppointmentStatus.PENDING_CONFIRMATION;
            appointmentDTO.Adapt(appointment);
            appointment.Users = user;
            appointment.OfferAppointments = new List<OfferAppointment>();
            OfferId?.ForEach(x =>
            {
                appointment.OfferAppointments?.Add(new OfferAppointment
                    { Offerings = _offeringsRepository.GetById(x) });
            });
            appointment.Providers = _providersRepository.GetById(providerId);
            var appointmentDb = await _appointmentRepository.AddAsync(appointment);
            var appointmentResponseObject = appointmentDb.Adapt<AppointmentResponseDTO>();

            appointmentResponseObject.User = user.Adapt<UserDTO>();
            appointmentResponseObject.OfferingsDto = new List<OfferResponseDTO>();
            appointmentDb.OfferAppointments?.ToList().ForEach(offer =>
            {
                appointmentResponseObject.OfferingsDto?.Add(offer.Offerings.Adapt<OfferResponseDTO>());
            });

            result.Data = appointmentResponseObject;
            result.Success = true;
            result.Messages = "Create Appointment Successfully";
            result.Code = 201;
        }
        catch (Exception ex)
        {
            result.Messages = ex.Message;
        }

        return result;
    }

    public bool DeleteAppointment(Guid appointmentId)
    {
        try
        {
            _appointmentRepository.Delete(appointmentId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<List<AppointmentResponseDTO>> GetByUser(Guid userId)
    {
        try
        {
            var result = _appointmentRepository.GetByCriteria(x => x.Users.Id == userId);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<AppointmentResponseDTO>> GetByProvider(Guid providerId)
    {
        try
        {
            var result = _appointmentRepository.GetByCriteria(x => x.Providers.Id == providerId);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<AppointmentRequestDTO> GetAllAppointment()
    {
        return _appointmentRepository.GetAll().ProjectToType<AppointmentRequestDTO>().ToList();
    }

    public async Task<ResultResponse<AppointmentResponseDTO>> GetAppointmentAsync(Guid appointmentId)
    {
        var result = new ResultResponse<AppointmentResponseDTO>();
        var appointmentDb = _appointmentRepository.GetById(appointmentId);
        if (appointmentDb != null)
        {
            var appointmentResponseObject = appointmentDb.Adapt<AppointmentResponseDTO>();
            appointmentResponseObject.OfferingsDto = new List<OfferResponseDTO>();
            appointmentDb.OfferAppointments?.ToList().ForEach(offer =>
            {
                appointmentResponseObject.OfferingsDto?.Add(offer.Offerings.Adapt<OfferResponseDTO>());
            });
            result.Code = 200;
            result.Data = appointmentResponseObject;
            result.Success = true;
            result.Messages = "FIND_APPOINTMENT_IN_DATABASE";
        }
        else
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "APPOINTMENT_NOT_FOUND";
        }

        return result;
    }

    public async Task<ResultResponse<AppointmentResponseDTO>> UpdateAppointmentAsync(Guid appointmentId,
        AppointmentRequestDTO appointmentDTO)
    {
        var result = new ResultResponse<AppointmentResponseDTO>();
        var appointmentDb = _appointmentRepository.GetById(appointmentId);
        if (appointmentDb == null)
        {
            result.Code = 300;
            result.Success = false;
            result.Messages = "APPOINTMENT_NOT_FOUND";
            return result;
        }

        appointmentDTO.AppointmentStatus = appointmentDb.AppointmentStatus; // giu nguyen status
        _appointmentRepository.Update(appointmentDTO.Adapt(appointmentDb));
        var appointmentResponseObject = appointmentDb.Adapt<AppointmentResponseDTO>();
        appointmentResponseObject.OfferingsDto = new List<OfferResponseDTO>();
        appointmentDb.OfferAppointments?.ToList().ForEach(offer =>
        {
            appointmentResponseObject.OfferingsDto?.Add(offer.Offerings.Adapt<OfferResponseDTO>());
        });

        result.Code = 200;
        result.Success = true;
        result.Data = appointmentResponseObject;
        result.Messages = "UPDATE_APPOINTMENT_SUCCESSFULLY";
        return result;
    }
}