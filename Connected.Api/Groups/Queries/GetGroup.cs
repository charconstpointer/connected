using System;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Groups.Dto;
using Connected.Api.Groups.Extensions;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Groups.Queries
{
    public class GetGroup : IRequest<GroupDto>
    {
        public int GroupId { get; set; }
    }

    public class GetGroupHandler : IRequestHandler<GetGroup, GroupDto>
    {
        private readonly ConnectedContext _context;

        public GetGroupHandler(ConnectedContext context)
        {
            _context = context;
        }

        public async Task<GroupDto> Handle(GetGroup request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups
                .Include(g => g.Creator)
                .Include(g => g.Feed)
                .ThenInclude(f => f.Items)
                .ThenInclude(i => i.Poster)
                .Include(g => g.Feed)
                .ThenInclude(f => f.Items)
                .ThenInclude(i => i.Comments)
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == request.GroupId,
                    cancellationToken: cancellationToken);
            if (group is null)
            {
                throw new ApplicationException($"could not find group with id {request.GroupId}");
            }

            return group.AsDto();
        }
    }
}