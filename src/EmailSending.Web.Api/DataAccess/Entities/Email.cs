using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EmailSending.Web.Api.DataAccess.Entities
{
    public class Email
    {
        public Email(string id, IList<string> to, string from, IList<string> cc, IList<string> bcc, string subject,
            string body)
        {
            Id = id;
            To = to ?? new List<string>();
            From = from;
            CC = cc ?? new List<string>();
            BCC = bcc ?? new List<string>();
            Subject = subject;
            Body = body;
        }


        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public IList<string> To { get; set; }
        public string From { get; set; }
        public IList<string> CC { get; set; }
        public IList<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Success { get; set; }
    }
}