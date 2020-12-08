using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users
{
    public class UpdateUser : IRequest
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly ConnectedContext _context;

        public UpdateUserHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = request.Password;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}