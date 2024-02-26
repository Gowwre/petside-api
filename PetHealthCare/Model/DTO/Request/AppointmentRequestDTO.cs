using PetHealthCare.Model.Enums;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model.DTO.Request;

public class AppointmentRequestDTO
{
    //[AdaptMember(nameof(Appointment.Id))]
    //public Guid? AppointmentId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? Notes { get; set; }
    public string? VisitType { get; set; }
    public string? Address { get; set; }
    public decimal? AppointmentFee { get; set; }

    //public int? DurationMinutes { get; set; }
    [JsonIgnore] public AppointmentStatus? AppointmentStatus { get; set; }
}