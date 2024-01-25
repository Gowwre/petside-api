using PetHealthCare.Model.Abstract;

namespace PetHealthCare.Model;

public class Providers : Common
{
    public string? ProviderName { get; set; }
    public string? ContactInformation { get; set; }
    public string? ServiceType { get; set; }
    public string? Availability { get; set; }
    public double? Rating { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public virtual ICollection<Offerings>? Offerings { get; set; }

}
