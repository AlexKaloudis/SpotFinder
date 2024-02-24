using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SpotFinder_Api.Models
{
    public class Spot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Link")]
        public string Link{ get; set; } = string.Empty;

        [BsonElement("Date")]
        public string Date { get; set; } = string.Empty;
        
        [BsonElement("Paid")]
        public string Paid { get; set; } = string.Empty;

        [BsonElement("Location")]
        public string Location { get; set; } = string.Empty;

        [BsonElement("Image")]
        public string Image { get; set; } = string.Empty;
    }
}
