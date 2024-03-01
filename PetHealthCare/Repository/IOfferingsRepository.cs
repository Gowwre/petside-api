using System.Linq.Expressions;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Repository;

public interface IOfferingsRepository : IRepositoryBase<Offerings>
{
    Task<List<OfferResponseDTO>> GetByCriteria(Expression<Func<Offerings, bool>> predicate);
}