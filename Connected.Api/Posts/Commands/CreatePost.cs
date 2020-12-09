using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Auth;

namespace Connected.Api.Posts.Commands
{
    public class CreatePost : IRequest
    {
        public int GroupId { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
    }

    public class CreatePostHandler : IRequestHandler<CreatePost>
    {
        private readonly ConnectedContext _context;
        private readonly ILogger<CreatePostHandler> _logger;
        private readonly IUserAccessor _userAccessor;

        public CreatePostHandler(ILogger<CreatePostHandler> logger, ConnectedContext context,
            IUserAccessor userAccessor)
        {
            _logger = logger;
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups
                .Include(g => g.Feed)
                .ThenInclude(f => f.Items)
                .ThenInclude(i => i.Comments)
                .FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);

            if (group is null)
            {
                throw new ApplicationException($"Group with id {request.GroupId} could not be found");
            }

            var user = await _userAccessor.GetUserFromContext(cancellationToken);
            //TODO Poster
            var post = new Post(request.Body, user, group);
            group.AddPost(post);
            await _context.Items.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}