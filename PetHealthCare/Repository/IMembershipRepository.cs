using PetHealthCare.Model;

namespace PetHealthCare.Repository;

public interface IMembershipRepository : IRepositoryBase<Membership>
{
    public void AddAllMembership(List<Membership> memberships);
}
