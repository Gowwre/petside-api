using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Repository.Impl;

public class AppointmentRepository : RepositoryBaseImpl<Appointment>, IAppointmentRepository
{
    private readonly PetDbContext _context;

    public AppointmentRepository(PetDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<AppointmentResponseDTO>> GetByCriteria(Expression<Func<Appointment, bool>> expression)
    {
        try
        {
            var appointments = await _context.Appointments
                .Include(x => x.Users)
                .Include(x => x.OfferAppointments)
                .Include(x => x.Providers)
                .Where(expression)
                .ToListAsync();

            var result = appointments.Select(x => new AppointmentResponseDTO()
            {
                AppointmentId = x.Id,
                User = x.Users.Adapt<UserDTO>(),
                OfferingsDto = x.OfferAppointments.Select(y => y.Offerings.Adapt<OfferResponseDTO>()).ToList(),
                AppointmentStatus = x.AppointmentStatus.ToString(),
                Address = x.Address,
                Notes = x.Notes,
                AppointmentFee = x.AppointmentFee,
                BookingDate = x.BookingDate.Value,
                ReturnDate = x.ReturnDate.Value,
                VisitType = x.VisitType,
                Providers = x.Providers.Adapt<ProvidersOfferResponse>(),
            }).ToList();

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}