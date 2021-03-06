using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using Connected.Api.Users.Exceptions;
using Connected.Api.Users.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Users.Queries
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
            if (!users.Any())
            {
                throw new UsersNotFoundException("Could not find any users");
            }
            return users.AsDto();
        }
    }
}