using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Guest
    {
        
    }

    public class User : Guest
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<Group> CreatedGroups { get; set; }
        public IEnumerable<UserGroup> Groups { get; set; }
    }

    public class Admin : User
    {
        
    }
}