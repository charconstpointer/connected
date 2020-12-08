using System.Collections.Generic;
using System.Linq;
using Connected.Api.Comments.Extensions;
using Connected.Api.Domain.Entities;
using Connected.Api.Posts.Dto;
using Connected.Api.Users.Extensions;

namespace Connected.Api.Posts.Extensions
{
    public static class PostMapping
    {
        public static PostDto AsDto(this Post post)
            => new()
            {
                Poster = post.Poster.AsDto(),
                Body = post.Body,
                Id = post.Id,
                Comments = post.Comments.AsDto(),
                PostDate = post.PostDate
            };

        public static IEnumerable<PostDto> AsDto(this IEnumerable<Post> posts)
            => posts.Select(AsDto);
    }
}