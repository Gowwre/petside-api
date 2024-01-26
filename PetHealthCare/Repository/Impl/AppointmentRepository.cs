using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;

namespace PetHealthCare.Repository.Impl;

public class AppointmentRepository : RepositoryBaseImpl<Appointment>, IAppointmentRepository
{
    private readonly PetDbContext _context;

    public AppointmentRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }
}