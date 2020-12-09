using System.Collections.Generic;
using System.Linq;
using Connected.Api.Domain.Entities;
using Connected.Api.Groups.Dto;
using Connected.Api.Posts.Extensions;
using Connected.Api.Users.Extensions;

namespace Connected.Api.Groups.Extensions
{
    public static class GroupMapping
    {
        public static GroupDto AsDto(this Group group)
            => new()
            {
                Creator = group.Creator?.AsDto(),
                Id = group.Id,
                Name = group.Name,
                Posts = group.Feed?.Items?.AsDto(),
                Tags = group.Tags?.Split(","),
                Users = group.Users?.Select(u => u?.User)?.AsDto(),
                CreateDate = group.CreateDate
            };

        public static IEnumerable<GroupDto> AsDto(this IEnumerable<Group> groups)
            => groups.Select(AsDto);
    }
}