using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Connected.Api.Comments.Commands
{
    public class CreateComment : IRequest
    {
        public string Content { get; set; }
        public int GroupId { get; set; }
        public int PostId { get; set; }
    }

    public class CreateCommentHandler : IRequestHandler<CreateComment>
    {
        private readonly ConnectedContext _context;
        private readonly ILogger<CreateCommentHandler> _logger;

        public CreateCommentHandler(ILogger<CreateCommentHandler> logger, ConnectedContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(CreateComment request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.
                Include(g=>g.Feed)
                .ThenInclude(f=>f.Items)
                .ThenInclude(i=>i.Comments)
                .FirstOrDefaultAsync(g => g.Id == request.GroupId,
                cancellationToken: cancellationToken);
            if (@group == null)
            {
                throw new ApplicationException("Requested group does not exist");
            }

            var post = group.GetById(request.PostId);
            if (post == null)
            {
                throw new ApplicationException("Requested post doest not exist");
            }

            var comment = new Comment(request.Content, null, post);
            post.AddComment(comment);

            await _context.Comments.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}