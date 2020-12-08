using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Comments.Commands
{
    public class DeleteComment : IRequest
    {
        public int GroupId { get; set; }
        public int CommentId { get; set; }

        public DeleteComment(int groupId, int commentId)
        {
            GroupId = groupId;
            CommentId = commentId;
        }
    }

    public class DeleteCommentHandler : IRequestHandler<DeleteComment>
    {
        private readonly ConnectedContext _context;

        public DeleteCommentHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteComment request, CancellationToken cancellationToken)
        {
            var comment =
                await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
