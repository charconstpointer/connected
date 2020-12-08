using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Connected.Api.Posts.Commands
{
    public class CreatePost : IRequest
    {
        public string Name { get; set; }
    }
    public class CreatePostHandler : IRequestHandler<CreatePost>
    {
        private readonly ConnectedContext _context;
        private readonly ILogger<CreatePostHandler> _logger;

        public CreatePostHandler(ILogger<CreatePostHandler> logger, ConnectedContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            // var group = new Group { Name = request.Name };
            // await _context.Groups.AddAsync(group, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Creating new group");
            return Unit.Value;
        }
    }
}
