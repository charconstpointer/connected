using System.Threading.Tasks;
using Connected.Api.Groups.Commands;
using Connected.Api.Groups.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Groups
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
            => Ok(await _mediator.Send(new GetGroups()));

        [HttpGet("{groupId:int}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var command = new GetGroup {GroupId = groupId};
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup(CreateGroup createGroup)
        {
            var headers = Request.Headers;
            return Created("", await _mediator.Send(createGroup));
        }

        [HttpPut("{groupId:int}")]
        public async Task<IActionResult> UpdateGroup(int groupId, UpdateGroup updateGroup)
        {
            updateGroup.GroupId = groupId;
            return Ok(await _mediator.Send(updateGroup));
        }

        [HttpDelete("{groupId:int}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
            => Ok(await _mediator.Send(new DeleteGroup(groupId)));
    }
}