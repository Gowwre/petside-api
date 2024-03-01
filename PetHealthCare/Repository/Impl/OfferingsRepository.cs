using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Repository.Impl;

public class OfferingsRepository : RepositoryBaseImpl<Offerings>, IOfferingsRepository
{
    private readonly PetDbContext _context;

    public OfferingsRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<OfferResponseDTO>> GetByCriteria(Expression<Func<Offerings, bool>> predicate)
    {
        var query = _context.Offerings.AsQueryable();
        
        return query.Where(predicate).ProjectToType<OfferResponseDTO>().ToListAsync();
    }
}