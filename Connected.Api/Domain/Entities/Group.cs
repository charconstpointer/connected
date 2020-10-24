using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connected.Api.Domain.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Tags { get; set; }
        public User Creator { get; set; }
        public IEnumerable<UserGroup> Users { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
    }
}