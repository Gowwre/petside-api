using PetHealthCare.Model.DTO.Request;

namespace PetHealthCare.Services;

public interface IEmailService
{
    Task SendAsync(EmailRequestDTO request);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}