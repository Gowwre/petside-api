using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class UserRepository : RepositoryBaseImpl<Users>, IUserRepository
{
    private readonly PetDbContext _context;

    public UserRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}