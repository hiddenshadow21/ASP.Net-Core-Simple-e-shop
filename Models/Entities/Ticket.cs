using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MVCApp.Models.Entities
{
    public class Ticket
    {
        [BsonId]
        public string Id { get; set; }
        public List<Item> Items { get; set; }
        public ApplicationUser Buyer { get; set; }
    }
}
