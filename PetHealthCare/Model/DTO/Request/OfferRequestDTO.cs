namespace PetHealthCare.Model.DTO.Request;

public class OfferRequestDTO
{
    //[AdaptMember(nameof(Offerings.Id))]
    //public Guid? OfferId { get; set; } = null;
    public string ServiceName { get; set; }
    public string? Description { get; set; }

    public decimal Price { get; set; }
    //[AdaptMember(nameof(Providers))]
    //public virtual ProvidersDTO? ProvidersDto { get; set; }
}