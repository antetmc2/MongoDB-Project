using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTest.Models
{
    public class PortalMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<BsonDocument> Articles { get; set; }

        [Display(Name = "New Comment:")]
        public string NewComment { get; set; }
        public string Chosen { get; set; }
    }
}