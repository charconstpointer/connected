using System.Collections.Generic;
using System.Linq;
using Connected.Api.Comments.Dto;
using Connected.Api.Domain.Entities;

namespace Connected.Api.Comments.Extensions
{
    public static class CommentMapping
    {
        public static CommentDto AsDto(this Comment comment)
            => new CommentDto
            {
                Author = comment.Author?.Username,
                Content = comment.Content,
                Id = comment.Id,
                CreateDate = comment.CreateDate
            };

        public static IEnumerable<CommentDto> AsDto(this IEnumerable<Comment> comments)
            => comments.Select(AsDto);
    }
}