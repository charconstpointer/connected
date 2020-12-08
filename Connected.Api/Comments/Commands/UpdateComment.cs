using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Comments.Commands
{
    public class UpdateGroup : IRequest
    {
        public int GroupId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
    }

    public class UpdateGroupHandler : IRequestHandler<UpdateGroup>
    {
        private readonly ConnectedContext _context;

        public UpdateGroupHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateGroup request, CancellationToken cancellationToken)
        {
            var comment =
                await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);


            if (comment is null)
            {
                throw new ApplicationException();
            }

            comment.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}