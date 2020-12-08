using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Posts.Commands
{
    public class DeletePost : IRequest
    {
        public int GroupId { get; set; }
        public int PostId { get; set; }
    }

    public class DeletePostHandler : IRequestHandler<DeletePost>
    {
        private readonly ConnectedContext _context;

        public DeletePostHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePost request, CancellationToken cancellationToken)
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