using System.Collections.Generic;
using Connected.Api.Groups.Dto;

namespace Connected.Api.Users.Dto
{
    public class UserDetailedDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<GroupSimpleDto> Groups { get; set; }
    }
}