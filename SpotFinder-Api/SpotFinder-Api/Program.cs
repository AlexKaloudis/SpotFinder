using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using SpotFinder_Api.Caching;
using SpotFinder_Api.Database;
using SpotFinder_Api.Models;
using SpotFinder_Api.Pagination;
using SpotFinder_Api.Repositories.Spots;
using SpotFinder_Api.WebScrapingService;
using StackExchange.Redis;

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
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisConn");
//    options.InstanceName = "Spots";
//});

builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<ISpotsRepository,SpotsRepository>();
builder.Services.AddDbContext<PostgresDbContext>(x => x
.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConn")));
Console.WriteLine("postgres: " +builder.Configuration.GetConnectionString("PostgresConn"));
builder.Services.AddRedisOutputCache();
builder.Services.AddSingleton<RedisOutputCacheStore>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<WebScraper>();
builder.Services.Configure<SpotFinderDatabaseSettings>(
    builder.Configuration.GetSection("SpotFinderDatabase"));
Console.WriteLine($"pame ligo {builder.Configuration.GetSection("SpotFinderDatabase").Value}");
builder.Services.AddResponseCaching();
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
//app.UseAuthorization();
app.UseOutputCache();
//app.MapControllers();
app.MapGet("/spots", async ([AsParameters] Page page, [FromServices] ISpotsRepository repo) =>
{
    var spots = await repo.GetAllAsync(page);
    if (spots == null || !spots.Items.Any())
    {
        return Results.NotFound();
    }
    return Results.Ok(spots);
}).CacheOutput();

app.MapPost("/spots", async ([FromBody] List<Spot> spots, [FromServices] ISpotsRepository repo) =>
{
    if (spots != null)
    {
        await repo.CreateManyAsync(spots);
    }
    return Results.Ok(spots);
});

app.Run();

