using Mapster;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Response;

public class AppointmentResponseDTO
{
    [AdaptMember(nameof(Appointment.Id))] public Guid? AppointmentId { get; set; }

    public DateTime BookingDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? Notes { get; set; }
    public string? VisitType { get; set; }
    public string? Address { get; set; }
    public decimal? AppointmentFee { get; set; }
    public string? AppointmentStatus { get; set; }
    public UserDTO User  { get; set; }
    public AppointmentStatus Status { get; set; }

    [AdaptMember(nameof(Offerings))] public virtual ICollection<OfferResponseDTO>? OfferingsDto { get; set; }
    public virtual ProvidersOfferResponse? Providers { get; set; }
}