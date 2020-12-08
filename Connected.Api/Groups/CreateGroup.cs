using Connected.Api.Domain.Entities;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Connected.Api.Groups
{
    public class CreateGroup : IRequest
    {
        public string Name { get; set; }
    }
    public class CreateGroupHandler : IRequestHandler<CreateGroup>
    {
        private readonly ConnectedContext _context;
        private readonly ILogger<CreateGroupHandler> _logger;

        public CreateGroupHandler(ILogger<CreateGroupHandler> logger, ConnectedContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(CreateGroup request, CancellationToken cancellationToken)
        {
            var group = new Group { Name = request.Name };
            await _context.Groups.AddAsync(group, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Creating new group");
            return Unit.Value;
        }
    }
}
