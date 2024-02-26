using System.Text.Json.Serialization;

namespace PetHealthCare.Model;

public class OfferProviders
{
    public Guid ProvidersId { get; set; }
    public Guid OfferingsId { get; set; }

    [JsonIgnore] public virtual Providers? Providers { get; set; }

    [JsonIgnore] public virtual Offerings? Offerings { get; set; }
}
