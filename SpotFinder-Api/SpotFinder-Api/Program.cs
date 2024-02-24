using SpotFinder_Api.Models;
using SpotFinder_Api.Services.Spots;
using SpotFinder_Api.WebScrapingService;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5173");
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<SpotsService>();
builder.Services.AddHostedService<WebScraper>();
builder.Services.Configure<SpotFinderDatabaseSettings>(
    builder.Configuration.GetSection("SpotFinderDatabase"));
Console.WriteLine($"pame ligo {builder.Configuration.GetSection("SpotFinderDatabase").Value}");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
