using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class MemberUserRepository : RepositoryBaseImpl<MemberUser>, IMemberUserRepository
{
    private readonly PetDbContext _context;

    public MemberUserRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}