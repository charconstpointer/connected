using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime PostDate { get; set; }
        public User Poster { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}