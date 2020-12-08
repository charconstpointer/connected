using System;
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

        public DeleteComment(int groupId)
        {
            GroupId = groupId;
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
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            if (group is null)
            {
                throw new ApplicationException($"Group with id {request.GroupId} could not be found");
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}