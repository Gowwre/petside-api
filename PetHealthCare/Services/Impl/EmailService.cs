using System.Net.Mail;
using PetHealthCare.Model.DTO.Request;

namespace PetHealthCare.Services.Impl;

public class EmailService : IEmailService
{
    public async Task SendAsync(EmailRequestDTO request)
    {
        var emailClient = new SmtpClient("localhost");
        var message = new MailMessage
        {
            From = new MailAddress(request.From),
            Subject = request.Subject,
            Body = request.Body
        };
        message.To.Add(new MailAddress(request.To));
        await emailClient.SendMailAsync(message);
    }
}