using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using Connected.Api.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users.Commands
{
    public class DeleteUser : IRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly ConnectedContext _context;

        public DeleteUserHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException($"Could not find a user with id {request.UserId}");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}