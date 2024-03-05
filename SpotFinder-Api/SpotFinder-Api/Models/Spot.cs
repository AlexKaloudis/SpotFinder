using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore;

namespace SpotFinder_Api.Models
{
    public class Spot
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Link{ get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;
        
        public string Paid { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
    }
}
