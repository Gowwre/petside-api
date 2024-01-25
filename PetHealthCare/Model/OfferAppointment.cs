namespace PetHealthCare.Model;

public class OfferAppointment
{
    public Guid AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }
    public Guid OfferingsId { get; set; }
    public virtual Offerings? Offerings { get; set; }
}
