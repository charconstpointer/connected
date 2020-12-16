using System.Collections.Generic;
using System.Linq;
using Connected.Api.Domain.Entities;
using Connected.Api.Groups.Extensions;
using Connected.Api.Users.Dto;

namespace Connected.Api.Users.Extensions
{
    public static class UserMapping
    {
        public static UserDto AsDto(this User user)
            => new()
            {
                Username = user.Username,
                Email = user.Email
            };

        public static UserDetailedDto AsDetailedDto(this User user)
            => new()
            {
                Username = user.Username,
                Email = user.Email,
                Groups = user.Groups?.Select(g => g.Group).AsSimpleDto()
            };
        public static IEnumerable<UserDetailedDto> AsDetailedDto(this IEnumerable<User> users)
            => users.Select(AsDetailedDto);
        public static IEnumerable<UserDto> AsDto(this IEnumerable<User> users)
            => users.Select(AsDto);
    }
}