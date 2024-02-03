using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class RoleRepository : RepositoryBaseImpl<Role>, IRoleRepository
{
    private readonly PetDbContext _context;

    public RoleRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }

    public void AddAllRole(List<Role> role)
    {
        _context.Roles.AddRange(role);
        _context.SaveChanges();
    }
}