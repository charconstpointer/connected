using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Groups
{
    public class DeleteGroup : IRequest
    {
        public int GroupId { get; set; }

        public DeleteGroup(int groupId)
        {
            GroupId = groupId;
        }
    }

    public class DeleteGroupHandler : IRequestHandler<DeleteGroup>
    {
        private readonly ConnectedContext _context;

        public DeleteGroupHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGroup request, CancellationToken cancellationToken)
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