namespace SpotFinder_Api.EmailService
{
    public class MailSettings
    {
        public string ApiToken { get; set; } = string.Empty;
        public string ApiBaseUrl { get; set; } = string.Empty;
        public string EmailBoxAddress { get; set; } = string.Empty;
    }
}
