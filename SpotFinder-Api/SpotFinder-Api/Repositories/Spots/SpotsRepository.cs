using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SpotFinder_Api.Caching;
using SpotFinder_Api.Database;
using SpotFinder_Api.Models;
using SpotFinder_Api.Pagination;

namespace SpotFinder_Api.Repositories.Spots
{
    public class SpotsRepository : ISpotsRepository
    {
        private readonly PostgresDbContext _context;
        private IServiceProvider _provider;

        public SpotsRepository(PostgresDbContext context, IServiceProvider provider)
        {
            _context = context;
           _provider = provider;
        }
        public async Task CreateManyAsync(List<Spot> spots)
        {
            await _context.spots.AddRangeAsync(spots);
            await _context.SaveChangesAsync();
            var scope = _provider.CreateScope();
            var redis = scope.ServiceProvider.GetRequiredService<RedisOutputCacheStore>();
            await redis.EvictByTagAsync("spots");
        }

        public async Task<PagedResult<Spot>> GetAllAsync(Page page)
        {
            var totalCount = await _context.spots.CountAsync();
            var items = await _context.spots
                              .Skip(page.PageIndex * page.PageSize)
                              .Take(page.PageSize)
                              .ToListAsync();
            return new PagedResult<Spot> { Items = items, TotalCount = (int)totalCount };
        }

        public async Task<List<Spot>> GetSpotsByTitlesAsync(HashSet<string> titles)
        {
            return await _context.spots
                         .Where(spot => titles.Contains(spot.Title))
                         .ToListAsync();
        }
    }
}
