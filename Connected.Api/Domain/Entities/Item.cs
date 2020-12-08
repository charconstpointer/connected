using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; private set; }
        public string Url { get; private set; }
        public DateTime PostDate { get; private set; }
        public User Poster { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }

        public Item(string url)
        {
            Url = url;
        }
    }
}