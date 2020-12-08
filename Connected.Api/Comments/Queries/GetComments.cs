using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Comments.Queries
{
    public class GetComments : IRequest<object>
    {
    }
    public class GetCommentsHandler : IRequestHandler<GetComments, object>
    {
        private readonly ConnectedContext _context;

        public GetCommentsHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetComments request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.ToListAsync(cancellationToken);
            return groups;
        }
    }
}