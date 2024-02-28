using Mapster;

namespace PetHealthCare.Model.DTO.Response;

public class OfferResponseDTO
{
    [AdaptMember(nameof(Offerings.Id))] public Guid? OfferId { get; set; } = null;

    public string ServiceName { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string Category { get; set; }
    public ICollection<OfferProvidersDTO>? OfferProviders { get; set; }
    //[AdaptMember(nameof(Providers))] public virtual ProviderResponseDTO? ProviderResponse { get; set; }
}