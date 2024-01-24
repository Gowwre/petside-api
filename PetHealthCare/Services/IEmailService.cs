using PetHealthCare.Model.DTO.Request;

namespace PetHealthCare.Services;

public interface IEmailService
{
    Task SendAsync(EmailRequestDto request);
}