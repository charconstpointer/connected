using System.Threading;
using System.Threading.Tasks;
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
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException($"Could not find a user with id {request.UserId}");
            }
            return user.AsDto();
        }
    }
}