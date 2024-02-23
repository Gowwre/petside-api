using Mapster;
using PetHealthCare.Model.DTO;
using PetHealthCare.Repository;

namespace PetHealthCare.Services.Impl;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;

    public MembershipService(IMembershipRepository membershipRepository)
    {
        _membershipRepository = membershipRepository;
    }

    public List<MembershipDTO> GetAllOfferings()
    {
        return _membershipRepository.GetAll().ProjectToType<MembershipDTO>().ToList();
    }
}