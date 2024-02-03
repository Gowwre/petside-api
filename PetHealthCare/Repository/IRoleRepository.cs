using PetHealthCare.Model;

namespace PetHealthCare.Repository;

public interface IRoleRepository : IRepositoryBase<Role>
{
    public void AddAllRole(List<Role> role);
}