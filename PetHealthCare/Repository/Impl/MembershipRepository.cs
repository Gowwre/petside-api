using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class MembershipRepository : RepositoryBaseImpl<Membership>, IMembershipRepository
{
    private readonly PetDbContext _context;

    public MembershipRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }

    public void AddAllMembership(List<Membership> memberships)
    {
        _context.Memberships.AddRange(memberships);
        _context.SaveChanges();
    }
}