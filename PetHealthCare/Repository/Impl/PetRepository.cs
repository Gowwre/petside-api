using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class PetRepository : RepositoryBaseImpl<Pets>, IPetRepository
{
    private readonly PetDbContext _context;

    public PetRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}