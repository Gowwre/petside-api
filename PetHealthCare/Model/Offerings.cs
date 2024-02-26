using PetHealthCare.Model.Abstract;

namespace PetHealthCare.Model;

public class Offerings : Common
{
    public string? ServiceName { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public virtual ICollection<OfferAppointment>? OfferAppointments { get; set; }
    public virtual ICollection<OfferProviders>? OfferProviders { get; set; }
}