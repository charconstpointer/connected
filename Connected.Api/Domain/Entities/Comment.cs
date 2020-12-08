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
        public Post Post { get; private set; }

        public Comment(string content, User author, Post post = null)
        {
            CreateDate = DateTime.UtcNow;
            Author = author;
            Content = content;
            Post = post;
        }

        private Comment()
        {
        }
    }
}