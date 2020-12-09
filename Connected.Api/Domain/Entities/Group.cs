using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Connected.Api.Groups.Extensions;

namespace Connected.Api.Domain.Entities
{
    public class Group
    {
        [Key] public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public User Creator { get; set; }
        public IEnumerable<UserGroup> Users { get; set; }
        [ForeignKey("FeedId")] public Feed Feed { get; set; }

        public Group(string name, string tags, User creator)
        {
            Name = name;
            Tags = tags;
            Feed = new Feed(this);
            Users = new List<UserGroup>();
            Creator = creator;
            creator.CreatedGroups.Add(this);
        }

        private Group()
        {
        }

        public void AddPost(Post post)
        {
            if (post is null)
            {
                throw new ApplicationException("Item cannot be null");
            }
            
            Feed.Items.Add(post);
        }

        public Post GetById(int postId) => Feed?.Items.FirstOrDefault(p => p.Id == postId);
    }
}