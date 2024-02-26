using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpotFinder_Api.Models;

namespace SpotFinder_Api.Services.Spots
{
    public class SpotsService
    {
        private readonly IMongoCollection<Spot> _spotsCollection;
        

        public SpotsService(
            IOptions<SpotFinderDatabaseSettings> spotFinderDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                spotFinderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                spotFinderDatabaseSettings.Value.DatabaseName);
            _spotsCollection = mongoDatabase.GetCollection<Spot>(
                "Spots");
        }

        public async Task<List<Spot>> GetAsync() =>
            await _spotsCollection.Find(_ => true).ToListAsync();

        public async Task<List<Spot>> SearchLocation(string location) =>
           await _spotsCollection.Find( x => x.Location == location).ToListAsync();

        public async Task<List<Spot>> GetSpotsByTitlesAsync(HashSet<string> titles)
        {
            var filter = Builders<Spot>.Filter.In(x => x.Title, titles);
            return await _spotsCollection.Find(filter).ToListAsync();
        }


        public async Task<Spot?> GetAsync(string id) =>
            await _spotsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Spot newSpot) =>
            await _spotsCollection.InsertOneAsync(newSpot);
        
        public async Task CreateManyAsync(List<Spot> spots) =>
            await _spotsCollection.InsertManyAsync(spots);

        public async Task UpdateAsync(string id, Spot updatedSpot) =>
            await _spotsCollection.ReplaceOneAsync(x => x.Id == id, updatedSpot);

        public async Task RemoveAsync(string id) =>
            await _spotsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
