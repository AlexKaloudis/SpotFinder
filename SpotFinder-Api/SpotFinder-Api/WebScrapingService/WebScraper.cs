//using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SpotFinder_Api.Models;
using SpotFinder_Api.Services.Spots;
using System.Text;

namespace SpotFinder_Api.WebScrapingService
{
    public class WebScraper : BackgroundService
    {
        private readonly ILogger<WebScraper> _logger;
        private IWebDriver _driver;

        private IServiceProvider _services;

        public WebScraper(IServiceProvider services, ILogger<WebScraper> logger)
        {
            _logger = logger;
            _services = services;
            ChromeOptions options = new ChromeOptions();

            // Disable cookies
            options.AddUserProfilePreference("profile.default_content_setting_values.cookies", 1);
            _driver = new ChromeDriver(options);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "WebScrapping Background service is working");

            await DoScraping(stoppingToken);
        }

        private async Task DoScraping(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");
            //scraping logic
            //1. retrieve the list of events/spots using a WebScraping library

            // visiting the target web page 
            _driver.Navigate().GoToUrl("https://www.eventbrite.com/d/greece--%CE%B8%CE%B5%CF%83%CF%83%CE%B1%CE%BB%CE%BF%CE%BD%CE%B9%CE%BA%CE%B7/tech/"); 
            List<IWebElement> elements = _driver.FindElements(By.CssSelector("li.search-main-content__events-list-item.search-main-content__events-list-item__mobile")).ToList();
            List<Spot> spotData = new List<Spot>();

            //2. throw them into a list or concurrentbag
            foreach (var element in elements)
            {
                Spot spot = new Spot
                {
                    Title = ToUtf8(element.FindElement(By.CssSelector("h2.Typography_root__487rx.Typography_align-match-parent__487rx.Typography_root__487rx.Typography_body-lg__487rx.event-card__clamp-line--two"))
                    .GetAttribute("innerText")),
                    Location = ToUtf8(element.FindElement(By.CssSelector("a.event-card-link")).GetAttribute("data-event-location")),
                    Date = ToUtf8(element.FindElement(By.CssSelector("p.Typography_root__487rx.Typography_align-match-parent__487rx")).Text),
                    Paid = element.FindElement(By.CssSelector("a.event-card-link")).GetAttribute("data-event-paid-status"),
                    Link = element.FindElement(By.CssSelector("a.event-card-link")).GetAttribute("href"),
                    Image = element.FindElement(By.CssSelector("img.event-card-image")).GetAttribute("src")
                };

                spotData.Add(spot);
            }

            //3. store them into database in bulk
            if (spotData.Any()) await StoreScrapingData(spotData);
        }

        private async Task StoreScrapingData(List<Spot> spots)
        {
            _logger.LogInformation(
                "Storage of scraping data is working");

            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<SpotsService>();//here the webscraping data will be stored to the database
                List<Spot> storedSpots = await scopedProcessingService.GetAsync();
                List<Spot> newItems = spots.Except(storedSpots, new SpotTitleComparer()).ToList();

                if(newItems.Any()) await scopedProcessingService.CreateManyAsync(newItems);
            }
        }

        public static string ToUtf8(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            string utf8String = Encoding.UTF8.GetString(bytes);
            return utf8String;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
