using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SpotFinder_Api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        public string HashPassword { get; set; } = string.Empty;
        public string SaltPassword { get; set; } = string.Empty;

    }
}
