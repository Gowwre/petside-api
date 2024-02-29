using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Repository.Impl;

public class AppointmentRepository : RepositoryBaseImpl<Appointment>, IAppointmentRepository
{
    private readonly PetDbContext _context;

    public AppointmentRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<AppointmentResponseDTO>> GetByCriteria(Expression<Func<Appointment, bool>> expression)
    {
        try
        {
            var data = _context.Appointments.Where(expression).AsNoTracking().ProjectToType<AppointmentResponseDTO>()
                .ToListAsync();

            return data;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}