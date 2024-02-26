using SpotFinder_Api.Models;
using SpotFinder_Api.Pagination;

namespace SpotFinder_Api.Services.Spots
{
    public interface ISpotsService
    {
        public Task<PagedResult<Spot>> GetAsync(Page page);

        public Task<List<Spot>> SearchLocation(string location);
        public Task<List<Spot>> GetSpotsByTitlesAsync(HashSet<string> titles);

        public Task CreateAsync(Spot newSpot);

        public Task CreateManyAsync(List<Spot> spots);

        public Task UpdateAsync(string id, Spot updatedSpot);

        public Task RemoveAsync(string id);

    }
}