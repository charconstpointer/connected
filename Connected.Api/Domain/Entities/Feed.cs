using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Connected.Api.Domain.Entities
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public IList<Item> Items { get; set; } 
        [JsonIgnore]
        public Group Group { get; set; }

        public Feed(Group group)
        {
            Group = group;
            CreateDate = DateTime.UtcNow;
            Items = new List<Item>();
        }

        private Feed() { }
    }
}