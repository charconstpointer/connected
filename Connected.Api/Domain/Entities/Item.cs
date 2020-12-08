using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Connected.Api.Domain.Entities
{
    public class Item
    {
        [Key] public int Id { get; private set; }
        public string Body { get; private set; }
        public DateTime PostDate { get; private set; }
        public User Poster { get; private set; }
        [JsonIgnore]
        public Group Group { get; set; }
        public IList<Comment> Comments { get; private set; }

        public Item(string body, User poster, Group group)
        {
            Body = body;
            Comments = new List<Comment>();
            PostDate = DateTime.UtcNow;
            Poster = poster;
            Group = group;
        }

        private Item()
        {
        }

        public void AddComment(Comment comment) => Comments.Add(comment);
    }
}