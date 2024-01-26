using System.Text.Json.Serialization;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Request;

public class AppointmentRequestDTO
{
    //[AdaptMember(nameof(Appointment.Id))]
    //public Guid? AppointmentId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? Notes { get; set; }

    public decimal? AppointmentFee { get; set; }

    //public int? DurationMinutes { get; set; }
    [JsonIgnore] public AppointmentStatus? AppointmentStatus { get; set; }
}