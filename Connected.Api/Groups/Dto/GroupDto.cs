using System;
using System.Collections.Generic;
using Connected.Api.Posts.Dto;
using Connected.Api.Users.Dto;

namespace Connected.Api.Groups.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public UserDto Creator { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }
}