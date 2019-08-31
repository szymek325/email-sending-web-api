using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmailSending.Web.Api.DataAccess.Entities
{
    public class Email
    {
        public Email(Guid id, IList<string> to, IList<string> cc, IList<string> bcc, string subject, string body)
        {
            Id = id.ToString();
            To = to;
            CC = cc;
            BCC = bcc;
            Subject = subject;
            Body = body;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public IList<string> To { get; set; }
        public IList<string> CC { get; set; }
        public IList<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}