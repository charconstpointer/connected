using System;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public Item Item { get; set; }
    }
}