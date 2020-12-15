using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using Connected.Api.Users.Exceptions;
using Connected.Api.Users.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users.Queries
{
    public class GetUser : IRequest<object>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }

    public class GetUserHandler : IRequestHandler<GetUser, object>
    {
        private readonly ConnectedContext _context;
        public GetUserHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetUser request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> expression = u => u.Id == request.UserId;
            if (!string.IsNullOrEmpty(request.Username))
            {
                expression = u => u.Username == request.Username;
            }


            var user = await _context.Users
                .Include(u => u.Items)
                .FirstOrDefaultAsync(expression, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException($"Could not find a user with id {request.UserId}");
            }

            return user.AsDto();
        }
    }
}