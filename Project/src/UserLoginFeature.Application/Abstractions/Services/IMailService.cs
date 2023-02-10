namespace UserLoginFeature.Application.Abstractions.Services
{
    public interface IMailService
    {
        void SendMail(string email, string subject, string htmlMessage);
    }
}
