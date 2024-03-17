namespace SpotFinder_Api.EmailService
{
    public interface IAPIMailService
    {
        public Task<bool> SendMailAsync(MailData mailData);
    }
}