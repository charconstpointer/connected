using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Connected.Api.Groups
{
    public class GetGroups : IRequest<object>
    {
    }
    public class GetGroupsHandler : IRequestHandler<GetGroups, object>
    {
        private readonly ConnectedContext _context;

        public GetGroupsHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetGroups request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.ToListAsync(cancellationToken);
            return groups;
        }
    }
}