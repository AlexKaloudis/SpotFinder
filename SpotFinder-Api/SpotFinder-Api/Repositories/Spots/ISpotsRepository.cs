using SpotFinder_Api.Models;
using SpotFinder_Api.Pagination;

namespace SpotFinder_Api.Repositories.Spots
{
    public interface ISpotsRepository
    {
        public  Task<PagedResult<Spot>> GetAllAsync(Page page);
        public Task CreateManyAsync(List<Spot> spots);
        public Task<List<Spot>> GetSpotsByTitlesAsync(HashSet<string> titles);
    }
}
