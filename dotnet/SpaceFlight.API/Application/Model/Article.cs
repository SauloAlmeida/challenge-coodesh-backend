using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SpaceFlight.API.Application.Model
{
    public class Article 
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Featured { get; set; }
        public IEnumerable<Launches> Launches { get; set; }
        public IEnumerable<Events> Events { get; set; }
    }
}
