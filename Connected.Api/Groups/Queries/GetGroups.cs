using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Groups.Extensions;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Groups.Queries
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
            var groups = await _context.Groups
                .Include(g=>g.Feed)
                .ThenInclude(f=>f.Items)
                .ThenInclude(i=>i.Comments)
                .Include(g=>g.Users)
                .Include(g=>g.Creator)
                .ToListAsync(cancellationToken);
            return groups.AsDto();
        }
    }
}