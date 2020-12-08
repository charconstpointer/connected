using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Comments.Queries
{
    public class GetComment : IRequest<object>
    {
        public int GroupId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }

    }

    public class GetCommentHandler : IRequestHandler<GetComment, object>
    {
        private readonly ConnectedContext _context;

        public GetCommentHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetComment request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken: cancellationToken);
            return comment;
        }
    }
}