using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public IList<Group> CreatedGroups { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public IEnumerable<Post> Items { get; private set; }
        public IList<UserGroup> Groups { get; private set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            CreatedGroups = new List<Group>();
            Comments = new List<Comment>();
            Items = new List<Post>();
            Groups = new List<UserGroup>();
        }

        protected User()
        {
        }

        public void AddGroup(Group group)
        {
            var userGroup = new UserGroup {Group = @group, User = this};
            Groups.Add(userGroup);
            group.AddUser(userGroup);
        }

        public bool IsInGroup(Group @group)
        {
            return Groups.Any(g => g.Group.Id == group.Id);
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