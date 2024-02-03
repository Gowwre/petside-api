using PetHealthCare.Model.DTO;

namespace PetHealthCare.Services;

public interface IMembershipService
{
    public List<MembershipDTO> GetAllOfferings();
}
