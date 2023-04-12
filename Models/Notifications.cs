using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotifService.Models
{
    public class Notif
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("postid")]
        public string PostID { get; set; }

        [BsonElement("posttext")]
        public string PostText { get; set; }

        [BsonElement("userid")]
        public string UserID { get; set; }

        [BsonElement("timestamp")]
        public string Timestamp { get; set; }

        [BsonElement("commentusername")]
        public string CommentUsername { get; set; }

        [BsonElement("seen")]
        [DefaultValue(false)]
        public bool? Seen { get; set; }
    }

}