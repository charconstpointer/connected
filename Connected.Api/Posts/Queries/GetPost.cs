using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using Connected.Api.Posts.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Posts.Queries
{
    public class GetPost : IRequest<object>
    {
        public int PostId { get; set; }
        public int GroupId { get; set; }
    }

    public class GetPostHandler : IRequestHandler<GetPost, object>
    {
        private readonly ConnectedContext _context;

        public GetPostHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetPost request, CancellationToken cancellationToken)
        {
            var post = await _context.Items
                .Include(p=>p.Comments)
                .ThenInclude(c=>c.Author)
                .Include(p=>p.Poster)
                .FirstOrDefaultAsync(p => p.Id == request.PostId,
                cancellationToken: cancellationToken);
            if (post is null)
            {
                throw new ApplicationException("Requested post does not exist");
            }

            return post.AsDto();
        }
    }
}