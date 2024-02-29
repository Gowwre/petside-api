using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Model.DTO;

public class OfferProvidersDTO
{
    //public Guid ProvidersId { get; set; }

    //public Guid OfferingsId { get; set; }
    public virtual ProvidersOfferResponse? Providers { get; set; }

    public virtual OfferProvidersResponse? Offerings { get; set; }
}
