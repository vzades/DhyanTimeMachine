using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DhyanTimeMachine.Data.Entity
{
    public class Quotes
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Quote { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> Comments { get; set; } 
    }

    public class Comment
    {
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
