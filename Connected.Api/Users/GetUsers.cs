using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users
{
    public class GetUsers : IRequest<object>
    {
    }

    public class GetUsersHandler : IRequestHandler<GetUsers, object>
    {
        private readonly ConnectedContext _context;

        public GetUsersHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            return users;
        }
    }
}