using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotFinder_Api.EmailService
{
    public class APIMailService : IAPIMailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public APIMailService( IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("MailTrapApiClient");
            _configuration = configuration;
        }

        public async Task<bool> SendMailAsync(MailData mailData)
        {
            var mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();
            ArgumentNullException.ThrowIfNull(mailSettings);
            var apiEmail = new
            {
                from = new { email = mailData.EmailAddress, name = mailData.Name },
                to = new [] { new { email = mailSettings.EmailBoxAddress } },
                subject = mailData.Subject,
                Text = mailData.Body,
                category = "test"
            };

            var httpResponse = await _httpClient.PostAsJsonAsync(mailSettings.ApiBaseUrl, apiEmail);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);

            if (response != null && response.TryGetValue("success", out object? success) && success is bool boolSuccess && boolSuccess)
            {
                return true;
            }

            return false;
        }
    }
}
