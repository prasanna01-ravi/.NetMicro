using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        [BsonElement("Name")]
        public String? Name { get; set; }
        public String? Category { get; set; }
        public String? Summary { get; set; }
        public String? Description { get; set; }
        public String? ImageFile { get; set; }
        public double Price { get; set; }
    }
}
