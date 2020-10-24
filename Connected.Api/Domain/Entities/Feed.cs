using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connected.Api.Domain.Entities
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public Group Group { get; set; }
    }
}