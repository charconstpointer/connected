using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Posts.Commands
{
    public class UpdatePost : IRequest
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
    
    public class UpdatePostHandler : IRequestHandler<UpdatePost>
    {
        private readonly ConnectedContext _context;

        public UpdatePostHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            if (@group == null)
            {
                throw new ApplicationException($"Group with id {request.GroupId} could not be found");
            }

            group.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}