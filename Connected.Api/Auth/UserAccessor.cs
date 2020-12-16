using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Auth
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConnectedContext _connectedContext;

        public UserAccessor(IHttpContextAccessor httpContextAccessor, ConnectedContext connectedContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _connectedContext = connectedContext;
        }

        public async Task<User> GetUserFromContext(CancellationToken cancellationToken)
        {
            var header = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            if (!header.HasValue)
            {
                throw new ApplicationException("cannot parse header");
            }

            var token = header.Value.ToString().Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var tokenValues = handler.ReadJwtToken(token);
            var name = tokenValues.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value.ToString();
            if (string.IsNullOrEmpty(name))
            {
                throw new ApplicationException("token err");
            }

            var user = await _connectedContext.Users
                .Include(u => u.CreatedGroups)
                .Include(u=>u.Groups)
                .ThenInclude(g=>g.Group)
                .SingleOrDefaultAsync(u => u.Username == name,
                    cancellationToken: cancellationToken);
            return user;
        }
    }
}