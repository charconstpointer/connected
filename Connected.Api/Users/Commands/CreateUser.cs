using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using Connected.Api.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users.Commands
{
    public class CreateUser : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly ConnectedContext _context;

        public CreateUserHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await CheckUsername(request, cancellationToken);
            await CheckEmail(request, cancellationToken);

            var user = new User(request.Username, request.Password, request.Email);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task CheckUsername(CreateUser request, CancellationToken cancellationToken)
        {
            var exists = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username,
                cancellationToken: cancellationToken);
            if (exists is not null)
            {
                throw new UserAlreadyExistsException($"There already is user with username {request.Username}");
            }
        }

        private async Task CheckEmail(CreateUser request, CancellationToken cancellationToken)
        {
            var exists = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email,
                cancellationToken: cancellationToken);
            if (exists is not null)
            {
                throw new EmailAlreadyRegisteredException($"Email {request.Email} is already registered");
            }
        }
    }
}