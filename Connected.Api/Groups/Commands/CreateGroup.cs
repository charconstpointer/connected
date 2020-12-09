using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Connected.Api.Groups.Commands
{
    public class CreateGroup : IRequest
    {
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }

    public class CreateGroupHandler : IRequestHandler<CreateGroup>
    {
        private readonly ConnectedContext _context;
        private readonly ILogger<CreateGroupHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateGroupHandler(ILogger<CreateGroupHandler> logger, ConnectedContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateGroup request, CancellationToken cancellationToken)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = header.ToString().Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var tokenValues = handler.ReadJwtToken(token);
            var name = tokenValues.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value.ToString();
            if (string.IsNullOrEmpty(name))
            {
                throw new ApplicationException("token err");
            }

            var user = await _context.Users
                .Include(u=>u.CreatedGroups)
                .SingleOrDefaultAsync(u => u.Username == name,
                cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new ApplicationException("user is null");
            }
            var tags = string.Join(",", request.Tags);
            var group = new Group(request.Name, tags, user);
            
            await _context.Groups.AddAsync(group, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Creating new group");
            return Unit.Value;
        }
    }
}