using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class ProvidersRepository : RepositoryBaseImpl<Providers>, IProvidersRepository
{

    private readonly PetDbContext _context;

    public ProvidersRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}
