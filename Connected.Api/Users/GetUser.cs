using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users
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
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        }
    }
}