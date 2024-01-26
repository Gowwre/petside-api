using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class OfferingsRepository : RepositoryBaseImpl<Offerings>, IOfferingsRepository
{
    private readonly PetDbContext _context;

    public OfferingsRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}