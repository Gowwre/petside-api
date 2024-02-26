using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model;

public class Appointment : Common
{
    public DateTime? BookingDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Notes { get; set; }
    public decimal? AppointmentFee { get; set; }
    public string? VisitType { get; set; }
    public string? Address { get; set; }
    //public string? PaymentMethod { get; set; }
    //public int? DurationMinutes { get; set; }
    public AppointmentStatus AppointmentStatus
    { get; set; }
    public Guid UsersId { get; set; }
    public virtual Users? Users { get; set; }
    public virtual Payment? Payment { get; set; }

    public virtual ICollection<Pets>? Pets { get; set; }
    public virtual ICollection<OfferAppointment>? OfferAppointments { get; set; }
}