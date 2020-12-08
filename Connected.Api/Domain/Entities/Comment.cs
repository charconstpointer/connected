using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Connected.Api.Domain.Entities
{
    public class Comment
    {
        [Key] public int Id { get; private set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; private set; }
        public User Author { get; private set; }
        [JsonIgnore]
        public Item Item { get; private set; }

        public Comment(string content, User author, Item item = null)
        {
            CreateDate = DateTime.UtcNow;
            Author = author;
            Content = content;
            Item = item;
        }

        private Comment()
        {
        }
    }
}