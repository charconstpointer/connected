using System;
using System.Collections.Generic;
using Connected.Api.Comments.Dto;
using Connected.Api.Users.Dto;

namespace Connected.Api.Posts.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
        public UserDto Poster { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}