using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;

namespace Connected.Api.Users
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
            var user = new User(request.Username, request.Password, request.Email);
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}