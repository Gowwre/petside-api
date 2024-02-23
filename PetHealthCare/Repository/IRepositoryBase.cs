using System.Linq.Expressions;
using PetHealthCare.Utils;

namespace PetHealthCare.Repository;

public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll();

    T GetById(Guid id);

    Task<PaginatedList<TDTO>> FindPaginAsync<TDTO>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDTO : class;

    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(Guid id);
}