using System.Linq.Expressions;
using PetHealthCare.Model;
using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Repository;

public interface IAppointmentRepository : IRepositoryBase<Appointment>
{
    Task<List<AppointmentResponseDTO>> GetByCriteria(Expression<Func<Appointment, bool>> expression);
}