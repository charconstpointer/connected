using Connected.Api.Comments.Extensions;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connected.Api.Comments.Queries
{
    public class GetComments : IRequest<object>
    {
        public int GroupId { get; set; }
        public int PostId { get; set; }
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
            var comments = await _context.Comments
                .Include(c => c.Post)
                .ThenInclude(p => p.Group)
                .Where(c => c.Post.Group.Id == request.GroupId)
                .ToListAsync(cancellationToken: cancellationToken);

            return comments.AsDto();
        }
    }
}