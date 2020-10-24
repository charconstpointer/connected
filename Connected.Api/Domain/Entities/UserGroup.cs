using System;

namespace Connected.Api.Domain.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public DateTime LeaveDate { get; set; }
    }
}