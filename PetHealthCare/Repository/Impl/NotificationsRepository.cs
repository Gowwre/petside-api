using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class NotificationsRepository : RepositoryBaseImpl<Notifications>, INotificationRepository
{
    private readonly PetDbContext _context;

    public NotificationsRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}
