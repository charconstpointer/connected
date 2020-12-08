using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var post = _context.Groups
                .Include(g => g.Feed)
                .ThenInclude(f => f.Items)
                .ThenInclude(i=>i.Comments)
                .FirstOrDefault(g => g.Id == request.GroupId)
                ?.Feed.Items.FirstOrDefault(i => i.Id == request.PostId);


            if (post is null)
            {
                throw new ApplicationException();
            }

            return post.Comments;
        }
    }
}