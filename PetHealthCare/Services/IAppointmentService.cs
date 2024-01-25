using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Services;

public interface IAppointmentService
{
    public Task<ResultResponse<AppointmentResponseDTO>> CreateAppointmentAsync(AppointmentRequestDTO appointmentDTO, Guid userId, List<Guid> OfferId);
    public Task<ResultResponse<AppointmentResponseDTO>> GetAppointmentAsync(Guid appointmentId);
    public List<AppointmentRequestDTO> GetAllAppointment();
    public Task<ResultResponse<AppointmentResponseDTO>> UpdateAppointmentAsync(Guid appointmentId, AppointmentRequestDTO appointmentDTO);

}
