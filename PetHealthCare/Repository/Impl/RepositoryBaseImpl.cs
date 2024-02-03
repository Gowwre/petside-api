using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Utils;
using System.Linq.Expressions;

namespace PetHealthCare.Repository.Impl;

public class RepositoryBaseImpl<T> : IRepositoryBase<T> where T : class
{
    private readonly PetDbContext _context;

    public RepositoryBaseImpl(PetDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<PaginatedList<TDTO>> FindPaginAsync<TDTO>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDTO : class
    {
        IQueryable<T> query = _context.Set<T>();

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ProjectToType<TDTO>().PaginatedListAsync(pageIndex, pageSize, cancellationToken);
    }

    public void Delete(Guid id)
    {
        var entity = GetById(id);
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public T GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
}