using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model;

public class Payment : Common
{
    public double Amount { get; set; }
    public string? Currency { get; set; }
    public Guid PayerId { get; set; }
    public string? PaymentMethod { get; set; }
    public PaymentStatus? Status { get; set; }
    public string? Description { get; set; }

    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }
    public Guid? UsersId { get; set; }
    public virtual Users? Users { get; set; }
}