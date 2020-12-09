using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Groups.Extensions;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Posts.Queries
{
    public class GetPosts : IRequest<object>
    {
        public int GroupId { get; set; }
    }
    public class GetPostsHandler : IRequestHandler<GetPosts, object>
    {
        private readonly ConnectedContext _context;

        public GetPostsHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetPosts request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.ToListAsync(cancellationToken);
            return groups.AsDto();
        }
    }
}