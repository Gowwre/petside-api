namespace PetHealthCare.Repository;

public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll();
    T GetById(Guid id);
    //void Add(T entity); // chua bi xoa
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(Guid id);
}
