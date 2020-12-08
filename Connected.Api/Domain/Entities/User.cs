using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connected.Api.Domain.Entities
{
    public class Guest
    {
    }

    public class User : Guest
    {
        [Key] public int Id { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<Group> CreatedGroups { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public IEnumerable<Item> Items { get; private set; }
        public IEnumerable<UserGroup> Groups { get; private set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            CreatedGroups = new List<Group>();
            Comments = new List<Comment>();
            Items = new List<Item>();
            Groups = new List<UserGroup>();
        }

        protected User()
        {
        }
    }

    public class Admin : User
    {
        public Admin(string username, string password, string email) : base(username, password, email)
        {
        }

        protected Admin()
        {
        }
    }
}