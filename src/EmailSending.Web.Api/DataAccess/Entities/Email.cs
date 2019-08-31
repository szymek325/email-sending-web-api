using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmailSending.Web.Api.DataAccess.Entities
{
    public class Email
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}