
using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;

namespace PetHealthCare.Repository.Impl;

public class RepositoryBaseImpl<T> : IRepositoryBase<T> where T : class
{
    private readonly PetDbContext _context;

    public RepositoryBaseImpl(PetDbContext context)
    {
        _context = context;
    }
    //public void Add(T entity) // chua bi xoa
    //{
    //    _context.Set<T>().Add(entity);
    //    _context.SaveChanges();
    //}
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(Guid id)
    {
        T entity = GetById(id);
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
