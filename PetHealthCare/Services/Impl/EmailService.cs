using PetHealthCare.Model.DTO.Request;
using System.Net;
using System.Net.Mail;

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
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        SmtpClient client = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("haoluonghuynh2001@gmail.com", "bddg jwrd icyx qphb")
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("haoluonghuynh2001@gmail.com"),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}