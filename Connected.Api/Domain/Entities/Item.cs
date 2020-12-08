using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Item
    {
        [Key] public int Id { get; private set; }
        public string Body { get; private set; }
        public DateTime PostDate { get; private set; }
        public User Poster { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }

        public Item(string body, User poster)
        {
            Body = body;
            Comments = new List<Comment>();
            PostDate = DateTime.UtcNow;
            Poster = poster;
        }

        private Item()
        {
        }
    }
}