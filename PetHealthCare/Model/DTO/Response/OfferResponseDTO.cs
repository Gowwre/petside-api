using Mapster;

namespace PetHealthCare.Model.DTO.Response;

public class OfferResponseDTO
{
    [AdaptMember(nameof(Offerings.Id))] public Guid? OfferId { get; set; } = null;

    public string ServiceName { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    [AdaptMember(nameof(Providers))] public virtual ProviderResponseDTO? ProviderResponse { get; set; }
}