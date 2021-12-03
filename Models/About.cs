using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCV.Models
{
    public class About
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }
        public string Text1 { get; set; }
       
        public string Text3 { get; set; }
        public string subTitle { get; set; }
        public string Birtday { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Degree { get; set; }
        public string Email { get; set; }
        public string OpenToWork { get; set; }
       

    }
}
