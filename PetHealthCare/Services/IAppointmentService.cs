using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Services;

public interface IAppointmentService
{
    public Task<ResultResponse<AppointmentResponseDTO>> CreateAppointmentAsync(AppointmentRequestDTO appointmentDTO,
        Guid userId, List<Guid> OfferId, Guid providerId);

    public Task<ResultResponse<AppointmentResponseDTO>> GetAppointmentAsync(Guid appointmentId);
    public List<AppointmentResponseDTO> GetAllAppointment(string? status);

    public Task<ResultResponse<AppointmentResponseDTO>> UpdateAppointmentAsync(Guid appointmentId,
        AppointmentRequestDTO appointmentDTO);
    public bool DeleteAppointment(Guid appointmentId);
    Task<List<AppointmentResponseDTO>> GetByUser(Guid userId);
    Task<List<AppointmentResponseDTO>> GetByProvider(Guid providerId);
    Task<ResultResponse<AppointmentResponseDTO>> CompleteAppointment(Guid appointmentId);
}