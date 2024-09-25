using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp2.Data
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // MongoDB uses a string for its IDs
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
