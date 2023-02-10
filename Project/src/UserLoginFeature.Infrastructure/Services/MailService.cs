using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new()
            {
                IsBodyHtml = true
            };
            mail.To.Add(email);
            mail.From = new MailAddress(_configuration["Mail:Email"]!, "Digitus App", Encoding.UTF8);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            SmtpClient smp = new("smtp.gmail.com");
            smp.Credentials = new NetworkCredential(_configuration["Mail:Email"], _configuration["Mail:AuthKey"]);
            smp.Port = 587;
            smp.EnableSsl = true;
            smp.Send(mail);
        }
    }
}
